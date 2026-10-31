using EWallet.BLL.Models;
using EWallet.BLL.Resources;
using EWallet.DAL.Entities;
using EWallet.DAL.Repositories;
using Microsoft.Extensions.Localization;

namespace EWallet.BLL.Services;

public class TransferService : ITransferService
{
    private readonly IUserRepository _users;
    private readonly ITransactionRepository _transactions;
    private readonly IStringLocalizer<Messages> _l;

    public TransferService(IUserRepository users, ITransactionRepository transactions,
        IStringLocalizer<Messages> l)
    {
        _users = users;
        _transactions = transactions;
        _l = l;
    }

    public async Task<OperationResult> TransferAsync(
        string senderUserId, string recipientAccount, decimal amount)
    {
        amount = Math.Round(amount, 2);
        if (amount <= 0)
            return OperationResult.Fail(_l["InvalidAmount"]);

        var sender = await _users.GetByIdAsync(senderUserId);
        if (sender == null)
            return OperationResult.Fail(_l["UnexpectedError"]);

        var recipient = await _users.GetByAccountNumberAsync(recipientAccount?.Trim() ?? string.Empty);
        if (recipient == null)
            return OperationResult.Fail(_l["AccountNotFound"]);

        if (recipient.Id == sender.Id)
            return OperationResult.Fail(_l["TransferToSelf"]);

        if (sender.Balance < amount)
            return OperationResult.Fail(_l["InsufficientFunds"]);
        
        if (amount > 1_000_000)
            return OperationResult.Fail(_l["AmountTooLarge"]);

        sender.Balance -= amount;
        recipient.Balance += amount;

        await _transactions.AddAsync(new Transaction
        {
            Date = DateTime.UtcNow,
            Amount = amount,
            Type = "Transfer",
            FromAccount = sender.AccountNumber,
            ToAccount = recipient.AccountNumber
        });

        await _users.SaveChangesAsync();

        return OperationResult.Ok(_l["TransferSuccess"], sender.Balance);
    }
}