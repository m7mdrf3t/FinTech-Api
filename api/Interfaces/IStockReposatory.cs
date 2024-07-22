using api.DTOs.Stock;
using api.Helpers;
using api.Model;

namespace api.Interfaces;

public interface IStockReposatory
{
    Task<List<Stock>> GetAllAsync(QueryObject query);
    Task<Stock?> GetByID(int id);

    Task<Stock> CreateAsync(Stock stockModel);

    Task<Stock?> UpdateAsync(int id , UpdateStockDto stockModel);

    Task<Stock?> DeleteAsync(int id);

    Task<bool> IsExsit(int id);

}