using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
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
    public StockController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]

    public IActionResult GetAll()
    {
        var stocks = _context.Stocks.ToList().Select(s => s.ToStockDto());
        return Ok(stocks);
    }

    [HttpGet("{id}")]
    public IActionResult GetById([FromRoute]int id)
    {
        var stock = _context.Stocks.Find(id);
        if(stock is null)
            return NotFound();
        return Ok(stock.ToStockDto());
            
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreatedStockRequestDto stockDto)
    {
        var stockModel = stockDto.ToStockFromCreateDto();
        _context.Stocks.Add(stockModel);
        _context.SaveChanges();
        return CreatedAtAction(nameof(GetById),new{id = stockModel.Id},stockModel.ToStockDto());
            
    }

    [HttpPut]
    [Route("{id}")]
    public IActionResult Update([FromRoute] int id , [FromBody] UpdateStockRequestDto updateDto)
    {
        var stockModel = _context.Stocks.FirstOrDefault(s=>s.Id ==id );
        if(stockModel is null)
            return NotFound();

        stockModel.Symbol = updateDto.Symbol;
        stockModel.CompanyName = updateDto.CompanyName;
        stockModel.Purchase = updateDto.Purchase;
        stockModel.LastDiv = updateDto.LastDiv;
        stockModel.Industry = updateDto.Industry;
        stockModel.MarketCap = updateDto.MarketCap;
        
        _context.SaveChanges();
        return Ok(stockModel.ToStockDto());

            
    }

    [HttpDelete]
    [Route("{id}")]
    public IActionResult Delete([FromRoute] int id)
    {
        var stockModel = _context.Stocks.FirstOrDefault(s=>s.Id ==id);
        if(stockModel is null)
            return NotFound();
        _context.Stocks.Remove(stockModel);
        _context.SaveChanges();

        // this used to show all the other elements in the list if you want it.
        // var stocks = _context.Stocks.ToList().Select(s=>s.ToStockDto());
        // return Ok(stocks);

        return NoContent();
    }




    }
    
    
}