using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatientAiderDashboard.Models
{
    public partial class TopicsMedia
    {
        public int TopicId { get; set; }
        public int MediaId { get; set; }

        [ForeignKey("MediaId")]
        [InverseProperty("TopicsMedia")]
        public virtual Media Media { get; set; }
        [ForeignKey("TopicId")]
        [InverseProperty("TopicsMedia")]
        public virtual Topics Topic { get; set; }
    }
}
