using api.DTOs.Stock;
using api.Helpers;
using api.Interfaces;
using api.Mapper;
using api.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StockController : ControllerBase
{
    private readonly IStockReposatory _stockReposatory;

    public StockController(IStockReposatory stockReposatory)
    {
        _stockReposatory = stockReposatory;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAll([FromQuery] QueryObject query){
        
        if(!ModelState.IsValid) return BadRequest(ModelState);
        List<Stock> stocks =  await _stockReposatory.GetAllAsync(query);
        var stockDto = stocks.Select(s => s.ToStockDto()).ToList(); 
        if(stocks == null) return NotFound();

        return Ok(stockDto);
    }


    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetByID([FromQuery] int id)
    {
        if(!ModelState.IsValid) return BadRequest(ModelState);
        Stock stock = await _stockReposatory.GetByID(id);

        if(stock == null){
            return NotFound();
        }

        return Ok(stock.ToStockDto());
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody]CreatStockDto CreatStockDto){
        if(!ModelState.IsValid) return BadRequest(ModelState);
        var newCreatedstock = await _stockReposatory.CreateAsync(CreatStockDto.ToStockFromCreateDTO());

        if(newCreatedstock == null){
            return BadRequest();
        }

        return CreatedAtAction(nameof(GetByID) , new {id = newCreatedstock.Id } , newCreatedstock.ToStockDto());
    }


    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> Update([FromRoute]int id , [FromBody]UpdateStockDto updateStockDto){
        if(!ModelState.IsValid) return BadRequest(ModelState);

        var updatestatus = await _stockReposatory.UpdateAsync(id,updateStockDto);

        if(updatestatus == null){
            return NotFound();
        }

        return Ok(updatestatus.ToStockDto());
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id){
        if(!ModelState.IsValid) return BadRequest(ModelState);

        var stock = await _stockReposatory.DeleteAsync(id);

        if(stock == null){
            return BadRequest();
        }

        return NoContent();
    }





}
