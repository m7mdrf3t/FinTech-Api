using api.Model;

namespace api.Interfaces;

public interface IFMPService
{
    Task<Stock> FindStockBySymbol(string symbol);
}
