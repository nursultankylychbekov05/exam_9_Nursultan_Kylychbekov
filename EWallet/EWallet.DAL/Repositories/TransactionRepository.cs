using EWallet.DAL.Context;
using EWallet.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace EWallet.DAL.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private readonly ApplicationDbContext _db;
    public TransactionRepository(ApplicationDbContext db) => _db = db;

    public async Task AddAsync(Transaction transaction) =>
        await _db.Transactions.AddAsync(transaction);

    public Task<List<Transaction>> GetByAccountAsync(string accountNumber, DateTime? from, DateTime? to)
    {
        var query = _db.Transactions
            .Where(t => t.FromAccount == accountNumber || t.ToAccount == accountNumber);

        if (from.HasValue) query = query.Where(t => t.Date >= from.Value);
        if (to.HasValue) query = query.Where(t => t.Date < to.Value);

        return query.OrderByDescending(t => t.Date).ToListAsync();
    }
}