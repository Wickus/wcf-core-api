using System.Xml.Linq;

namespace Example.WCF.Core.Domain.Interfaces.Soap;

public interface ISoapValidationService
{
  public void Validate(XDocument soapMessage);
  public void ValidateSignature(XDocument decryptedSoapMessage);
}
