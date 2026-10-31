using EWallet.BLL.Models;

namespace EWallet.BLL.Services;

public interface ITransferService
{
    Task<OperationResult> TransferAsync(string senderUserId, string recipientAccount, decimal amount);
}