using FeedbackReporting.Domain.Models.Entities;

namespace FeedbackReporting.Domain.Models.Ressources
{
    public class UserRessource
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }

        public UserRessource()
        { }

        public UserRessource(UserEntity fromEntity)
        {
            this.Id = fromEntity.Id;
            this.Name = fromEntity.Name;
            this.Role = fromEntity.Role;
        }

        public UserEntity ToEntity()
        {
            return new UserEntity
            {
                Id = this.Id,
                Name = this.Name,
                Role = this.Role,
            };
        }
    }
}
