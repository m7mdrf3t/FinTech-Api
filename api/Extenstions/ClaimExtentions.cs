using System.Security.Claims;

namespace api.Extenstions;

public static class ClaimExtentions
{
    public static string GetUserName(this ClaimsPrincipal claimsPrincipal){
        var username = claimsPrincipal.Claims.SingleOrDefault(x => x.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname")).Value;
        return username;
    }
}
