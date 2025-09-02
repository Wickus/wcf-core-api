using System.Xml.Linq;

using Example.WCF.Core.Infrastructure.Constants;
using Example.WCF.Core.Infrastructure.Utils;
using Example.WCF.Core.Tests.Fixtures;

namespace Example.WCF.Core.Tests.InfrastructureServices;

[Collection("DI collection")]
public class ElementExtractionTest(GlobalFixture globalFixture)
{
  private readonly XDocument EncryptedSoapMessage = globalFixture.EncryptedSoapMessage;

  [Fact]
  public void ShouldExtractBodyElementAsync()
  {
    // Assemble
    XNamespace soapNs = SoapNamespace.Environment;
    XName expectedElementName = new XElement(soapNs + "Body").Name;

    // Act
    XElement bodyElement = EncryptedSoapMessage.ExtractBodyElement();

    // Assert
    Assert.Equal(expectedElementName, bodyElement.Name);
  }

  [Fact]
  public void ShouldExtractHeaderElementAsync()
  {
    // Assemble
    XNamespace soapNs = SoapNamespace.Environment;
    XName expectedElementName = new XElement(soapNs + "Header").Name;

    // Act
    XElement bodyElement = EncryptedSoapMessage.ExtractHeaderElement();

    // Assert
    Assert.Equal(expectedElementName, bodyElement.Name);
  }

  [Fact]
  public void ShouldExtractSecurityElementAsync()
  {
    // Assemble
    XNamespace wsseNs = SoapNamespace.WsseSecurity;
    XName expectedElementName = new XElement(wsseNs + "Security").Name;

    // Act
    XElement bodyElement = EncryptedSoapMessage.ExtractSecurityElement();

    // Assert
    Assert.Equal(expectedElementName, bodyElement.Name);
  }

  [Fact]
  public void ShouldExtractEncryptedKeyElementAsync()
  {
    // Assemble
    XNamespace encNs = SoapNamespace.Encryption;
    XName expectedElementName = new XElement(encNs + "EncryptedKey").Name;

    // Act
    XElement bodyElement = EncryptedSoapMessage.ExtractEncryptedKeyElement();

    // Assert
    Assert.Equal(expectedElementName, bodyElement.Name);
  }
}
