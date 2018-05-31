using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatientAiderDashboard.Models
{
    public partial class Sections
    {
        public Sections()
        {
            SectionsXtopics = new HashSet<SectionsXtopics>();
        }

        [Column("id")]
        public int Id { get; set; }
        public int? EncounterId { get; set; }
        public string SectionName { get; set; }
        [StringLength(50)]
        public string SectionIcon { get; set; }
        [StringLength(255)]
        public string SectionTopicOrder { get; set; }

        [ForeignKey("EncounterId")]
        [InverseProperty("Sections")]
        public virtual Encounters Encounter { get; set; }
        [InverseProperty("Section")]
        public virtual ICollection<SectionsXtopics> SectionsXtopics { get; set; }
    }
}
