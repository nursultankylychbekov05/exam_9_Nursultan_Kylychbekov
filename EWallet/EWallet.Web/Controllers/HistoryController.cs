using EWallet.BLL.Services;
using EWallet.DAL.Entities;
using EWallet.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EWallet.Web.Controllers;

[Authorize]
public class HistoryController : Controller
{
    private readonly ITransactionService _service;
    private readonly UserManager<ApplicationUser> _userManager;

    public HistoryController(ITransactionService service, UserManager<ApplicationUser> userManager)
    {
        _service = service;
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> Index(DateTime? from, DateTime? to)
    {
        var userId = _userManager.GetUserId(User)!;
        var model = new HistoryViewModel
        {
            From = from,
            To = to,
            Items = await _service.GetHistoryAsync(userId, from, to)
        };
        return View(model);
    }
}