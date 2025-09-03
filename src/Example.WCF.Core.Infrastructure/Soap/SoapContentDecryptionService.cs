
using System.Xml.Linq;

using Example.WCF.Core.Domain.Interfaces;
using Example.WCF.Core.Domain.Interfaces.Soap;
using Example.WCF.Core.Infrastructure.Utils;

namespace Example.WCF.Core.Infrastructure.Soap;

public class SoapContentDecryptionService(IDecryptionService decryptionService): ISoapContentDecryptionService
{
  private readonly IDecryptionService _decryptionService = decryptionService;

  public async Task<string> DecryptBodyContent(XDocument soapMessage)
  {
    XElement bodyElement = soapMessage.ExtractBodyElement();
    XElement securityElement = soapMessage.ExtractSecurityElement();
    string encryptedAesKey = securityElement.ExtractEncryptedAesKeyValue();
    string encryptedValue = bodyElement.ExtractEncryptedValue();
    string decryptedData = _decryptionService.DecryptBase64String(encryptedValue, encryptedAesKey);

    return await Task.Run(() => decryptedData);
  }
}
