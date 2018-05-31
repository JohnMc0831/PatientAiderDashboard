using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatientAiderDashboard.Models
{
    public partial class References
    {
        public References()
        {
            TopicsReferences = new HashSet<TopicsReferences>();
        }

        [Column("id")]
        public int Id { get; set; }
        [Required]
        public string Reference { get; set; }
        public int? LinkId { get; set; }

        [ForeignKey("LinkId")]
        [InverseProperty("References")]
        public virtual Links Link { get; set; }
        [InverseProperty("Reference")]
        public virtual ICollection<TopicsReferences> TopicsReferences { get; set; }
    }
}
