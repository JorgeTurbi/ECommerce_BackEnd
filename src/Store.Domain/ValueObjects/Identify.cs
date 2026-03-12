using JasperFx.CodeGeneration;
using Store.Domain.Enums;
using Store.Domain.Errors;
namespace Store.Domain.ValueObjects;


public partial record Identify
{
    private const string CedulaPattern = @"^\d{3}-\d{7}-\d{1}$";
    private const string RncPattern = @"^\d{9}$";
    private const int MaxLengthCedula = 11;
    private const int MaxLengthRnc = 9;

    public string Value { get; }
    public IdentificationType Type { get; }

    public bool IsCedula => Type == IdentificationType.Cedula;
    public bool IsRnc => Type == IdentificationType.RNC;
    private Identify(string value, IdentificationType type)
    {
        Value = value;
        Type = type;
    }


    public static Identify? Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException(IdentificationError.Empty);
        if (value.Length == 0)
            throw new DomainException(IdentificationError.Empty);

        var normalized = value.Trim();
        if (!normalized.All(char.IsDigit))
            throw new DomainException(IdentificationError.InvalidCharacters);

        if (normalized.Length < MaxLengthCedula && normalized.Length > MaxLengthRnc)
            throw new DomainException(IdentificationError.InvalidLength);

        if (normalized.Length > MaxLengthCedula && normalized.Length > MaxLengthRnc)
            throw new DomainException(IdentificationError.InvalidLength);

        if (normalized.Length == MaxLengthCedula && !System.Text.RegularExpressions.Regex.IsMatch(normalized, CedulaPattern))
            throw new DomainException(IdentificationError.InvalidCedula);
        if (normalized.Length == MaxLengthRnc && !System.Text.RegularExpressions.Regex.IsMatch(normalized, RncPattern))
            throw new DomainException(IdentificationError.InvalidRnc);

        if (normalized.Length < MaxLengthRnc)
            throw new DomainException(IdentificationError.InvalidLength);


        if (normalized.Length == MaxLengthCedula)
            new Identify(normalized, IdentificationType.Cedula);
        if (normalized.Length == MaxLengthRnc)
            return new Identify(normalized, IdentificationType.RNC);

        return new Identify(normalized, IdentificationType.Cedula);

    }

}

