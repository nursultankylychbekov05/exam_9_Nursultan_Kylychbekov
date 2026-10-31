using EWallet.DAL.Entities;

namespace EWallet.DAL.Repositories;

public interface IProviderRepository
{
    Task<List<Provider>> GetAllAsync();
    Task<ProviderAccount?> FindAccountAsync(int providerId, string identifier);
}