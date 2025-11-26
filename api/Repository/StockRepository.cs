using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDbContext _context;
        public StockRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Stock>> GetAllAsync()
        {
            var stocks = await _context.Stocks.Include(c=>c.Comments).ToListAsync();
            return stocks;
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            // var stock = await _context.Stocks.FindAsync(id);
            var stock = await _context.Stocks.Include(c=>c.Comments).FirstOrDefaultAsync(s=>s.Id == id);
            return stock;
        }
        public async Task<Stock> CreateAsync(Stock stockModel)
        {
            await _context.Stocks.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto updateDto)
        {
            var stock = await _context.Stocks.FirstOrDefaultAsync(s=>s.Id == id);
            if(stock == null)
                return null;

            stock.Symbol = updateDto.Symbol;
            stock.CompanyName = updateDto.CompanyName;
            stock.Purchase = updateDto.Purchase;
            stock.LastDiv = updateDto.LastDiv;
            stock.Industry = updateDto.Industry;
            stock.MarketCap = updateDto.MarketCap;
            await _context.SaveChangesAsync();
            return stock;
        }
        public async Task<Stock?> DeleteAsync(int id)
        {
            var stock = await _context.Stocks.FirstOrDefaultAsync(s=>s.Id==id);
            if(stock is null)
                return null;
            _context.Stocks.Remove(stock);
            await _context.SaveChangesAsync();
            return stock;
        }
    }
}