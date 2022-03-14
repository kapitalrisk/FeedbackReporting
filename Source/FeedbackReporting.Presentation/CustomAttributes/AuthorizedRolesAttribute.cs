using Microsoft.AspNetCore.Authorization;

namespace FeedbackReporting.Presentation.CustomAttributes
{
    public class AuthorizedRolesAttribute : AuthorizeAttribute
    {
        public AuthorizedRolesAttribute(params string[] roles) : base()
        {
            Roles = string.Join(",", roles);
        }
    }
}
