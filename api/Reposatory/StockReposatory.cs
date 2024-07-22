using api.Data;
using api.DTOs.Stock;
using api.Helpers;
using api.Interfaces;
using api.Mapper;
using api.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace api.Reposatory;

public class StockReposatory : IStockReposatory
{
    private readonly ApplicationDbContext _applicationDbContext;

    public StockReposatory(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<bool> IsExsit(int stockId) 
    { 
        return await _applicationDbContext.Stocks.AnyAsync(s => s.Id == stockId);
    }

    public async Task<Stock> CreateAsync(Stock stockModel)
    {
        await _applicationDbContext.Stocks.AddAsync(stockModel);
        await _applicationDbContext.SaveChangesAsync();
        return stockModel;
    }

    public async Task<Stock?> DeleteAsync(int id)
    {
        var stock = await _applicationDbContext.Stocks.FirstOrDefaultAsync(s => s.Id == id);

        if(stock == null){
            return null;
        }

         _applicationDbContext.Stocks.Remove(stock);
         await _applicationDbContext.SaveChangesAsync();

         return (stock);
    }

    public async Task<List<Stock>> GetAllAsync(QueryObject query)
    {
        var stocks = _applicationDbContext.Stocks.Include(s=> s.Comments).ThenInclude(x => x.appUser).AsQueryable();

        if(!string.IsNullOrWhiteSpace(query.Sympol)){
            stocks = stocks.Where(s => s.sympol == query.Sympol);
        }
        if(!string.IsNullOrWhiteSpace(query.CompanyName)){
            stocks = stocks.Where(s => s.sympol == query.CompanyName);
        }

        if(!string.IsNullOrWhiteSpace(query.orderby)){
            if(query.orderby.Equals("symbol" , StringComparison.OrdinalIgnoreCase)){
                stocks = query.IsDescending ? stocks.OrderByDescending(s => s.sympol) : stocks.OrderBy(x => x.sympol); 
            }
        }

        int skipnumbner = (query.PageNumber - 1) * query.PageSize;


        return await stocks.Skip(skipnumbner).Take(query.PageSize).ToListAsync();
    }

    public async Task<Stock?> GetByID(int Id)
    {
        Stock stock = await _applicationDbContext.Stocks.Include(c => c.Comments).FirstOrDefaultAsync(d => d.Id == Id);
        return stock;
    }

    public async Task<Stock?> UpdateAsync(int id , UpdateStockDto stock)
    {
        Stock _stock = await _applicationDbContext.Stocks.FirstOrDefaultAsync(d => d.Id == id);
        if(_stock == null){
            return null;
        }
        _stock.sympol = stock.sympol;
        _stock.Industry = stock.Industry;
        _stock.CompanyName = stock.Industry;
        _stock.Lastdev = stock.Lastdev;
        _stock.MarketCap = _stock.MarketCap;

        await _applicationDbContext.SaveChangesAsync();

        return _stock;
    }
}
