using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatientAiderDashboard.Models
{
    public partial class Icons
    {
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("icon")]
        [StringLength(100)]
        public string Icon { get; set; }
    }
}
