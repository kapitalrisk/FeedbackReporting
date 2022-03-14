using FeedbackReporting.Domain.Models.Ressources;
using System.Threading.Tasks;

namespace FeedbackReporting.Domain.Services
{
    public interface IJWTService
    {
        string GenerateJWT(UserRessource user);
        Task<UserRessource> LoginUser(LoginRessource loginIdentifiers);
        Task<bool> CreateUser(UserRessource user);
        Task<bool> DeleteUserByEmail(string email);
    }
}
