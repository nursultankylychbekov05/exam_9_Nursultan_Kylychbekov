using EWallet.DAL.Entities;

namespace EWallet.DAL.Repositories;

public interface ITransactionRepository
{
    Task AddAsync(Transaction transaction);
    Task<List<Transaction>> GetByAccountAsync(string accountNumber, DateTime? from, DateTime? to);
}