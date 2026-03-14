using System.Reflection;

namespace Store.Application;

/// <summary>
/// Punto de referencia para localizar el ensamblado de la capa Application.
/// </summary>
public class ApplicationAssemblyReference
{
    /// <summary>
    /// Ensamblado actual utilizado para descubrimiento de handlers y validadores.
    /// </summary>
    internal static readonly Assembly Assembly = typeof(ApplicationAssemblyReference).Assembly;
}
