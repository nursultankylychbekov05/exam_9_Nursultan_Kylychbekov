namespace EWallet.DAL.Entities;

public class Provider
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<ProviderAccount> Accounts { get; set; } = new();
}