using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatientAiderDashboard.Models
{
    public partial class Encounters
    {
        public Encounters()
        {
            Sections = new HashSet<Sections>();
        }

        [Column("id")]
        public int Id { get; set; }
        [Required]
        public string EncounterName { get; set; }

        [InverseProperty("Encounter")]
        public virtual ICollection<Sections> Sections { get; set; }
    }
}
