using System.ComponentModel.DataAnnotations;
using EWallet.BLL.Resources;

namespace EWallet.Web.Models;

public class RegisterViewModel
{
    [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "EmailRequired")]
    [EmailAddress(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "EmailInvalid")]
    [Display(ResourceType = typeof(Messages), Name = "Email")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PasswordRequired")]
    [DataType(DataType.Password)]
    [Display(ResourceType = typeof(Messages), Name = "Password")]
    public string Password { get; set; } = string.Empty;

    [Compare(nameof(Password), ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PasswordsDoNotMatch")]
    [DataType(DataType.Password)]
    [Display(ResourceType = typeof(Messages), Name = "ConfirmPassword")]
    public string ConfirmPassword { get; set; } = string.Empty;
}