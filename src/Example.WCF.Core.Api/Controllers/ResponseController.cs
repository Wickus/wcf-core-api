using System.Xml.Linq;

using Example.WCF.Core.Domain.Dto;
using Example.WCF.Core.Domain.Interfaces;
using Example.WCF.Core.Infrastructure.Soap;

using Microsoft.AspNetCore.Mvc;

namespace Example.WCF.Core.Api.Controllers
{
  [Route("api/response")]
  [ApiController]
  public class ResponseController(
    ISoapService soapService,
    ISoapFaultService soapFaultService,
    ISoapValidationService soapValidationService,
    IDecryptionService decryptionService): ControllerBase
  {
    private readonly ISoapService _soapService = soapService;
    private readonly ISoapFaultService _soapFaultService = soapFaultService;
    private readonly ISoapValidationService _soapValidationService = soapValidationService;
    private readonly IDecryptionService _decryptionService = decryptionService;

    [HttpPost]
    [Consumes("application/soap+xml")]
    [Produces("application/soap+xml")]
    public async Task<IActionResult> HandleSoap([FromBody] string soapMessage)
    {
      try
      {
        XDocument soapDocument = XDocument.Parse(soapMessage);

        _soapValidationService.Validate(soapDocument);

        string responseContent = await _soapService.Process(soapDocument.ToString());

        return Content(responseContent, "application/soap+xml", System.Text.Encoding.UTF8);
      }
      catch (SoapFaultException ex)
      {
        return CreateSoapFaultResult(ex.FaultCode, ex.GetType().Name, $"{ex.Message} {ex.StackTrace}");
      }
      catch (Exception ex)
      {
        return CreateSoapFaultResult("env:Receiver", ex.GetType().Name, $"{ex.Message} {ex.StackTrace}");
      }
    }

    private ContentResult CreateSoapFaultResult(string code, string type, string message)
    {
      return new ContentResult
      {
        StatusCode = 500,
        ContentType = "application/soap+xml; charset=utf-8",
        Content = _soapFaultService.CreateSoapFault(new SoapFault(code, type, message))
      };
    }
  }
}
