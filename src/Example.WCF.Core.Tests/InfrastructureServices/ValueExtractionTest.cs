using System.Xml.Linq;

using Example.WCF.Core.Infrastructure.Utils;
using Example.WCF.Core.Tests.Fixtures;

namespace Example.WCF.Core.Tests.InfrastructureServices;

[Collection("DI collection")]
public class ValueExtractionTest(GlobalFixture globalFixture)
{
  // Assemble
  private readonly XDocument EncryptedDocument = globalFixture.EncryptedSoapMessage;

  [Fact]
  public void ShouldReturnEncryptedValue()
  {
    // Act
    string encryptedData = EncryptedDocument.ExtractBodyElement().ExtractEncryptedValue();

    // Assert
    Assert.NotNull(encryptedData);
    Assert.NotEmpty(encryptedData);
    Assert.True(globalFixture.IsBase64(encryptedData));
  }

}
