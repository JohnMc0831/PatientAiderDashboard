using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PatientAiderDashboard.Models
{
    public class SectionDTO
    {
        public int Id { get; set; }

        public int? EncounterId { get; set; }

        public string SectionName { get; set; }

        [StringLength(50)]
        public string SectionIcon { get; set; }

        public string SectionTopicOrder { get; set; }

        public SectionDTO()
        {
            
        }
        public SectionDTO(Sections s)
        {
            Id = s.Id;
            EncounterId = s.EncounterId.HasValue ? s.EncounterId : 0;
            SectionName = s.SectionName;
            SectionIcon = s.SectionIcon;
        }

    }
}