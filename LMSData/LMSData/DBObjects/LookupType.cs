using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIMSData.DBObjects
{
    public class LookupType
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public bool Archived { get; set; }
        public ICollection<Lookup> Lookups { get; set; }

    }
}
