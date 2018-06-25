using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PatientAiderDashboard.Models;
using PatientAiderDashboard.Repositories;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace PatientAiderDashboard.Controllers
{
    [Route("api/client")]
    [ApiController]
    [AllowAnonymous]
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

        [HttpGet("topic/{id}")]
        public TopicDTO GetTopicById(int id)
        {
            var topic = db.GetTopicById(id);
            var topicDto = mapper.Map<Topics, TopicDTO>(topic);
            return topicDto;
        }

        [HttpGet("footnotes")]
        public Footnotes GetFootnotes()
        {
            return db.GetFootnotes();
        }
    }
}