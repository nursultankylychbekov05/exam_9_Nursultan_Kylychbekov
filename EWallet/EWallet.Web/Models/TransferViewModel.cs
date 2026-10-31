using System.ComponentModel.DataAnnotations;
using EWallet.BLL.Resources;

namespace EWallet.Web.Models;

public class TransferViewModel
{
    [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "RecipientRequired")]
    [Display(ResourceType = typeof(Messages), Name = "RecipientAccount")]
    public string RecipientAccount { get; set; } = string.Empty;

    [Display(ResourceType = typeof(Messages), Name = "Amount")]
    public decimal Amount { get; set; }
}