using EWallet.BLL.Models;
using EWallet.DAL.Entities;

namespace EWallet.BLL.Services;

public interface IPaymentService
{
    Task<List<Provider>> GetProvidersAsync();
    Task<OperationResult> PayAsync(string userId, int providerId, string identifier, decimal amount);
}