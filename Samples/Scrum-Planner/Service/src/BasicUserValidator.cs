using System.Net;
using System.Security.Claims;
using Nancy.Authentication.Basic;

namespace Service
{
    public class BasicUserValidator : IUserValidator
    {
        public ClaimsPrincipal Validate(string username, string password)
        {
            if (password.ToLowerInvariant() != "spartakiade2018")
            {
                return null;
            }
            return new ClaimsPrincipal(new HttpListenerBasicIdentity(username, password));
        }
    }
}
