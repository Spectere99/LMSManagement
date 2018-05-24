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
        public int CreatedBy { get; set; }
        public DateTime LastModified { get; set; }
        public int LastModifiedBy { get; set; }
        public bool Archived { get; set; }
    }

}
