using Store.Domain.Errors;

namespace Store.Domain.Primitives;

/// <summary>
/// Contiene errores comunes relacionados con montos monetarios.
/// </summary>
public static class MoneyErrors
{
    /// <summary>
    /// Error usado cuando se intenta trabajar con montos negativos.
    /// </summary>
    public static readonly Error NegativeAmount =
        new("money.negative_amount", "El monto no puede ser negativo.");
}
