namespace EWallet.DAL.Entities;

public class ProviderAccount
{
    public int Id { get; set; }
    public int ProviderId { get; set; }
    public Provider Provider { get; set; } = null!;
    public string Identifier { get; set; } = string.Empty;
    public decimal Balance { get; set; }
}