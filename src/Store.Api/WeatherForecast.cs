namespace Store.Api;

/// <summary>
/// Modelo de ejemplo generado por la plantilla inicial de ASP.NET Core.
/// </summary>
public class WeatherForecast
{
    /// <summary>
    /// Fecha asociada al pronostico.
    /// </summary>
    public DateOnly Date { get; set; }

    /// <summary>
    /// Temperatura expresada en grados Celsius.
    /// </summary>
    public int TemperatureC { get; set; }

    /// <summary>
    /// Temperatura expresada en grados Fahrenheit.
    /// </summary>
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    /// <summary>
    /// Resumen textual del clima.
    /// </summary>
    public string? Summary { get; set; }
}
