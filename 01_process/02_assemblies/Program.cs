

#region AppDomain / static loading

//using System.Reflection;

//AppDomain domain = AppDomain.CurrentDomain;

//Console.WriteLine($"{domain.FriendlyName}/{domain.BaseDirectory}");

//foreach (Assembly a in domain.GetAssemblies())
//    Console.WriteLine($"{a.GetName().Name}\t{a.GetName().Version}");

#endregion

#region Dynamic loading

using System.Reflection;
using System.Runtime.Loader;

RenderAssembliesList("BEFORE LOADING");

AssemblyLoadContext ctx = new AssemblyLoadContext("lib_ctx", true);
Assembly assembly = ctx.LoadFromAssemblyPath(Path.Combine(Directory.GetCurrentDirectory(), "MathLib.dll"));
ctx.Unloading += ctx => Console.WriteLine("ASSEMBLY UNLOADED!!!");

RenderAssembliesList("AFTER LOADING");

Type? type = assembly.GetType("MathLib.Calculator");

// static call
//MethodInfo? method = type?.GetMethod("Factorial");
//int? result = (int?)method.Invoke(assembly, new object[] { 5 });
//Console.WriteLine($"Factorial = {result}");

// non-static call
MethodInfo? method = type?.GetMethod("Sum");
object? calc = Activator.CreateInstance(type);
int? result = (int?)method.Invoke(calc, new object[] { 5, 4 });
Console.WriteLine($"Sum = {result}");

ctx.Unload();
GC.Collect();
RenderAssembliesList("AFTER UNLOADING");

void RenderAssembliesList(string message)
{
    Console.WriteLine($"\n==========={message}===========");

    AppDomain domain = AppDomain.CurrentDomain;

    Console.WriteLine($"{domain.FriendlyName}/{domain.BaseDirectory}");

    foreach (Assembly a in domain.GetAssemblies())
        Console.WriteLine($"{a.GetName().Name}\t{a.GetName().Version}");
}

#endregion
