using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using PatientAiderDashboard.Models;
using PatientAiderDashboard.Repositories;

namespace PatientAiderDashboard.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ITopicRepository db;
        public HomeController(ITopicRepository context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            return View(db.GetEncounters());
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult EditTopic(int id)
        {
            return View("EditTopic", db.GetTopicById(id));
        }

        public JsonResult GetTopicsForAddRemove(int sectionId, int encounterId)
        {
            var topics = db.GetTopics();
            var sect = db.GetSectionById(sectionId);
            var selectedTopics = sect.SectionsXtopics.ToList().Select(st => st.Topic.Id).ToList();
            var topicsSelectList = new MultiSelectList(topics, "Id", "Title", selectedTopics);
            return Json(topicsSelectList);
        }

        public string UpdateTopicsForSection(int sectionId, int encounterId, string topics)
        {
            List<Topics> newTopics = new List<Topics>();
            var section = db.GetSectionById(sectionId);
            List<TopicMetadata> topicMetadataList = new List<TopicMetadata>();

            //Next, actually link the new list of child topics.  To do this, first blow away the current list.
            //Fuck it, the logic is simpler and this in in-mem, right?
            foreach (var st in section.SectionsXtopics.ToList())
            {
                section.SectionsXtopics.Remove(st);
            } //flush...

            foreach (var topicId in topics.Split(','))
            {
                int tId = Int32.Parse(topicId);
                var currentTopic = db.GetTopicById(tId);

                section.SectionsXtopics.Add(new SectionsXtopics
                {
                    Section = section,
                    SectionId = section.Id,
                    Topic = currentTopic,
                    TopicId = tId
                });
                newTopics.Add(currentTopic);
                topicMetadataList.Add(new TopicMetadata(currentTopic));
            }

            section.SectionTopicOrder = JsonConvert.SerializeObject(topicMetadataList);
            try
            {
                db.UpdateSection(section);
                var topicViewModel = new UpdatedSectionViewModel(section, topicMetadataList);
                var jsonTopics = JsonConvert.SerializeObject(topicViewModel);
                return jsonTopics;
            }
            catch (Exception e)
            {
                return "Update Failed";
            }
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
