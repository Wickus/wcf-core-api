using System.Xml.Linq;

using Example.WCF.Core.Domain.Interfaces;
using Example.WCF.Core.Domain.Interfaces.Soap;
using Example.WCF.Core.Infrastructure.Services;
using Example.WCF.Core.Infrastructure.Soap;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Example.WCF.Core.Tests.Fixtures;

public class GlobalFixture
{
  public ServiceProvider Services { get; }
  public XDocument EncryptedSoapMessage { get; }
  public XDocument InvalidSoapMessage { get; }

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

    EncryptedSoapMessage = XDocument.Load(Path.Combine(AppContext.BaseDirectory, "TestData", "EncryptedSoapMessage.xml"));
    InvalidSoapMessage = XDocument.Load(Path.Combine(AppContext.BaseDirectory, "TestData", "InvalidSoapMessage.xml"));

    Services = services.BuildServiceProvider();
  }

  public bool IsBase64(string value)
  {
    Span<byte> buffer = new(new byte[value.Length]);
    return Convert.TryFromBase64String(value, buffer, out _);
  }
}
