using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PatientAiderDashboard.Models;
using PatientAiderDashboard.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace PatientAiderDashboard
{
    [Route("api/client")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ITopicRepository db;

        public ClientController(IMapper map, ITopicRepository context)
        {
            mapper = map;
            db = context;
        }

        [HttpGet("encounters")]
        public IEnumerable<EncounterDTO> GetEncounters()
        {
            var encs = db.GetEncounters();
            List<EncounterDTO> encounters = new List<EncounterDTO>();
            List<SectionDTO> sections = new List<SectionDTO>();
            foreach (var enc in encs)
            {
                var encounter = (mapper.Map<Encounters, EncounterDTO>(enc));
                encounters.Add(encounter);   
            }
            return encounters;
        }

        [HttpGet("sections")]
        public IEnumerable<SectionDTO> GetSections()
        {
            var sects = db.GetSections();
            List<SectionDTO> sections = new List<SectionDTO>();
            foreach (var s in sects)
            {
                var sectDto = mapper.Map<Sections, SectionDTO>(s);
                sections.Add(sectDto);
            }
            return sections;
        }

        [HttpGet("topics")]
        public IEnumerable<TopicMetadata> GetTopics()
        {
            return db.GetTopicMetadata();
        }
    }
}