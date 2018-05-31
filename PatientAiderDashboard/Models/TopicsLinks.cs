using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatientAiderDashboard.Models
{
    public partial class TopicsLinks
    {
        public int TopicId { get; set; }
        public int LinkId { get; set; }

        [ForeignKey("LinkId")]
        [InverseProperty("TopicsLinks")]
        public virtual Links Link { get; set; }
        [ForeignKey("TopicId")]
        [InverseProperty("TopicsLinks")]
        public virtual Topics Topic { get; set; }
    }
}
