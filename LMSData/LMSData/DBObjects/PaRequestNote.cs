using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIMSData.DBObjects
{
    public class PaRequestNote
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("PaRequest")]
        public int PaRequestId { get; set; }
        public PaRequest PaRequest { get; set; }
        public string NoteText { get; set; }
        public bool IsPublic { get; set; }
        public DateTime Created { get; set; }
        public string CreatedBy { get; set; }
        public DateTime LastModified { get; set; }
        public string LastModifiedBy { get; set; }
        public bool Archived { get; set; }

    }
}
