using System.Xml.Linq;

using Example.WCF.Core.Domain.Interfaces;
using Example.WCF.Core.Tests.Fixtures;

using Microsoft.Extensions.DependencyInjection;

namespace Example.WCF.Core.Tests.InfrastructureServices;

[Collection("DI collection")]
public class SoapValidationServiceTest(GlobalFixture globalFixture)
{
  // Assemble
  private ISoapValidationService _soapValidationService = globalFixture.Services.GetRequiredService<ISoapValidationService>();

  [Fact]
  public async Task ShouldPassValidation()
  {
    // Assemble
    string encryptedSoapMessageFile = Path.Combine(AppContext.BaseDirectory, "TestData", "EncryptedSoapMessage.xml");

    if (!File.Exists(encryptedSoapMessageFile))
    {
      throw new FileNotFoundException($"File '{encryptedSoapMessageFile}' could not be found in the given context.");
    }

    string encryptedSoapMessage = await File.ReadAllTextAsync(encryptedSoapMessageFile);
    XDocument xDocument = XDocument.Parse(encryptedSoapMessage);

    // Act
    // Assert
    _soapValidationService.Validate(xDocument);
  }
}
