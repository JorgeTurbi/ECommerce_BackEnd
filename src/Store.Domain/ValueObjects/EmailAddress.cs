using Store.Domain.Errors;
using Store.Domain.Primitives;

namespace Store.Domain.ValueObjects;

/// <summary>
/// Representa un correo electronico validado dentro del dominio.
/// </summary>
public partial record EmailAddress
{
    private const int DefaultLength = 100;

    /// <summary>
    /// Valor normalizado del correo electronico.
    /// </summary>
    public string Value { get; init; }

    private EmailAddress(string value) => Value = value;

    /// <summary>
    /// Crea y valida una direccion de correo electronico.
    /// </summary>
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

    /// <summary>
    /// Determina si el correo posee un formato valido.
    /// </summary>
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
