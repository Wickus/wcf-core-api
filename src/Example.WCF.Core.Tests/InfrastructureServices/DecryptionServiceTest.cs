using System.Xml.Linq;

using Example.WCF.Core.Domain.Interfaces;
using Example.WCF.Core.Infrastructure.Utils;
using Example.WCF.Core.Tests.Fixtures;

using Microsoft.Extensions.DependencyInjection;

namespace Example.WCF.Core.Tests.InfrastructureServices;

[Collection("DI collection")]
public class DecryptionServiceTest(GlobalFixture globalFixture)
{
  private readonly IDecryptionService _decryptionService = globalFixture.Services.GetRequiredService<IDecryptionService>();
  private readonly XDocument EncryptedSoapMessage = globalFixture.EncryptedSoapMessage;

  [Fact]
  public void ReturnCorrectDecryptedData()
  {
    // Assemble
    string encryptedAesKey = EncryptedSoapMessage.ExtractSecurityElement().ExtractEncryptedAesKeyValue();
    string encryptedData = EncryptedSoapMessage.ExtractBodyElement().ExtractEncryptedValue();

    // Act
    string decryptedResult = _decryptionService.DecryptBase64String(encryptedBase64: encryptedData, aesKeyBase64: encryptedAesKey);

    // Assert
    XDocument.Parse(decryptedResult);
  }
}
