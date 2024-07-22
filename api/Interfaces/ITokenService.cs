using api.Model;

namespace api.Interfaces;

public interface ITokenService
{
    string CreateToken(AppUser user);
}
