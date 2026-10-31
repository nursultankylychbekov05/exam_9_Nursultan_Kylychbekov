using Microsoft.AspNetCore.Identity;

namespace EWallet.DAL.Entities;

public class ApplicationUser : IdentityUser
{
    public string AccountNumber { get; set; } = string.Empty;
    public decimal Balance { get; set; } = 100000;
}