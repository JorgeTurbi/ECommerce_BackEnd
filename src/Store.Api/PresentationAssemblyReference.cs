
using System.Reflection;

namespace Store.Api;

/// <summary>
/// Referencia de ensamblado para la presentación de la API.
/// </summary>
///     
public class PresentationAssemblyReference
{
    internal static readonly Assembly AssemblyType = typeof(PresentationAssemblyReference).Assembly;
}