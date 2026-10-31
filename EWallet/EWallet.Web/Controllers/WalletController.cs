using EWallet.BLL.Models;
using EWallet.BLL.Resources;
using EWallet.BLL.Services;
using EWallet.DAL.Entities;
using EWallet.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace EWallet.Web.Controllers;

public class WalletController : Controller
{
    private readonly IWalletService _wallet;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IStringLocalizer<Messages> _l;

    public WalletController(
        IWalletService wallet,
        UserManager<ApplicationUser> userManager,
        IStringLocalizer<Messages> l)
    {
        _wallet = wallet;
        _userManager = userManager;
        _l = l;
    }

    [HttpPost]
    public async Task<IActionResult> Deposit(DepositViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var error = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .FirstOrDefault() ?? _l["UnexpectedError"].Value;
            return Json(OperationResult.Fail(error));
        }

        var result = await _wallet.DepositAsync(model.AccountNumber, model.Amount);
        if (!result.Success)
            return Json(result);
        
        var ownAccount = false;
        if (User.Identity?.IsAuthenticated == true)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            ownAccount = currentUser?.AccountNumber == model.AccountNumber.Trim();
        }

        return Json(ownAccount
            ? result
            : OperationResult.Ok(result.Message));
    }
}