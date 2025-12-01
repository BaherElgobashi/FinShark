using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace api.Controllers
{
    [Route("api/stock")]
    [ApiController]

    public class StockController : ControllerBase
    {
    private readonly ApplicationDbContext _context;
    private readonly IStockRepository _stockRepo;
    public StockController(ApplicationDbContext context , IStockRepository stockRepo)
    {
        _stockRepo = stockRepo;
        _context = context;
    }

    [HttpGet]

    public async Task<IActionResult> GetAll([FromQuery]QueryObject query)
    {
        if(!ModelState.IsValid)
                return BadRequest(ModelState);
        var stocks = await _stockRepo.GetAllAsync(query);
        var stockDto = stocks.Select(s => s.ToStockDto());
        return Ok(stockDto);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute]int id)
    {
        if(!ModelState.IsValid)
                return BadRequest(ModelState);
        var stock = await _stockRepo.GetByIdAsync(id);
        if(stock is null)
            return NotFound();
        return Ok(stock.ToStockDto());
            
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatedStockRequestDto stockDto)
    {
        if(!ModelState.IsValid)
                return BadRequest(ModelState);
        var stockModel =  stockDto.ToStockFromCreateDto();
        // await _context.Stocks.AddAsync(stockModel);
        // await _context.SaveChangesAsync();
        await _stockRepo.CreateAsync(stockModel);
        return CreatedAtAction(nameof(GetById),new{id = stockModel.Id},stockModel.ToStockDto());
            
    }

    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id , [FromBody] UpdateStockRequestDto updateDto)
    {
        if(!ModelState.IsValid)
                return BadRequest(ModelState);
        var stockModel = await _stockRepo.UpdateAsync(id,updateDto);
        if(stockModel is null)
            return NotFound();

        // stockModel.Symbol = updateDto.Symbol;
        // stockModel.CompanyName = updateDto.CompanyName;
        // stockModel.Purchase = updateDto.Purchase;
        // stockModel.LastDiv = updateDto.LastDiv;
        // stockModel.Industry = updateDto.Industry;
        // stockModel.MarketCap = updateDto.MarketCap;
        
        // await _context.SaveChangesAsync();
        return Ok(stockModel.ToStockDto());

            
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        if(!ModelState.IsValid)
                return BadRequest(ModelState);
        var stockModel = await _stockRepo.DeleteAsync(id);
        if(stockModel is null)
            return NotFound();
        // _context.Stocks.Remove(stockModel);
        // await _context.SaveChangesAsync();

        // this used to show all the other elements in the list if you want it.
        // var stocks = _context.Stocks.ToList().Select(s=>s.ToStockDto());
        // return Ok(stocks);

        return NoContent();
    }




    }
    
    
}