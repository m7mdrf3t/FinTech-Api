using System;
using System.Data.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using api.Interfaces;
using api.Model;
using Microsoft.IdentityModel.Tokens;

namespace api.Services;

public class TokenService : ITokenService
{

    public readonly IConfiguration _config;
    public readonly SymmetricSecurityKey _key;

    public TokenService(IConfiguration config)
    {
        _config = config;
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SigningKey"]));
        
    }

    public string CreateToken(AppUser user)
    {
        var Claims = new List<Claim>{
            new Claim(JwtRegisteredClaimNames.Email , user.Email),
            new Claim(JwtRegisteredClaimNames.GivenName , user.UserName)
        };

        var Cred = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

        var ClaimDiscriptor = new SecurityTokenDescriptor{
            Subject = new ClaimsIdentity(Claims),
            Expires = DateTime.Now.AddHours(2),
            SigningCredentials = Cred,
            Issuer = _config["Jwt:Issuer"],
            Audience = _config["JWT:Audience"]
        };

        var jwtHandler = new JwtSecurityTokenHandler();

        var Token = jwtHandler.CreateToken(ClaimDiscriptor);

        return jwtHandler.WriteToken(Token);

    }
}
