using FeedbackReporting.Domain.Constants;
using System;
using System.Security.Claims;

namespace FeedbackReporting.Presentation.Helpers
{
    public static class ClaimsHelper
    {
        public static DateTime? GetLastInsertionDate(this ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal.IsInRole(UserRoles.User) && claimsPrincipal.HasClaim(x => x.Type == Claims.LastInsertionDate))
            {
                if (DateTime.TryParse(claimsPrincipal.FindFirst(Claims.LastInsertionDate).Value, out var result))
                    return result;
            }
            return null;
        }

        public static void UpdateLastInsertionDate(this ClaimsPrincipal claimsPrincipal)
        {
            var identity = claimsPrincipal.Identity as ClaimsIdentity;

            if (identity == null)
                return;
            if (identity.HasClaim(x => x.Type == Claims.LastInsertionDate))
                identity.RemoveClaim(identity.FindFirst(Claims.LastInsertionDate));

            identity.AddClaim(new Claim(Claims.LastInsertionDate, DateTime.UtcNow.ToString()));
            claimsPrincipal.AddIdentity(identity);
        }
    }
}
