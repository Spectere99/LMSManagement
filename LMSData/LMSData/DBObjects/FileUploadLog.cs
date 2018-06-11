using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIMSData.DBObjects
{
    public class FileUploadLog
    {
        [Key]
        public int Id { get; set; }
        public string BatchName { get; set; }
        public Lookup Module { get; set; }
        public DateTime Uploaded { get; set; }
        public string FileName { get; set; }
        public string SourceIpAddress { get; set; }
        public int RecordCount { get; set; }
        public int SuccessCount { get; set; }
        public int FailureCount { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public bool Archived { get; set; }
    }
}
