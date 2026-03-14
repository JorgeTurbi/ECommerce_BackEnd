using JasperFx.CodeGeneration;
using Store.Domain.Enums;
using Store.Domain.Errors;

namespace Store.Domain.ValueObjects;

/// <summary>
/// Representa una identificacion fiscal o personal validada.
/// </summary>
public partial record Identify
{
    private const string CedulaPattern = @"^\d{3}-\d{7}-\d{1}$";
    private const string RncPattern = @"^\d{9}$";
    private const int MaxLengthCedula = 11;
    private const int MaxLengthRnc = 9;

    /// <summary>
    /// Valor normalizado de la identificacion.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Tipo identificado a partir del valor recibido.
    /// </summary>
    public IdentificationType Type { get; }

    /// <summary>
    /// Indica si el valor corresponde a una cedula.
    /// </summary>
    public bool IsCedula => Type == IdentificationType.Cedula;

    /// <summary>
    /// Indica si el valor corresponde a un RNC.
    /// </summary>
    public bool IsRnc => Type == IdentificationType.RNC;

    private Identify(string value, IdentificationType type)
    {
        Value = value;
        Type = type;
    }

    /// <summary>
    /// Crea y valida una identificacion soportada por el sistema.
    /// </summary>
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
