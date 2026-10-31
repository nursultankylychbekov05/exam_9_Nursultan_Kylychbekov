using System.ComponentModel.DataAnnotations;
using EWallet.BLL.Resources;
using EWallet.DAL.Entities;

namespace EWallet.Web.Models;

public class PaymentViewModel
{
    [Range(1, int.MaxValue, ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "ProviderRequired")]
    [Display(ResourceType = typeof(Messages), Name = "Provider")]
    public int ProviderId { get; set; }

    [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "IdentifierRequired")]
    [Display(ResourceType = typeof(Messages), Name = "ProviderIdentifier")]
    public string Identifier { get; set; } = string.Empty;

    [Display(ResourceType = typeof(Messages), Name = "Amount")]
    public decimal Amount { get; set; }
    
    public List<Provider> Providers { get; set; } = new();
}