using EWallet.BLL.Models;

namespace EWallet.Web.Models;

public class HistoryViewModel
{
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
    public List<TransactionItem> Items { get; set; } = new();
}