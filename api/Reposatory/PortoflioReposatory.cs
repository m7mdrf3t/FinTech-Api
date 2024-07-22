using api.Data;
using api.Interfaces;
using api.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace api.Reposatory;

public class PortoflioReposatory : IPortoflioReposatory
{
    private readonly ApplicationDbContext _applicationDbContext;

    public PortoflioReposatory(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }
    public async Task<List<Stock>> GetUserPortflio(AppUser User)
    {
        var portoflio = await _applicationDbContext.portoflios.Where(x => x.AppUserId == User.Id).
        Select(stock => new Stock{
            Id = stock.StockId,
            sympol = stock.stock.sympol,
            CompanyName = stock.stock.CompanyName,
            Purchase = stock.stock.Purchase,
            Lastdev = stock.stock.Lastdev,
            Industry = stock.stock.Industry,
            MarketCap = stock.stock.MarketCap
        }).ToListAsync();

        return portoflio;
    }

    public async Task<Portoflio> CreateAsync(Portoflio portoflio)
    {
        await _applicationDbContext.portoflios.AddAsync(portoflio);
        _applicationDbContext.SaveChanges();
        return portoflio;
    }

    public async Task<Portoflio> DeleteAsync(AppUser appUser, string symbol)
    {
        var portoflioModel = await _applicationDbContext.portoflios.FirstOrDefaultAsync(u => u.AppUserId == appUser.Id && u.stock.sympol.ToLower() == symbol.ToLower());
        if( portoflioModel == null) 
            return null;
        
        _applicationDbContext.portoflios.Remove(portoflioModel);
        await _applicationDbContext.SaveChangesAsync();

        return portoflioModel;
    }
}
