using EWallet.BLL.Models;
using EWallet.BLL.Resources;
using EWallet.BLL.Services;
using EWallet.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using EWallet.DAL.Entities;

namespace EWallet.Web.Controllers;

[Authorize]
public class TransferController : Controller
{
    private readonly ITransferService _transfer;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IStringLocalizer<Messages> _l;

    public TransferController(ITransferService transfer,
        UserManager<ApplicationUser> userManager, IStringLocalizer<Messages> l)
    {
        _transfer = transfer;
        _userManager = userManager;
        _l = l;
    }

    [HttpGet]
    public IActionResult Index() => View(new TransferViewModel());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(TransferViewModel model)
    {
        if (ModelState.TryGetValue(nameof(model.Amount), out var amountState)
            && amountState.Errors.Count > 0)
            return Json(OperationResult.Fail(_l["InvalidAmount"]));

        if (!ModelState.IsValid)
        {
            var error = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .FirstOrDefault() ?? _l["UnexpectedError"].Value;
            return Json(OperationResult.Fail(error));
        }

        var userId = _userManager.GetUserId(User)!;
        var result = await _transfer.TransferAsync(userId, model.RecipientAccount, model.Amount);
        return Json(result);
    }
}