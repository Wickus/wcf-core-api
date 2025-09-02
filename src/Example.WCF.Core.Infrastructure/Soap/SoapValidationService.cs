using System.Xml.Linq;

using Example.WCF.Core.Domain.Interfaces;

namespace Example.WCF.Core.Infrastructure.Soap;

public class SoapValidationService(ICertificateProviderService certificateProviderService): ISoapValidationService
{
  private readonly ICertificateProviderService _certificateProviderService = certificateProviderService;
  private readonly string _soapNs = "http://www.w3.org/2003/05/soap-envelope";
  private readonly string _wsseNs = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd";
  private readonly string _wsuNs = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd";
  private readonly string _dsNs = "http://www.w3.org/2000/09/xmldsig#";

  public void Validate(XDocument soapDocument)
  {
    XNamespace soapNs = _soapNs;
    XNamespace wsseNs = _wsseNs;
    XNamespace wsuNs = _wsuNs;
    XNamespace dsNs = _dsNs;

    XElement? header = soapDocument.Root?.Element(soapNs + "Header") ?? throw new SoapFaultException("env:Sender", @"Message could not be parsed, missing ""Header"" element");
    XElement? security = header?.Element(wsseNs + "Security") ?? throw new SoapFaultException("env:Sender", @"Message could not be parsed, missing ""Security"" element");
    _ = soapDocument.Root?.Element(soapNs + "Body") ?? throw new SoapFaultException("env:Sender", @"Message could not be parsed, missing ""Body"" element");
    _ = security?.Element(wsuNs + "Timestamp") ?? throw new SoapFaultException("env:Sender", @"Message could not be parsed, missing ""Timestamp"" element");
    _ = security?.Element(wsseNs + "BinarySecurityToken") ?? throw new SoapFaultException("env:Sender", @"Message could not be parsed, missing ""BinarySecurityToken"" element");
    _ = security?.Element(dsNs + "Signature") ?? throw new SoapFaultException("env:Sender", @"Message could not be parsed, missing ""Signature"" element");
  }

  public void ValidateSignature(XDocument decryptedSoapMessage)
  {
  }
}
