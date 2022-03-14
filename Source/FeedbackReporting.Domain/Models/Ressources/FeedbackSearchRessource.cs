using System;

namespace FeedbackReporting.Domain.Models.Ressources
{
    public class FeedbackSearchRessource
    {
        public string CreatorName { get; set; }
        public DateTime? DateOfCreation { get; set; }
        public string[] Keywords { get; set; }
    }
}
