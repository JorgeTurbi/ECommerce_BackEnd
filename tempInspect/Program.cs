using System;
using System.Linq;
using System.Reflection;
using Wolverine;

var assembly = typeof(IMessageBus).Assembly;
foreach (var typeName in new[] { "ISender", "IMessageBus", "IMessageRouter", "IEnvelope" })
{
    var type = assembly.GetTypes().FirstOrDefault(t => t.Name == typeName);
    Console.WriteLine($"Found {typeName}: {type?.FullName}");
    if (type is null) continue;

    var methods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance);
    foreach (var method in methods)
    {
        var parameters = string.Join(", ", method.GetParameters().Select(p => $"{p.ParameterType.Name} {p.Name}"));
        Console.WriteLine($"  {method.ReturnType.Name} {method.Name}({parameters})");
    }
    Console.WriteLine();
}
