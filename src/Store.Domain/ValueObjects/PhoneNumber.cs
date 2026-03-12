
using System.Text.RegularExpressions;
using Store.Domain.Errors;
namespace Store.Domain.ValueObjects;


public partial record PhoneNumber
{
    private const int DefaultLength = 10;
    private const string Pattern = @"^(?:\+1[-.\s]?)?\(?(809|829|849)\)?[-.\s]?\d{3}[-.\s]?\d{4}$";


    // Constructor that initializes the Value property with the provided phone number string.
    private PhoneNumber(string value) => Value = value;

    public static PhoneNumber? Create(string value)
    {

        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException(PhoneError.Empty);

        var normalized = value.Trim();


        if (normalized.Length > DefaultLength)
            throw new DomainException(PhoneError.TooLong(DefaultLength));
        if (!System.Text.RegularExpressions.Regex.IsMatch(normalized, Pattern))
            throw new DomainException(PhoneError.InvalidFormat);

        return new PhoneNumber(normalized);


    }

    public string Value { get; init; }


    [GeneratedRegex(Pattern)]
    private static partial Regex PhoneRegex();

}