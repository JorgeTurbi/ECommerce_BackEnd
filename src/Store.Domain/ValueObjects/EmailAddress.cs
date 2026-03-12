using Store.Domain.Errors;
using Store.Domain.Primitives;
namespace Store.Domain.ValueObjects;

public partial record EmailAddress
{
    private const int DefaultLength = 100;
    public string Value { get; init; }

    private EmailAddress(string value) => Value = value;


    public static EmailAddress Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException(EmailAddressErrors.Empty);

        var normalized = value.Trim().ToLowerInvariant();

        if (normalized.Length > DefaultLength)
            throw new DomainException(EmailAddressErrors.TooLong(DefaultLength));

        if (!IsValidEmail(normalized))
            throw new DomainException(EmailAddressErrors.InvalidFormat);

        return new EmailAddress(normalized);
    }

    private static bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }
}



