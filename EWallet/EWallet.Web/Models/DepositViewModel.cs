using System.ComponentModel.DataAnnotations;
using EWallet.BLL.Resources;

namespace EWallet.Web.Models;

public class DepositViewModel
{
    [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "AccountNumberRequired")]
    [Display(ResourceType = typeof(Messages), Name = "AccountNumber")]
    public string AccountNumber { get; set; } = string.Empty;

    [Display(ResourceType = typeof(Messages), Name = "Amount")]
    public decimal Amount { get; set; }
}