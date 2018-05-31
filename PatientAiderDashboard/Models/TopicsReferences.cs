using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatientAiderDashboard.Models
{
    public partial class TopicsReferences
    {
        public int TopicId { get; set; }
        public int ReferenceId { get; set; }

        [ForeignKey("ReferenceId")]
        [InverseProperty("TopicsReferences")]
        public virtual References Reference { get; set; }
        [ForeignKey("TopicId")]
        [InverseProperty("TopicsReferences")]
        public virtual Topics Topic { get; set; }
    }
}
