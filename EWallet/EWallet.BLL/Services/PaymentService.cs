using EWallet.BLL.Models;
using EWallet.BLL.Resources;
using EWallet.DAL.Entities;
using EWallet.DAL.Repositories;
using Microsoft.Extensions.Localization;

namespace EWallet.BLL.Services;

public class PaymentService : IPaymentService
{
    private readonly IUserRepository _users;
    private readonly IProviderRepository _providers;
    private readonly ITransactionRepository _transactions;
    private readonly IStringLocalizer<Messages> _l;

    public PaymentService(IUserRepository users, IProviderRepository providers,
        ITransactionRepository transactions, IStringLocalizer<Messages> l)
    {
        _users = users;
        _providers = providers;
        _transactions = transactions;
        _l = l;
    }

    public Task<List<Provider>> GetProvidersAsync() => _providers.GetAllAsync();

    public async Task<OperationResult> PayAsync(
        string userId, int providerId, string identifier, decimal amount)
    {
        amount = Math.Round(amount, 2);
        if (amount <= 0)
            return OperationResult.Fail(_l["InvalidAmount"]);

        var user = await _users.GetByIdAsync(userId);
        if (user == null)
            return OperationResult.Fail(_l["UnexpectedError"]);

        var provider = await _providers.GetByIdAsync(providerId);
        if (provider == null)
            return OperationResult.Fail(_l["ProviderNotFound"]);

        var account = await _providers.FindAccountAsync(providerId, identifier?.Trim() ?? string.Empty);
        if (account == null)
            return OperationResult.Fail(_l["ProviderAccountNotFound"]);

        if (user.Balance < amount)
            return OperationResult.Fail(_l["InsufficientFunds"]);
        
        if (amount > 1_000_000)
            return OperationResult.Fail(_l["AmountTooLarge"]);

        user.Balance -= amount;
        account.Balance += amount;

        await _transactions.AddAsync(new Transaction
        {
            Date = DateTime.UtcNow,
            Amount = amount,
            Type = "Payment",
            FromAccount = user.AccountNumber,
            ToAccount = $"{provider.Name}: {account.Identifier}"
        });

        await _users.SaveChangesAsync();

        return OperationResult.Ok(_l["PaymentSuccess"], user.Balance);
    }
}