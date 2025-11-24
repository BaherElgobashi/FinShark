using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
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
        var stocks = _context.Stocks.ToList();
        return Ok(stocks);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var stock = _context.Stocks.Find(id);
        if(stock is null)
            return NotFound();
        return Ok(stock);
            
    }




    }
    
    
}