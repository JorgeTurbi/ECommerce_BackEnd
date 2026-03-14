# Configuración y Arranque

## Propósito
Este documento describe cómo encajan los archivos de configuración de la solución actual y qué papel juegan dentro de la arquitectura `Api -> Application -> Domain -> Infrastructure`.

## Archivos principales
- `src/Store.Api/appsettings.json`
  Archivo base de configuración compartido por todos los entornos. Hoy solo define logging y `AllowedHosts`, pero aquí deberían vivir también cadenas de conexión, configuración de Wolverine, autenticación y opciones de servicios externos.
- `src/Store.Api/appsettings.Development.json`
  Sobrescribe la configuración base para desarrollo local. Es el lugar correcto para valores específicos de desarrollo que no deben aplicarse en otros entornos.
- `src/Store.Api/Store.Api.http`
  Archivo pensado para pruebas manuales rápidas desde el IDE. Hoy mantiene el endpoint de ejemplo `weatherforecast`; más adelante conviene reemplazarlo por requests reales de `Customers`, `Products`, `Orders` y demás slices.
- `src/Store.Api/Program.cs`
  Punto de composición de la aplicación. Aquí se registran servicios, middleware y endpoints. Debe mantenerse delgado y sin lógica de negocio.

## Relación entre proyectos
- `src/Store.Api/Store.Api.csproj`
  Proyecto web y punto de entrada HTTP.
- `src/Store.Application/Store.Application.csproj`
  Contiene casos de uso, handlers y validaciones. Usa Wolverine y FluentValidation.
- `src/Store.Infrastructure/Store.Infrastructure.csproj`
  Debe contener implementaciones de persistencia y servicios externos.
- `src/Store.Domain/Store.Domain.csproj`
  Contiene entidades, value objects y reglas del negocio.

## Notas arquitectónicas
- `appsettings.json` y `appsettings.Development.json` no aceptan comentarios JSON válidos en producción, por eso la explicación se deja en este documento en lugar de incrustarla dentro del archivo.
- `Store.Api.http` forma parte de la experiencia de desarrollo, no de la lógica de negocio.
- La dependencia de `WolverineFx` dentro de `Store.Domain.csproj` es una señal a revisar más adelante porque el dominio debería mantenerse lo más aislado posible de detalles de infraestructura o mensajería.
- Cuando agregues módulos reales del ecommerce, conviene reflejar su configuración en secciones nombradas, por ejemplo `ConnectionStrings`, `Authentication`, `Wolverine`, `Payments`, `Storage`.

## Siguiente evolución recomendada
- Agregar en `appsettings.json` una sección `ConnectionStrings`.
- Reemplazar `Store.Api.http` por requests reales de `Customers`.
- Registrar `AddApplication()` y `AddInfrastructure()` en `Program.cs` cuando cierres la composición de dependencias.
