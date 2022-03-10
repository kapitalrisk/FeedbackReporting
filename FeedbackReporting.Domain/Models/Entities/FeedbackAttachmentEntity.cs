using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FeedbackReporting.Domain.Models.Entities
{
    [Table("feedback_attachments")]
    public class FeedbackAttachmentEntity
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int Id { get; set; }

        [Column("feedback_id")]
        public int FeedbackId { get; set; }

        [Column("data")]
        public byte[] Data { get; set; }
    }
}
