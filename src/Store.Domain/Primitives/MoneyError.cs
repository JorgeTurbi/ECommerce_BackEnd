

using Store.Domain.Errors;

namespace Store.Domain.Primitives;

public static class MoneyErrors
{
    public static readonly Error NegativeAmount =
        new("money.negative_amount", "El monto no puede ser negativo.");


}