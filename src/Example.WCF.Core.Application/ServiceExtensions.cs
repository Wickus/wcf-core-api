using Example.WCF.Core.Application.Services;
using Example.WCF.Core.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Example.WCF.Core.Application;

public static class ServiceExtensions
{
	public static IServiceCollection AddApplication(this IServiceCollection services)
	{
		services.AddScoped<ISoapService, SoapService>();

		return services;
	}
}
