using System.Runtime.Loader;
using Example.WCF.Core.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Example.WCF.Core.Infrastructure;

public static class ServiceExtensions
{
	public static IServiceCollection AddInfrastructure(this IServiceCollection services)
	{
		string pluginPath = Path.Combine(AppContext.BaseDirectory, "plugins");

		if (!Directory.Exists(pluginPath))
		{
			Directory.CreateDirectory(pluginPath);
		}

		var dllFiles = Directory.GetFiles(pluginPath, "*.dll");

		if (!dllFiles.Any())
		{
			Console.WriteLine("⚠️ No plugins found.");
			return services;
		}

		foreach (var dll in dllFiles)
		{
			var assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(dll);

			var pluginTypes = assembly.GetTypes()
				.Where(t => typeof(IApiPlugin).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

			foreach (var type in pluginTypes)
			{
				Console.WriteLine($"🔌 Registering plugin: {type.FullName}");
				services.AddScoped(typeof(IApiPlugin), type);
			}
		}

		return services;
	}
}
