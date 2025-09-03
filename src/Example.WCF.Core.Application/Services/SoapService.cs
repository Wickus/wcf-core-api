using System.Xml.Linq;

using Example.WCF.Core.Domain.Interfaces;
using Example.WCF.Core.Domain.Interfaces.Soap;
using Example.WCF.Core.Infrastructure.Soap;
using Example.WCF.Core.Infrastructure.Utils;

using Microsoft.Extensions.Configuration;

namespace Example.WCF.Core.Application.Services;

public class SoapService(IEnumerable<IApiPlugin> plugins, ISoapContentDecryptionService soapContentDecryptionService, IConfiguration configuration): ISoapService
{
  private readonly ISoapContentDecryptionService _soapContentDecryptionService = soapContentDecryptionService;
  private readonly IConfiguration _configuration = configuration;
  private readonly IApiPlugin? _apiPlugin = plugins.FirstOrDefault();

  public async Task<string> Process(string message)
  {
    XDocument soapMessage = XDocument.Parse(message);

    string decryptedMessage = await _soapContentDecryptionService.DecryptBodyContent(soapMessage);
    XDocument decryptedXml = XDocument.Parse(decryptedMessage);

    string messageType = decryptedXml.Root?.GetMessageType()
                     ?? throw new SoapFaultException("env:Sender", "Message type is missing.");

    Type? type = AppDomain.CurrentDomain
      .GetAssemblies()
      .SelectMany(a => a.GetTypes())
      .FirstOrDefault(t => t.Name == messageType) ??
        throw new SoapFaultException("env:Sender", $"Message could not be processed, type {messageType} does not exists in the given context.");

    object providerResponse = decryptedXml.Deserialize(type);

    if (_apiPlugin is not null)
    {
      await _apiPlugin.HandleResponse(providerResponse);
    }

    return GenerateResponse();
  }

  private string GenerateResponse()
  {
    Guid correlationId = Guid.NewGuid();
    string soapActionBase = _configuration["ServiceModel:SoapActionBase"] ?? string.Empty;

    return $@"
      <s:Envelope xmlns:s=""http://www.w3.org/2003/05/soap-envelope"">
        <s:Header>
          <ActivityId CorrelationId=""{correlationId}"" xmlns=""http://schemas.microsoft.com/2004/09/ServiceModel/Diagnostics"">00000000-0000-0000-0000-000000000000</ActivityId>
        </s:Header>
        <s:Body>
          <Value xmlns=""{soapActionBase}"">true</Value>
        </s:Body>
      </s:Envelope>
    ".Trim();
  }
}
