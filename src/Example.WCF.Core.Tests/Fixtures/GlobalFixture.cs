using Example.WCF.Core.Domain.Interfaces;
using Example.WCF.Core.Infrastructure.Services;
using Example.WCF.Core.Infrastructure.Soap;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Example.WCF.Core.Tests.Fixtures;

public class GlobalFixture
{
  public ServiceProvider Services { get; }

  public GlobalFixture()
  {
    IServiceCollection services = new ServiceCollection();
    IConfiguration config = new ConfigurationBuilder()
      .SetBasePath(AppContext.BaseDirectory)
      .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
      .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true)
      .Build();

    services.AddSingleton<IConfiguration>(config);
    services.AddSingleton<ICertificateProviderService, CertificateProviderService>();
    services.AddSingleton<IDecryptionService, DecryptionService>();
    services.AddSingleton<ISoapValidationService, SoapValidationService>();

    Services = services.BuildServiceProvider();
  }
}
