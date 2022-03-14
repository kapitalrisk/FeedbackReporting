using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FeedbackReporting.Domain.Models.Entities
{
    [Table("feedback_keywords")]
    public class FeedbackKeywordEntity
    {
        [Column("keyword_hashcode")]
        public int HashCode { get; set; }

        [Column("keyword")]
        public string Keyword { get; set; }

        [Column("feedback_id")]
        public int FeedbackId { get; set; }
    }
}
