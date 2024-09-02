using System.Reflection;

namespace Retailer.Service;

public static class MediatorAssembly
{
    public static Assembly Assembly { get; } = typeof(MediatorAssembly).Assembly;
}
