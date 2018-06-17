using System.Collections.Generic;
using System.Linq;
using PatientAiderDashboard.Models;

namespace PatientAiderDashboard.Models
{
    public class EncounterDTO
    {
        public int id { get; set; }

        public string EncounterName { get; set; }
        public IEnumerable<SectionDTO> Sections { get; set; }

        public EncounterDTO()
        {

            
        }

        public EncounterDTO(Encounters e)
        {
            id = e.Id;
            EncounterName = e.EncounterName;
            Sections = e.Sections.Select(s => new SectionDTO(s)).ToList();
        }
    }
}