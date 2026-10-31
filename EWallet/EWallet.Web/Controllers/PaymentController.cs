using EWallet.BLL.Models;
using EWallet.BLL.Resources;
using EWallet.BLL.Services;
using EWallet.DAL.Entities;
using EWallet.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace EWallet.Web.Controllers;

[Authorize]
public class PaymentController : Controller
{
    private readonly IPaymentService _payment;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IStringLocalizer<Messages> _l;

    public PaymentController(IPaymentService payment,
        UserManager<ApplicationUser> userManager, IStringLocalizer<Messages> l)
    {
        _payment = payment;
        _userManager = userManager;
        _l = l;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        return View(new PaymentViewModel { Providers = await _payment.GetProvidersAsync() });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(PaymentViewModel model)
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
        var result = await _payment.PayAsync(userId, model.ProviderId, model.Identifier, model.Amount);
        return Json(result);
    }
}