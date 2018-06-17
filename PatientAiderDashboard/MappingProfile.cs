using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PatientAiderDashboard.Models;

namespace PatientAiderDashboard
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<Sections, SectionDTO>();
            CreateMap<Encounters, EncounterDTO>();
            CreateMap<Topics, TopicDTO>();
        }
    }
}
