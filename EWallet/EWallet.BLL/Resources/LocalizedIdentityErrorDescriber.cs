using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;

namespace EWallet.BLL.Resources;

public class LocalizedIdentityErrorDescriber : IdentityErrorDescriber
{
    private readonly IStringLocalizer<Messages> _l;

    public LocalizedIdentityErrorDescriber(IStringLocalizer<Messages> l) => _l = l;

    private IdentityError Make(string code, string key, params object[] args) =>
        new() { Code = code, Description = string.Format(_l[key].Value, args) };

    public override IdentityError DefaultError() =>
        Make(nameof(DefaultError), "IdentityDefaultError");

    public override IdentityError ConcurrencyFailure() =>
        Make(nameof(ConcurrencyFailure), "IdentityConcurrencyFailure");

    public override IdentityError PasswordMismatch() =>
        Make(nameof(PasswordMismatch), "IdentityPasswordMismatch");

    public override IdentityError InvalidToken() =>
        Make(nameof(InvalidToken), "IdentityInvalidToken");

    public override IdentityError InvalidUserName(string? userName) =>
        Make(nameof(InvalidUserName), "IdentityInvalidUserName", userName ?? "");

    public override IdentityError InvalidEmail(string? email) =>
        Make(nameof(InvalidEmail), "IdentityInvalidEmail", email ?? "");

    public override IdentityError DuplicateUserName(string userName) =>
        Make(nameof(DuplicateUserName), "IdentityDuplicateUserName", userName);

    public override IdentityError DuplicateEmail(string email) =>
        Make(nameof(DuplicateEmail), "IdentityDuplicateEmail", email);

    public override IdentityError PasswordTooShort(int length) =>
        Make(nameof(PasswordTooShort), "IdentityPasswordTooShort", length);

    public override IdentityError PasswordRequiresNonAlphanumeric() =>
        Make(nameof(PasswordRequiresNonAlphanumeric), "IdentityPasswordRequiresNonAlphanumeric");

    public override IdentityError PasswordRequiresDigit() =>
        Make(nameof(PasswordRequiresDigit), "IdentityPasswordRequiresDigit");

    public override IdentityError PasswordRequiresLower() =>
        Make(nameof(PasswordRequiresLower), "IdentityPasswordRequiresLower");

    public override IdentityError PasswordRequiresUpper() =>
        Make(nameof(PasswordRequiresUpper), "IdentityPasswordRequiresUpper");

    public override IdentityError PasswordRequiresUniqueChars(int uniqueChars) =>
        Make(nameof(PasswordRequiresUniqueChars), "IdentityPasswordRequiresUniqueChars", uniqueChars);
}