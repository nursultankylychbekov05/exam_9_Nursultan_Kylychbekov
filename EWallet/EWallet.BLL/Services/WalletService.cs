using EWallet.BLL.Models;
using EWallet.BLL.Resources;
using EWallet.DAL.Entities;
using EWallet.DAL.Repositories;
using Microsoft.Extensions.Localization;

namespace EWallet.BLL.Services;

public class WalletService : IWalletService
{
    private readonly IUserRepository _users;
    private readonly ITransactionRepository _transactions;
    private readonly IStringLocalizer<Messages> _l;

    public WalletService(IUserRepository users, ITransactionRepository transactions,
        IStringLocalizer<Messages> l)
    {
        _users = users;
        _transactions = transactions;
        _l = l;
    }

    public async Task<OperationResult> DepositAsync(string accountNumber, decimal amount)
    {
        if (amount <= 0)
            return OperationResult.Fail(_l["InvalidAmount"]);
        
        if (amount > 1_000_000)
            return OperationResult.Fail(_l["AmountTooLarge"]);

        var user = await _users.GetByAccountNumberAsync(accountNumber?.Trim() ?? string.Empty);
        if (user == null)
            return OperationResult.Fail(_l["AccountNotFound"]);

        user.Balance += amount;

        await _transactions.AddAsync(new Transaction
        {
            Date = DateTime.UtcNow,
            Amount = amount,
            Type = "Deposit",
            FromAccount = null,
            ToAccount = user.AccountNumber
        });

        await _users.SaveChangesAsync();

        return OperationResult.Ok(_l["DepositSuccess"], user.Balance);
    }
}