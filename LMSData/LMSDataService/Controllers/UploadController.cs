using System.CodeDom;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using log4net;
using LIMSData;
using LMSDataService.Helpers;

namespace LMSDataService.Controllers
{
    public class UploadController : ApiController
    {
        private const int MOD_PAREQUEST = 1;
        private const int MOD_CLAIMS = 2;

        private LMSDataDBContext db = new LMSDataDBContext();
        static ILog _log = log4net.LogManager.GetLogger(
            System.Reflection.MethodBase.GetCurrentMethod().DeclaringType
        );

        public HttpResponseMessage Post(HttpRequestMessage request)
        {
            //Get the orderNumber from the header.  With this, we can prefix the name of the file and save it
            string saveFolder = System.Configuration.ConfigurationManager.AppSettings["TempDocFolder"];
            HttpResponseMessage result = null;
            var filePath = string.Empty;

            if (_log.IsDebugEnabled)
            {
                _log.DebugFormat("Executing call in debug mode");
            }

            var headers = request.Headers;

            //Check the request object to see if they passed a userId
            if (headers.Contains("userid"))
            {
                if (headers.Contains("module"))
                {
                    var user = headers.GetValues("userid").First();
                    var moduleId = 0; 
                    
                    var res = int.TryParse(headers.GetValues("module").First(), out moduleId);

                    _log.InfoFormat("Handling {0} Document Upload from user: {1}", moduleId, user);

                    
                    var httpRequest = HttpContext.Current.Request;
                    IFileImportHelper fileImportHelper;

                    if (httpRequest.Files.Count > 0)
                    {
                        switch (moduleId)
                        {
                            case MOD_CLAIMS:
                            {
                                
                                break;
                            }
                            case MOD_PAREQUEST:
                            {
                                fileImportHelper = new PaRequestImportHelper();
                                var uploadResults = fileImportHelper.Import(httpRequest.Files, saveFolder, user, NetworkHelper.GetIpAddress());
                                break;
                            }
                            default:
                            {
                                result = Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid Module in Header");
                                break;
                            }
                        }
                        
                        


                    }
                    else
                    {
                        result = Request.CreateResponse(HttpStatusCode.BadRequest, "No Files Submitted for Upload");
                    }

                    return result;
                }

                result = Request.CreateResponse(HttpStatusCode.BadRequest, "Header value <module> not found.");
                return result;

            }

            result = Request.CreateResponse(HttpStatusCode.BadRequest, "Header value <userid> not found.");
            return result;
        }
    }
}
