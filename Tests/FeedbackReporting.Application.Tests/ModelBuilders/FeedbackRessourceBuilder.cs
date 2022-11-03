using FeedbackReporting.Domain.Models.Ressources;
using System;

namespace FeedbackReporting.Application.Tests.ModelBuilders
{
    public class FeedbackRessourceBuilder
    {
        private int _id;
        private string _title;
        private string _description;
        private string _creatorName;
        private DateTime _creationDate;
        private string _payload;

        public FeedbackRessource Build()
        {
            return new FeedbackRessource
            {
                Id = this._id,
                Title = this._title,
                Description = this._description,
                CreatorName = this._creatorName,
                CreationDate = this._creationDate,
                Payload = this._payload
            };
        }

        public FeedbackRessourceBuilder WithId(int id)
        {
            this._id = id;
            return this;
        }

        public FeedbackRessourceBuilder WithTitle(string title)
        {
            this._title = title;
            return this;
        }

        public FeedbackRessourceBuilder WithDescription(string description)
        {
            this._description = description;
            return this;
        }

        public FeedbackRessourceBuilder WithCreatorName(string creatorName)
        {
            this._creatorName = creatorName;
            return this;
        }

        public FeedbackRessourceBuilder WithCreationDate(DateTime creationDate)
        {
            this._creationDate = creationDate;
            return this;
        }

        public FeedbackRessourceBuilder WithPayload(string payload)
        {
            this._payload = payload;
            return this;
        }
    }
}
