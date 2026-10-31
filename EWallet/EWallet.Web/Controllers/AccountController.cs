using EWallet.BLL.Resources;
using EWallet.BLL.Services;
using EWallet.DAL.Entities;
using EWallet.Web.Models;
using EWallet.BLL.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace EWallet.Web.Controllers;

public class AccountController : Controller
{
    private readonly SignInManager<ApplicationUser> _signIn;
    private readonly IAccountService _account;
    private readonly IStringLocalizer<Messages> _l;

    public AccountController(SignInManager<ApplicationUser> signIn,
        IAccountService account, IStringLocalizer<Messages> l)
    {
        _signIn = signIn;
        _account = account;
        _l = l;
    }

    [HttpGet]
    public IActionResult Register() => View(new RegisterViewModel());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        var result = await _account.RegisterAsync(model.Email, model.Password);
        if (!result.Success)
        {
            foreach (var error in result.Errors)
                ModelState.AddModelError(string.Empty, error);
            return View(model);
        }

        await _signIn.SignInAsync(result.User!, isPersistent: false);
        TempData["Toast"] = string.Format(_l["RegistrationSuccess"].Value, result.User!.AccountNumber);
        return RedirectToAction("Index", "Home");
    }

    [HttpGet]
    public IActionResult Login(string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View(new LoginViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        if (!ModelState.IsValid) return View(model);

        var user = await _account.FindByLoginAsync(model.Login);
        if (user != null)
        {
            var result = await _signIn.PasswordSignInAsync(
                user, model.Password, model.RememberMe, lockoutOnFailure: false);

            if (result.Succeeded)
                return !string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl)
                    ? Redirect(returnUrl)
                    : RedirectToAction("Index", "Home");
        }

        ModelState.AddModelError(string.Empty, _l["InvalidCredentials"]);
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Logout()
    {
        await _signIn.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}