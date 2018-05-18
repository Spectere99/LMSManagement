using System;

namespace LIMSData.DBObjects
{
    public class Lookup
    {
        public int Id { get; set; }
        public LookupType Type { get; set; }
        public string LookupValue { get; set; }
        public DateTime Created { get; set; }
        public int CreatedBy { get; set; }
        public DateTime LastModified { get; set; }
        public int LastModifiedBy { get; set; }
        public bool Archived { get; set; }
    }

}
