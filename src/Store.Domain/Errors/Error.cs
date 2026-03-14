namespace Store.Domain.Errors;

/// <summary>
/// Representa un error de dominio con codigo y mensaje legible.
/// </summary>
public sealed record Error(string Code, string Message);
