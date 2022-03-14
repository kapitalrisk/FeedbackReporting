using FeedbackReporting.Domain.Models.Entities;

namespace FeedbackReporting.Domain.Models.Ressources
{
    public class FeedbackAttachmentRessource
    {
        public int Id { get; set; }
        public int FeedbackId { get; set; }
        public byte[] Data { get; set; }

        public FeedbackAttachmentRessource()
        { }

        public FeedbackAttachmentRessource(FeedbackAttachmentEntity fromEntity)
        {
            this.Id = fromEntity.Id;
            this.FeedbackId = fromEntity.FeedbackId;
            this.Data = fromEntity.Data;
        }

        public FeedbackAttachmentEntity ToEntity()
        {
            return new FeedbackAttachmentEntity
            {
                Id = this.Id,
                FeedbackId = this.FeedbackId,
                Data = this.Data
            };
        }
    }
}
