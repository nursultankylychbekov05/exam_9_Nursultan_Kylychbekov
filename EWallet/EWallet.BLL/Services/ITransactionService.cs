using EWallet.BLL.Models;

namespace EWallet.BLL.Services;

public interface ITransactionService
{
    Task<List<TransactionItem>> GetHistoryAsync(string userId, DateTime? from, DateTime? to);
}