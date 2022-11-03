using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FeedbackReporting.Domain.Models.Entities
{
    [Table("feedback")]
    public class FeedbackEntity
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int Id { get; set; }

        [Column("title")]
        public string Title { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("creator_name")]
        public string CreatorName { get; set; }

        [Column("creation_date")]
        public DateTime CreationDate { get; set; }

        [Column("payload")]
        public string Payload { get; set; }
    }
}
