using System.Runtime.Loader;
using Example.WCF.Core.Domain.Interfaces;
using Example.WCF.Core.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Example.WCF.Core.Infrastructure;

/// <summary>
/// Provides extension methods for configuring application services.
/// </summary>
public static class ServiceExtensions
{
	/// <summary>
	/// Adds infrastructure services by dynamically loading plugins from the "plugins" folder.
	/// </summary>
	/// <param name="services">The IServiceCollection used for dependency injection.</param>
	/// <returns>The updated IServiceCollection with plugin services registered.</returns>
	public static IServiceCollection AddInfrastructure(this IServiceCollection services)
	{
		#region Plugin Injection Logic
		// Define the folder path where plugins are expected to be located
		string pluginPath = Path.Combine(AppContext.BaseDirectory, "plugins");

		// Ensure the plugins directory exists; create it if missing
		if (!Directory.Exists(pluginPath))
		{
			Directory.CreateDirectory(pluginPath);
		}

		// Look for all DLL files in the plugins folder
		string[] dllFiles = Directory.GetFiles(pluginPath, "*.dll");

		// If no DLLs are found, log a warning and return without registering anything
		if (dllFiles.Length == 0)
		{
			Console.WriteLine("⚠️ No plugins found.");
			return services;
		}

		// Iterate over each DLL file and load its assembly
		foreach (string dll in dllFiles)
		{
			System.Reflection.Assembly assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(dll);

			// Find all non-abstract, non-interface types that implement IApiPlugin
			IEnumerable<Type> pluginTypes = assembly.GetTypes()
				.Where(t => typeof(IApiPlugin).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

			// Register each discovered plugin type into the Dependency Injection container
			foreach (Type type in pluginTypes)
			{
				Console.WriteLine($"🔌 Registering plugin: {type.FullName}");
				services.AddScoped(typeof(IApiPlugin), type);
			}
		}
		#endregion

		services.AddScoped<ICertificateProviderService, CertificateProviderService>();
		
		// Return the updated service collection for chaining
		return services;
	}
}
