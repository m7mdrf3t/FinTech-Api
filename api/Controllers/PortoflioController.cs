using api.Extenstions;
using api.Interfaces;
using api.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PortoflioController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IStockReposatory _stockReposatory;
    private readonly IPortoflioReposatory _portoflioReposatory;
    private readonly IFMPService _fMPService;

    public PortoflioController(UserManager<AppUser> userManager , IStockReposatory stockReposatory , IPortoflioReposatory portoflioReposatory , IFMPService fMPService) 
    {
        _userManager = userManager;
        _stockReposatory = stockReposatory;
        _portoflioReposatory = portoflioReposatory;
        _fMPService = fMPService;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetPortoflio()
    {
        var Username = User.GetUserName();
        var user = await _userManager.FindByNameAsync(Username);
        
        if(user == null) return Unauthorized();
        
        List<Stock> stocks = await _portoflioReposatory.GetUserPortflio(user);
        return Ok(stocks);

    }


    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreatePortoflio(string symbol)
    {
        var username = User.GetUserName();
        var user = await _userManager.FindByNameAsync(username);
        if(user == null) return NotFound();
        var UserStock = await _fMPService.FindStockBySymbol(symbol);

        var UserPorto = await _portoflioReposatory.GetUserPortflio(user);
        if(UserPorto.Any(e => e.sympol.ToLower() == symbol.ToLower())) return BadRequest("Duplicated stock");

        var portoflio = new Portoflio{
            appUser = user,
            stock = UserStock
        };

        var Createdportoflio =  await _portoflioReposatory.CreateAsync(portoflio);

        if(Createdportoflio == null) 
        {
            return StatusCode(500 ,"couldn't create portoflio");
        }
        else
        {
            return Created("",portoflio);
        }
    }

    [HttpDelete]
    [Authorize]
    public async Task<IActionResult> DeletePortoflio(string symbol)
    {
        var username = User.GetUserName();
        var _user = await _userManager.FindByNameAsync(username);

        if(User == null) return BadRequest("User not Found");

        var userPortoflio = await _portoflioReposatory.GetUserPortflio(_user);

        var filterStock = userPortoflio.Where(e => e.sympol.ToLower() == symbol.ToLower()).ToList();

        if(filterStock.Count == 1){
            await _portoflioReposatory.DeleteAsync(_user,symbol);

        }else
        {
            return BadRequest("Nothing is in the Portoflio");
        }

        return Ok();


    }


}
