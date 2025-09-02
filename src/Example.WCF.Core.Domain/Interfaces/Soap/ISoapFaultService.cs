using System.Xml.Linq;

using Example.WCF.Core.Domain.Dto;

namespace Example.WCF.Core.Domain.Interfaces.Soap;

public interface ISoapFaultService
{
  public string CreateSoapFault(SoapFault soapFault);
}
