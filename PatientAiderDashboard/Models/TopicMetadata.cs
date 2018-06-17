using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PatientAiderDashboard.Models
{
    public class TopicMetadata
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Icon { get; set; }

        public TopicMetadata(Topics t)
        {
            Id = t.Id;
            Title = t.Title;
            Summary = t.Summary;
            Icon = t.TopicIcon;
        }
    }
}
