using EWallet.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace EWallet.DAL.Context;

public static class Seeder
{
    public static async Task SeedAsync(ApplicationDbContext db)
    {
        if (await db.Providers.AnyAsync()) return;

        var providers = new List<Provider>
        {
            new()
            {
                Name = "Beeline",
                Accounts =
                {
                    new ProviderAccount { Identifier = "+996700111111", Balance = 150 },
                    new ProviderAccount { Identifier = "+996700222222", Balance = 0 },
                    new ProviderAccount { Identifier = "+996700333333", Balance = 520 }
                }
            },
            new()
            {
                Name = "O!",
                Accounts =
                {
                    new ProviderAccount { Identifier = "+996500111111", Balance = 80 },
                    new ProviderAccount { Identifier = "+996500222222", Balance = 300 }
                }
            },
            new()
            {
                Name = "Megacom",
                Accounts =
                {
                    new ProviderAccount { Identifier = "+996555111111", Balance = 0 },
                    new ProviderAccount { Identifier = "+996555222222", Balance = 45 }
                }
            },
            new()
            {
                Name = "Домашний интернет",
                Accounts =
                {
                    new ProviderAccount { Identifier = "100001", Balance = 0 },
                    new ProviderAccount { Identifier = "100002", Balance = 1200 },
                    new ProviderAccount { Identifier = "100003", Balance = 60 }
                }
            }
        };

        db.Providers.AddRange(providers);
        await db.SaveChangesAsync();
    }
}