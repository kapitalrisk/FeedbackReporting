using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FeedbackReporting.Domain.Models.Entities
{
    [Table("user")]
    public class UserEntity
    {
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int Id { get; set; }

        [Key]
        [Column("name")]
        public string Name { get; set; }

        [Column("password_hash")]
        public string PasswordHash { get; set; }

        [Column("role")]
        public string Role { get; set; }
    }
}
