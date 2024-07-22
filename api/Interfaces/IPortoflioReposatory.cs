using api.Model;

namespace api.Interfaces;

public interface IPortoflioReposatory
{
    Task<List<Stock>> GetUserPortflio(AppUser User);
    Task<Portoflio> CreateAsync(Portoflio portoflio);

    Task<Portoflio> DeleteAsync(AppUser appUser , string symbol);
}
