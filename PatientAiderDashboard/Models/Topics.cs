using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatientAiderDashboard.Models
{
    public partial class Topics
    {
        public Topics()
        {
            SectionsXtopics = new HashSet<SectionsXtopics>();
        }

        [Column("id")]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string TitleGerman { get; set; }
        public string TitleSpanish { get; set; }
        public string Summary { get; set; }
        public string SummaryGerman { get; set; }
        public string SummarySpanish { get; set; }
        public string Body { get; set; }
        public string BodyGerman { get; set; }
        public string BodySpanish { get; set; }
        public string BackColor { get; set; }
        public string TextColor { get; set; }
        public int DisplayOrder { get; set; }
        [StringLength(100)]
        public string TopicIcon { get; set; }

        [InverseProperty("Topic")]
        public virtual ICollection<SectionsXtopics> SectionsXtopics { get; set; }
    }
}
