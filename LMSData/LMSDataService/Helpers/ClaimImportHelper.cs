using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using ExcelDataReader;
using LIMSData;
using LIMSData.DBObjects;
using log4net;

namespace LMSDataService.Helpers
{
    public class ClaimImportHelper : IFileImportHelper
    {
        private const int _SUBMIT_DATE_IDX = 2;
        private const int _INSURANCE_IDX = 3;
        private const int _DOCTOR_IDX = 4;
        private const int _PATIENT_IDX = 5;
        private const int _DRUG_NAME_IDX = 6;
        private const int _NOTES_IDX = 7;

        public List<string> Import(HttpFileCollection files, string saveFolder, string user, string hostIP)
        {
            LMSDataDBContext db = new LMSDataDBContext();
            ILog _log = log4net.LogManager.GetLogger(
                System.Reflection.MethodBase.GetCurrentMethod().DeclaringType
            );
            var filePath = string.Empty;
            var docfiles = new List<string>();

            foreach (string file in files)
            {
                var postedFile = files[file];
                if (!System.IO.Directory.Exists(HttpContext.Current.Server.MapPath("~/" + saveFolder + "/")))
                {
                    System.IO.Directory.CreateDirectory(
                        HttpContext.Current.Server.MapPath("~/" + saveFolder + "/"));
                }

                if (postedFile != null)
                {
                    filePath =
                        HttpContext.Current.Server.MapPath("~/" + saveFolder + "/" + postedFile.FileName);
                    var errRows = 0;
                    var successRows = 0;
                    var totalRowCount = 0;
                    var fileUploadId = 0;
                    //var filePath = saveFolder + postedFile.FileName;
                    postedFile.SaveAs(filePath);

                    FileUploadLog fileUploadLogRecord = db.FileUploadLogs.Create();

                    fileUploadLogRecord.Id = 0;
                    fileUploadLogRecord.Created = DateTime.Now;
                    fileUploadLogRecord.CreatedBy = user;
                    fileUploadLogRecord.FileName = postedFile.FileName;
                    fileUploadLogRecord.ModuleId = 2;
                    fileUploadLogRecord.RecordCount = totalRowCount;
                    fileUploadLogRecord.SuccessCount = successRows;
                    fileUploadLogRecord.FailureCount = errRows;
                    fileUploadLogRecord.SourceIpAddress = hostIP;
                    fileUploadLogRecord.Uploaded = DateTime.Now;

                    db.FileUploadLogs.Add(fileUploadLogRecord);
                    db.SaveChanges();

                    fileUploadId = fileUploadLogRecord.Id;
                    fileUploadLogRecord.BatchName =
                        fileUploadLogRecord.Created.ToString("yyyyMMdd") + fileUploadId;

                    db.SaveChanges();
                    // Process the file.
                    using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
                    {
                        // Auto-detect format, supports:
                        //  - Binary Excel files (2.0-2003 format; *.xls)
                        //  - OpenXml Excel files (2007 format; *.xlsx)
                        using (var reader = ExcelReaderFactory.CreateReader(stream))
                        {
                            // Need to hold the Submitted date and the Insurance Code.  
                            // If the following row is blank for either, then assume the one before.
                            var curSubmitDate = string.Empty;
                            var curInsuranceCode = string.Empty;
                            var excelSet = reader.AsDataSet();
                            totalRowCount = excelSet.Tables[0].Rows.Count - 1;
                            bool firstRow = true;
                            foreach (DataRow dr in excelSet.Tables[0].Rows)
                            {
                                var insuranceCompanyCode = dr[_INSURANCE_IDX].ToString().Trim() == string.Empty
                                    ? curInsuranceCode
                                    : dr[_INSURANCE_IDX].ToString();
                                curInsuranceCode = insuranceCompanyCode;
                                if (firstRow)
                                {
                                    firstRow = false;
                                    continue;
                                }

                                PaRequest newRequest = db.PaRequests.Create();

                                newRequest.Id = 0;
                                newRequest.Created = DateTime.Now;

                                InsuranceCompany insCompany = db.InsuranceCompanies.SingleOrDefault(p =>
                                    p.CompanyCode == insuranceCompanyCode);
                                if (insCompany == null) //If it doesn't exist, then add it by default.
                                {
                                    var newInsCompany = db.InsuranceCompanies.Create();
                                    newInsCompany.Id = 0;
                                    newInsCompany.CompanyCode = dr[_INSURANCE_IDX].ToString();
                                    newInsCompany.CompanyName =
                                        (newInsCompany.CompanyCode.Trim() == string.Empty)
                                            ? "Unknown"
                                            : newInsCompany.CompanyCode;
                                    newInsCompany.Archived = false;
                                    newInsCompany.CreatedBy = user;
                                    newInsCompany.Created = DateTime.Now;
                                    newInsCompany.LastModified = DateTime.Now;
                                    newInsCompany.LastModifiedBy = user;

                                    db.InsuranceCompanies.Add(newInsCompany);
                                    try
                                    {
                                        db.SaveChanges();
                                        insCompany = newInsCompany;
                                    }
                                    catch (Exception e)
                                    {
                                        _log.Error(
                                            "An error occurred when trying to add new Insurance Company.", e);
                                        insCompany = null;
                                        errRows++;
                                    }
                                }

                                if (insCompany == null) continue;
                                {
                                    newRequest.InsuranceCompany_Id = insCompany.Id;
                                    newRequest.CreatedBy = user;
                                    newRequest.LastModifiedBy = user;
                                    newRequest.LastModified = DateTime.Now;
                                    var submitDate = dr[_SUBMIT_DATE_IDX].ToString().Trim() == string.Empty
                                        ? curSubmitDate
                                        : dr[_SUBMIT_DATE_IDX].ToString();
                                    curSubmitDate = submitDate;
                                    newRequest.Submitted = DateTime.Parse(submitDate);
                                    newRequest.Archived = false;
                                    newRequest.DoctorName = dr[_DOCTOR_IDX].ToString();
                                    newRequest.PatientName = dr[_PATIENT_IDX].ToString();
                                    newRequest.DrugName = dr[_DRUG_NAME_IDX].ToString();
                                    newRequest.Note = dr[_NOTES_IDX].ToString();
                                    newRequest.Completed = false;
                                    newRequest.Status = 2; //  Default value of 'Pending'
                                    newRequest.AssignedToId = 1;
                                    // newRequest.CompletedTimeStamp = DateTime.Parse("1/1/1900"); 
                                    newRequest.FileUploadLogId = fileUploadId;
                                    // Need to check for Duplicates.  This is based on the Patient Name, Prescription Name and Doctor Name.
                                    var foundRequest = db.PaRequests.Any(p =>
                                        p.Submitted == newRequest.Submitted &&
                                        p.PatientName == newRequest.PatientName &&
                                        p.DoctorName == newRequest.DoctorName &&
                                        p.DrugName == newRequest.DrugName);

                                    if (foundRequest)
                                    {
                                        errRows++;
                                        continue;
                                    }

                                    db.PaRequests.Add(newRequest);

                                    try
                                    {
                                        db.SaveChanges();
                                        successRows++;
                                    }
                                    catch (Exception e)
                                    {
                                        _log.Error("An Error occurred during Excel Import of PA Requests", e);
                                        errRows++;
                                    }
                                }
                            }

                            // The result of each spreadsheet is in result.Tables
                        }
                    }

                    fileUploadLogRecord.RecordCount = totalRowCount;
                    fileUploadLogRecord.SuccessCount = successRows;
                    fileUploadLogRecord.FailureCount = errRows;
                }

                db.SaveChanges(); //Save to pickup the File Upload Log Record changes.
                // Delete the temp file.
            }

            // Need to delete working file.
            File.Delete(filePath);
            return docfiles;
            //result = Request.CreateResponse(HttpStatusCode.Created, docfiles);
        }
    }
}