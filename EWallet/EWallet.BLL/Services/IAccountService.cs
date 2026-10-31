using EWallet.BLL.Models;
using EWallet.DAL.Entities;

namespace EWallet.BLL.Services;

public interface IAccountService
{
    Task<RegistrationResult> RegisterAsync(string email, string password);
    Task<ApplicationUser?> FindByLoginAsync(string login);
}