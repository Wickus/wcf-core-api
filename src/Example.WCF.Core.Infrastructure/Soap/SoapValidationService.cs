using System.Xml.Linq;
using Example.WCF.Core.Domain.Interfaces;

namespace Example.WCF.Core.Infrastructure.Soap;

public class SoapValidationService : ISoapValidationService
{
	public void Validate(XDocument soapDocument)
	{
		XNamespace soapNs = "http://www.w3.org/2003/05/soap-envelope";
		XNamespace wsseNs = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd";
		XNamespace wsuNs = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd";

		XElement? header = soapDocument.Root?.Element(soapNs + "Header") ?? throw new SoapFaultException("env:Sender", @"Message could not be parsed, missing ""Header"" element");
		XElement? body = soapDocument.Root?.Element(soapNs + "Body") ?? throw new SoapFaultException("env:Sender", @"Message could not be parsed, missing ""Body"" element");
		XElement? security = header?.Element(wsseNs + "Security") ?? throw new SoapFaultException("env:Sender", @"Message could not be parsed, missing ""Security"" element");
		XElement? timestamp = security?.Element(wsuNs + "Timestamp") ?? throw new SoapFaultException("env:Sender", @"Message could not be parsed, missing ""Timestamp"" element");
		XElement? binarySecurityToken = security?.Element(wsseNs + "BinarySecurityToken") ?? throw new SoapFaultException("env:Sender", @"Message could not be parsed, missing ""BinarySecurityToken"" element");
		XElement? signature = security?.Element(wsseNs + "Signature") ?? throw new SoapFaultException("env:Sender", @"Message could not be parsed, missing ""Signature"" element");
	}
}
