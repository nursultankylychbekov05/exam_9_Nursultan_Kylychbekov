namespace EWallet.BLL.Models;

public class OperationResult
{
    public bool Success { get; init; }
    public string Message { get; init; } = string.Empty;
    public decimal? NewBalance { get; init; }

    public static OperationResult Ok(string message, decimal? newBalance = null) =>
        new() { Success = true, Message = message, NewBalance = newBalance };

    public static OperationResult Fail(string message) =>
        new() { Success = false, Message = message };
}