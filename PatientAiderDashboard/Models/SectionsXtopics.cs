using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatientAiderDashboard.Models
{
    [Table("SectionsXTopics")]
    public partial class SectionsXtopics
    {
        public int SectionId { get; set; }
        public int TopicId { get; set; }

        [ForeignKey("SectionId")]
        [InverseProperty("SectionsXtopics")]
        public virtual Sections Section { get; set; }
        [ForeignKey("TopicId")]
        [InverseProperty("SectionsXtopics")]
        public virtual Topics Topic { get; set; }
    }
}
