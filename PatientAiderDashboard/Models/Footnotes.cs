using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatientAiderDashboard.Models
{
    public partial class Footnotes
    {
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("footnotes")]
        public string Footnotes1 { get; set; }
    }
}
