using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PatientAiderDashboard.Models;


namespace PatientAiderDashboard.Repositories
{
    public interface ITopicRepository
    {
        List<TopicMetadata> GetTopicMetadata();
        Footnotes GetFootnotes();
        void UpdateFootnotes(Footnotes notes);
        List<Encounters> GetEncounters();
        List<Sections> GetSections();
        List<Sections> GetSectionsWithEncounters();
        Sections GetSectionById(int id);
        Sections GetSectionWithTopicsById(int id);
        void UpdateSection(Sections section);
        List<string> GetSupportedLanguages();
        List<Topics> GetTopics();
        List<SectionDTO> GetSectionsWithTopics();
        Topics GetTopicById(int id);
        Topics GetTopicByName(string name);
        int GetNextTopicDisplayOrderValue();
        void AddTopic(Topics topic);
        void UpdateTopic(Topics topic);
        void RemoveTopic(Topics topic);
        List<Icons> GetIcons();
    }

    public class TopicRepository : ITopicRepository
    {
        private readonly PatientAiderContext db = new PatientAiderContext();
        private bool disposed = false;

        public TopicRepository()
        {
            
        }

        public Footnotes GetFootnotes()
        {
            return db.Footnotes.First();
        }

        public void UpdateFootnotes(Footnotes notes)
        {
            db.Entry(notes).State = EntityState.Modified;
            db.SaveChanges();
        }

        public List<Encounters> GetEncounters()
        {
            List<Encounters> encounters = db.Encounters.OrderBy(e => e.Id).ToList();
            return encounters;
        }

        public List<Sections> GetSections()
        {
            return db.Sections.OrderBy(s => s.Id).ToList();
        }

        public List<Sections> GetSectionsWithEncounters()
        {
            //return db.Sections.Include(s => s.Encounter).Include(s => s.Topics).OrderBy(s => s.id).ToList();
            return db.Sections.OrderBy(s => s.Id).ToList();
        }

        public Sections GetSectionById(int id)
        {
            return db.Sections.Include(s => s.SectionsXtopics).ThenInclude(s => s.Topic).First(s => s.Id == id);
        }

        public Sections GetSectionWithTopicsById(int id)
        {
            return db.Sections.Find(id);
        }

        public void UpdateSection(Sections section)
        {
            db.Entry(section).State = EntityState.Modified;
            db.SaveChanges();
        }

        public List<string> GetSupportedLanguages()
        {
            var languages = new List<string>
            {
                "English",
                "German",
                "Spanish"
            };
            return languages;
        }

        public List<Topics> GetTopics()
        {
            return db.Topics.OrderBy(t => t.DisplayOrder).ToList();
        }

        public List<TopicMetadata> GetTopicMetadata()
        {
            return db.Topics.OrderBy(t => t.Id).Select(t => new TopicMetadata(t)).ToList();
        }


        public List<SectionDTO> GetSectionsWithTopics()
        {
            //var sectDTOs = new List<SectionDTO>();
            var sects = db.Sections.ToList();
            return sects.Select(s => new SectionDTO(s)).ToList();
            //foreach (var s in sects)
            //{
            //    sectDTOs.Add(new SectionDTO
            //    {
            //        EncounterId = s.EncounterId,
            //        id = s.id,
            //        SectionName = s.SectionName,
            //        SectionIcon = s.SectionIcon,
            //        Topic = s.Topic.Select(t => new TopicDTO(t)).ToList()
            //    });
            //}
            //return sectDTOs;
        }

        public Topics GetTopicById(int id)
        {
            return db.Topics.Find(id);
        }

        public Topics GetTopicByName(string name)
        {
            return db.Topics.First(t => t.Title.Contains(name));
        }

        public int GetNextTopicDisplayOrderValue()
        {
            if (db.Topics.Any())
            {
                int maxDisplayOrder = db.Topics.Max(t => t.DisplayOrder);
                return maxDisplayOrder + 1;
            }
            return 1;
        }

        public void AddTopic(Topics topic)
        {
            db.Topics.Add(topic);
            db.SaveChanges();
        }

        public void UpdateTopic(Topics topic)
        {
            db.Entry(topic).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void RemoveTopic(Topics topic)
        {
            db.Entry(topic).State = EntityState.Deleted;
            db.SaveChanges();
        }

        public List<Icons> GetIcons()
        {
            return db.Icons.OrderBy(i => i.Icon).ToList();
        }
    }
}
