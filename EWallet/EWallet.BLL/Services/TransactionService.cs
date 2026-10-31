using EWallet.BLL.Models;
using EWallet.BLL.Resources;
using EWallet.DAL.Repositories;
using Microsoft.Extensions.Localization;

namespace EWallet.BLL.Services;

public class TransactionService : ITransactionService
{
    private readonly IUserRepository _users;
    private readonly ITransactionRepository _transactions;
    private readonly IStringLocalizer<Messages> _l;

    public TransactionService(IUserRepository users, ITransactionRepository transactions,
        IStringLocalizer<Messages> l)
    {
        _users = users;
        _transactions = transactions;
        _l = l;
    }

    public async Task<List<TransactionItem>> GetHistoryAsync(
        string userId, DateTime? from, DateTime? to)
    {
        var user = await _users.GetByIdAsync(userId);
        if (user == null) return new List<TransactionItem>();
        
        DateTime? fromUtc = from.HasValue
            ? DateTime.SpecifyKind(from.Value.Date, DateTimeKind.Utc) : null;
        DateTime? toUtc = to.HasValue
            ? DateTime.SpecifyKind(to.Value.Date.AddDays(1), DateTimeKind.Utc) : null;

        var list = await _transactions.GetByAccountAsync(user.AccountNumber, fromUtc, toUtc);

        return list.Select(t =>
        {
            var incoming = t.ToAccount == user.AccountNumber;
            var outgoing = t.FromAccount == user.AccountNumber;

            string type, counterparty;
            switch (t.Type)
            {
                case "Deposit":
                    type = _l["TypeDeposit"];
                    counterparty = _l["Anonymous"];
                    break;
                case "Transfer":
                    type = incoming ? _l["TypeTransferIn"] : _l["TypeTransferOut"];
                    counterparty = incoming ? t.FromAccount ?? "" : t.ToAccount ?? "";
                    break;
                default: 
                    type = _l["TypePayment"];
                    counterparty = t.ToAccount ?? "";
                    break;
            }

            return new TransactionItem
            {
                Date = t.Date,
                Type = type,
                Amount = incoming ? t.Amount : -t.Amount,
                Counterparty = counterparty
            };
        }).ToList();
    }
}