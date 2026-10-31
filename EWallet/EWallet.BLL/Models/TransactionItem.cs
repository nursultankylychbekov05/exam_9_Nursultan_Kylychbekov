namespace EWallet.BLL.Models;

public class TransactionItem
{
    public DateTime Date { get; init; }
    public string Type { get; init; } = string.Empty;   
    public decimal Amount { get; init; }               
    public string Counterparty { get; init; } = string.Empty;
}