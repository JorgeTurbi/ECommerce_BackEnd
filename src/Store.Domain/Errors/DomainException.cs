namespace Store.Domain.Errors;

/// <summary>
/// Excepcion lanzada cuando una regla del dominio es violada.
/// </summary>
public sealed class DomainException : Exception
{
    /// <summary>
    /// Error de dominio asociado a la excepcion.
    /// </summary>
    public Error Error { get; }

    /// <summary>
    /// Inicializa la excepcion a partir de un error de dominio.
    /// </summary>
    public DomainException(Error error) : base(error.Message)
    {
        Error = error;
    }
}
