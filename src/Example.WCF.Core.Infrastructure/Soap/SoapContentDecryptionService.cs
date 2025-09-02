
using System.Xml.Linq;

using Example.WCF.Core.Domain.Interfaces.Soap;
using Example.WCF.Core.Infrastructure.Utils;

namespace Example.WCF.Core.Infrastructure.Soap;

public class SoapContentDecryptionService: ISoapContentDecryptionService
{
  public async Task<string> DecryptBodyContent(XDocument soapMessage)
  {
    XElement? bodyElement = soapMessage.ExtractBodyElement();
    XElement? encryptedKeyElement = soapMessage.ExtractEncryptedKeyElement();
    string? encryptedAesKey = encryptedKeyElement?.ExtractEncryptedAesKeyValue();
    string? encryptedValue = bodyElement?.ExtractEncryptedValue();

    return await Task.Run(() => encryptedValue ?? string.Empty);
  }
}
