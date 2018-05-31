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

        public List<int> TopicIds { get; set; }

        public string SectionTopicOrder { get; set; }

        public IEnumerable<TopicDTO> Topics { get; set; }

        public SectionDTO(Sections s)
        {
            Id = s.Id;
            EncounterId = s.EncounterId.HasValue ? s.EncounterId : 0;
            SectionName = s.SectionName;
            SectionIcon = s.SectionIcon;
            Topics = s.SectionsXtopics.ToList().Select(st => new TopicDTO(st.Topic)).ToList();
        }

    }
}