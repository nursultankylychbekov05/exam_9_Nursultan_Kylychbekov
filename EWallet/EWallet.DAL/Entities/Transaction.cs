namespace EWallet.DAL.Entities;

public class Transaction
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public string Type { get; set; } = string.Empty;  
    public string? FromAccount { get; set; }
    public string? ToAccount { get; set; }
}