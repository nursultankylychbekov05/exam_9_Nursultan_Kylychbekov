using System.ComponentModel.DataAnnotations;
using EWallet.BLL.Resources;

namespace EWallet.Web.Models;

public class LoginViewModel
{
    [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "LoginRequired")]
    [Display(ResourceType = typeof(Messages), Name = "LoginField")]
    public string Login { get; set; } = string.Empty;

    [Required(ErrorMessageResourceType = typeof(Messages), ErrorMessageResourceName = "PasswordRequired")]
    [DataType(DataType.Password)]
    [Display(ResourceType = typeof(Messages), Name = "Password")]
    public string Password { get; set; } = string.Empty;

    [Display(ResourceType = typeof(Messages), Name = "RememberMe")]
    public bool RememberMe { get; set; }
}