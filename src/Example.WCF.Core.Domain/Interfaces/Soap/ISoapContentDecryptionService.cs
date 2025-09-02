using System.Xml.Linq;

namespace Example.WCF.Core.Domain.Interfaces.Soap;

public interface ISoapContentDecryptionService
{
  public Task<string> DecryptBodyContent(XDocument soapMessage);
}
