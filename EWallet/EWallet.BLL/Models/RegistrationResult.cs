using EWallet.DAL.Entities;

namespace EWallet.BLL.Models;

public class RegistrationResult
{
    public bool Success { get; init; }
    public List<string> Errors { get; init; } = new();
    public ApplicationUser? User { get; init; }
}