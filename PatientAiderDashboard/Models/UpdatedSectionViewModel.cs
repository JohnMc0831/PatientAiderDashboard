using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientAiderDashboard.Models
{
    public class UpdatedSectionViewModel
    {
        public SectionDTO Section { get; set; }
        public List<TopicDTO> Topics { get; set; }
        public string SectionTopicListName { get; set; }
        public bool Initialized { get; set; }

        public UpdatedSectionViewModel()
        {
            Initialized = false;
        }

        public UpdatedSectionViewModel(Sections section, List<Topics> topics)
        {
            Section = new SectionDTO(section);
            Topics = topics.Select(t => new TopicDTO(t)).ToList();
            SectionTopicListName = $"{section.Encounter.EncounterName}-{section.SectionName}-topicList";
            Initialized = true;
        }
    }
}
