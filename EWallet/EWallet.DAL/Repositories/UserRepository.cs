using EWallet.DAL.Context;
using EWallet.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace EWallet.DAL.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _db;
    public UserRepository(ApplicationDbContext db) => _db = db;

    public Task<ApplicationUser?> GetByIdAsync(string id) =>
        _db.Users.FirstOrDefaultAsync(u => u.Id == id);

    public Task<ApplicationUser?> GetByAccountNumberAsync(string accountNumber) =>
        _db.Users.FirstOrDefaultAsync(u => u.AccountNumber == accountNumber);

    public Task<bool> AccountNumberExistsAsync(string accountNumber) =>
        _db.Users.AnyAsync(u => u.AccountNumber == accountNumber);

    public Task SaveChangesAsync() => _db.SaveChangesAsync();
}