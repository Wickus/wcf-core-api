using System.Xml.Linq;

namespace Example.WCF.Core.Domain.Interfaces;

public interface ISoapValidationService
{
  public void Validate(XDocument soapMessage);
  public void ValidateSignature(XDocument decryptedSoapMessage);
}
