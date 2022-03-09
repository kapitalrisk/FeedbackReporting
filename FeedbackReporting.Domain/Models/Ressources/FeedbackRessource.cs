using FeedbackReporting.Domain.Models.Entities;
using System;

namespace FeedbackReporting.Domain.Models.Ressources
{
    public class FeedbackRessource
    {
        public int Id { get; set;  }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CreatorId { get; set; }
        public DateTime CreationDate { get; set; }
        public string Payload { get; set; }

        public FeedbackRessource()
        { }

        public FeedbackRessource(FeedbackEntity fromEntity)
        {
            this.Id = fromEntity.Id;
            this.Title = fromEntity.Title;
            this.Description = fromEntity.Description;
            this.CreatorId = fromEntity.CreatorId;
            this.CreationDate = fromEntity.CreationDate;
            this.Payload = fromEntity.Payload;
        }

        public FeedbackEntity ToEntity()
        {
            return new FeedbackEntity
            {
                Id = this.Id,
                Title = this.Title,
                Description = this.Description,
                CreatorId = this.CreatorId,
                CreationDate = this.CreationDate,
                Payload = this.Payload
            };
        }
    }
}
