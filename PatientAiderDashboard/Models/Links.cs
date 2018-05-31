using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatientAiderDashboard.Models
{
    public partial class Links
    {
        public Links()
        {
            References = new HashSet<References>();
            TopicsLinks = new HashSet<TopicsLinks>();
        }

        [Column("id")]
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Title { get; set; }
        [Required]
        [StringLength(1024)]
        public string Link { get; set; }

        [InverseProperty("Link")]
        public virtual ICollection<References> References { get; set; }
        [InverseProperty("Link")]
        public virtual ICollection<TopicsLinks> TopicsLinks { get; set; }
    }
}
