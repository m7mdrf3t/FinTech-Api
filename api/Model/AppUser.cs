using Microsoft.AspNetCore.Identity;

namespace api.Model;

public class AppUser : IdentityUser
{
    public List<Portoflio> portoflios = new List<Portoflio>();
    public List<Comment> comment {get; set; }
}
