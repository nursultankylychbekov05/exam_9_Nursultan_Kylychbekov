using EWallet.DAL.Context;
using EWallet.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace EWallet.DAL.Repositories;

public class ProviderRepository : IProviderRepository
{
    private readonly ApplicationDbContext _db;
    public ProviderRepository(ApplicationDbContext db) => _db = db;

    public Task<List<Provider>> GetAllAsync() => _db.Providers.ToListAsync();

    public Task<ProviderAccount?> FindAccountAsync(int providerId, string identifier) =>
        _db.ProviderAccounts.FirstOrDefaultAsync(a =>
            a.ProviderId == providerId && a.Identifier == identifier);
    
    public Task<Provider?> GetByIdAsync(int id) =>
        _db.Providers.FirstOrDefaultAsync(p => p.Id == id);
}