using EWallet.BLL.Models;

namespace EWallet.BLL.Services;

public interface IWalletService
{
    Task<OperationResult> DepositAsync(string accountNumber, decimal amount);
}