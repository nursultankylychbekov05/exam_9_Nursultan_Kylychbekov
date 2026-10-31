using System.Text.RegularExpressions;
using EWallet.BLL.Models;
using EWallet.BLL.Resources;
using EWallet.DAL.Entities;
using EWallet.DAL.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace EWallet.BLL.Services;

public class AccountService : IAccountService
{
    private static readonly Regex AccountNumberRegex = new(@"^\d{6}$");

    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUserRepository _users;
    private readonly IStringLocalizer<Messages> _l;

    public AccountService(UserManager<ApplicationUser> userManager,
        IUserRepository users, IStringLocalizer<Messages> l)
    {
        _userManager = userManager;
        _users = users;
        _l = l;
    }

    public async Task<RegistrationResult> RegisterAsync(string email, string password)
    {
        email = email.Trim();

        if (await _userManager.FindByEmailAsync(email) != null)
            return new RegistrationResult { Errors = { _l["EmailTaken"].Value } };

        var user = new ApplicationUser
        {
            UserName = email,
            Email = email,
            AccountNumber = await GenerateAccountNumberAsync()
            // Balance = 100000 берётся из значения по умолчанию в сущности
        };

        var result = await _userManager.CreateAsync(user, password);
        if (!result.Succeeded)
            return new RegistrationResult
            {
                Errors = result.Errors.Select(e => e.Description).ToList()
            };

        return new RegistrationResult { Success = true, User = user };
    }

    public async Task<ApplicationUser?> FindByLoginAsync(string login)
    {
        login = login.Trim();

        return AccountNumberRegex.IsMatch(login)
            ? await _users.GetByAccountNumberAsync(login)
            : await _userManager.FindByEmailAsync(login);
    }

    private async Task<string> GenerateAccountNumberAsync()
    {
        string number;
        do
        {
            number = Random.Shared.Next(100000, 1000000).ToString();
        } while (await _users.AccountNumberExistsAsync(number));

        return number;
    }
}