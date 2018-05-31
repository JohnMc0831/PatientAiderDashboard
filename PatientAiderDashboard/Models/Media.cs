using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatientAiderDashboard.Models
{
    public partial class Media
    {
        public Media()
        {
            TopicsMedia = new HashSet<TopicsMedia>();
        }

        [Column("id")]
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Title { get; set; }
        public string Tip { get; set; }
        public int? FileId { get; set; }
        [StringLength(255)]
        public string FileName { get; set; }
        [StringLength(50)]
        public string ContentType { get; set; }
        public byte[] Content { get; set; }

        [InverseProperty("Media")]
        public virtual ICollection<TopicsMedia> TopicsMedia { get; set; }
    }
}
