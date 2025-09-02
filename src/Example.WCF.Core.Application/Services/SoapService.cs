using Example.WCF.Core.Domain.Interfaces;
using Example.WCF.Core.Infrastructure.Soap;

namespace Example.WCF.Core.Application.Services;

public class SoapService(IEnumerable<IApiPlugin> plugins): ISoapService
{
  private readonly IApiPlugin? _apiPlugin = plugins.FirstOrDefault();

  public async Task<string> Process(string message)
  {
    string responseXml = $@"<s:Envelope xmlns:s=""http://www.w3.org/2003/05/soap-envelope""></s:Envelope>";

    if (_apiPlugin is not null)
    {
      await _apiPlugin.HandleResponse(message);
    }

    responseXml =
  $@"<s:Envelope xmlns:s=""http://www.w3.org/2003/05/soap-envelope"">
    <s:Body>
        <HelloResponse xmlns=""http://tempuri.org/"">
            <Message>Hello from ASP.NET Core SOAP endpoint!</Message>
        </HelloResponse>
    </s:Body>
</s:Envelope>";

    return responseXml;
  }
}
