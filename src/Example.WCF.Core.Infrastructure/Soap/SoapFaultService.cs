using System.Xml.Linq;

using Example.WCF.Core.Domain.Dto;
using Example.WCF.Core.Domain.Interfaces;

namespace Example.WCF.Core.Infrastructure.Soap.Services;

public class SoapFaultService: ISoapFaultService
{
  public string CreateSoapFault(SoapFault soapFault)
  {
    XNamespace soapNameSpace = "http://www.w3.org/2003/05/soap-envelope";

    var fault = new XElement(soapNameSpace + "Fault",
      new XElement(soapNameSpace + "Code",
        new XElement(soapNameSpace + "Value", soapFault.Code)
      ),
      new XElement(soapNameSpace + "Reason",
        new XElement(soapNameSpace + "Text",
          new XAttribute(XNamespace.Xml + "lang", "en"),
          soapFault.Reason
        )
      )
    );

    if (!string.IsNullOrEmpty(soapFault.Detail))
    {
      fault.Add(
        new XElement(soapNameSpace + "Detail",
          new XElement("MessageDetail", soapFault.Detail)
        )
      );
    }

    return new XDocument(
      new XElement(soapNameSpace + "Envelope",
        new XAttribute(XNamespace.Xmlns + "env", soapNameSpace),
        new XElement(soapNameSpace + "Body", fault)
      )
    ).ToString();
  }
}
