using System.Xml.Linq;

using Example.WCF.Core.Infrastructure.Constants;
using Example.WCF.Core.Infrastructure.Soap;

namespace Example.WCF.Core.Infrastructure.Utils;

public static class ElementExtractionService
{
  public static XElement ExtractBodyElement(this XDocument soapDocument)
  {
    XNamespace soapNs = SoapNamespace.Environment;

    return soapDocument?.Root?.Element(soapNs + "Body") ??
      throw new SoapFaultException("env:Sender", @"Message could not be parsed, missing ""Body"" element"); ;
  }

  public static XElement ExtractHeaderElement(this XDocument soapDocument)
  {
    XNamespace soapNs = SoapNamespace.Environment;

    return soapDocument?.Root?.Element(soapNs + "Header") ??
      throw new SoapFaultException("env:Sender", @"Message could not be parsed, missing ""Header"" element");
  }

  public static XElement ExtractSecurityElement(this XDocument soapDocument)
  {
    XNamespace wsseNs = SoapNamespace.WsseSecurity;
    XElement headerElement = soapDocument.ExtractHeaderElement();

    return headerElement.Element(wsseNs + "Security") ??
      throw new SoapFaultException("env:Sender", @"Message could not be parsed, missing ""Security"" element");
  }

  public static XElement ExtractEncryptedKeyElement(this XDocument soapDocument)
  {
    XNamespace encNs = SoapNamespace.Encryption;
    XElement securityElement = soapDocument.ExtractSecurityElement();

    return securityElement.Element(encNs + "EncryptedKey") ??
      throw new SoapFaultException("env:Sender", @"Message could not be parsed, missing ""EncryptedKey"" element");
  }
}
