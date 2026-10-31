using EWallet.DAL.Entities;

namespace EWallet.DAL.Repositories;

public interface IUserRepository
{
    Task<ApplicationUser?> GetByIdAsync(string id);
    Task<ApplicationUser?> GetByAccountNumberAsync(string accountNumber);
    Task<bool> AccountNumberExistsAsync(string accountNumber);
    Task SaveChangesAsync();
}