using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace My_Scripture_Journal.Models
{
    public class JournalEntry
    {
        public int ID { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required] 
        public string Book { get; set; }
        [Display(Name = "Entry Date")]
        [DataType(DataType.Date)]
        public DateTime EntryDate { get; set; }
        public string Verse { get; set; }
        public string Notes { get; set; }
        [RegularExpression(@"^[0-9]*$")]
        public string Chapter { get; set; }
    }
}
