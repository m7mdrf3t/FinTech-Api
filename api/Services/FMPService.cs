using api.Data;
using api.Interfaces;
using api.Model;
using Microsoft.EntityFrameworkCore;

namespace api.Services;

public class FMPService : IFMPService
{
    private readonly ApplicationDbContext _applicationDbContext;

    public FMPService(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<Stock> FindStockBySymbol(string symbol)
    {
        return await _applicationDbContext.Stocks.FirstOrDefaultAsync(x => x.sympol == symbol);
    }
}
