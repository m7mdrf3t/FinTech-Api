using api.DTOs.Account;
using api.Interfaces;
using api.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly ITokenService _tokenService;
    private readonly SignInManager<AppUser> _signInManager;

    public AccountController(UserManager<AppUser> userManager , ITokenService tokenService , SignInManager<AppUser> signInManager)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _signInManager = signInManager;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        try
        {
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            var appUser = new AppUser{
                UserName = registerDto.Username,
                Email = registerDto.EmailAddress
            };

            var CreatedUser = await _userManager.CreateAsync(appUser,registerDto.Password);

            if(CreatedUser.Succeeded){
                var addUserRolw = await _userManager.AddToRoleAsync(appUser,"User");

                if(addUserRolw.Succeeded) 
                {
                    return Ok( new NewUserDto { 
                        UserName = appUser.UserName,
                        Email = appUser.Email,
                        Token = _tokenService.CreateToken(appUser)

                    });
                }
                else
                {
                    return StatusCode(500,addUserRolw.Errors);
                }
            }
            else
            {
                return StatusCode(500,CreatedUser.Errors);
            }


        }
        catch (System.Exception e)
        {
            
            return StatusCode(500,e);
        }

    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto){

        if(!ModelState.IsValid) return BadRequest(ModelState);

        try
        {
            var User = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.UserName);
            if(User == null) return Unauthorized("Wrong UserName , please check again");

            var CredCheck = await _signInManager.CheckPasswordSignInAsync(User , loginDto.Password,false);
            if(!CredCheck.Succeeded) 
            {
                return Unauthorized("Password / or Username is not Right please check your cred");
            }else
            {
                return Ok(new NewUserDto {
                    UserName = User.UserName,
                    Email = User.Email,
                    Token = _tokenService.CreateToken(User)

                });
            }
        }
        catch (System.Exception)
        {
            
            return Unauthorized("SomeThing went Wrong");
        }


    }

}
