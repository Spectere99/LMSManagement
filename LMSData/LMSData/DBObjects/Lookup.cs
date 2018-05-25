using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LIMSData.DBObjects
{
    public class Lookup
    {
        [Key]
        public int Id { get; set; }
        public int LookupTypeId { get; set; }
        public string LookupValue { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime LastModified { get; set; }
        public string LastModifiedBy { get; set; }
        public bool Archived { get; set; }
    }

}
