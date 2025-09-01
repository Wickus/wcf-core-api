using System.Xml.Linq;
using Example.WCF.Core.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Example.WCF.Core.Api.Controllers
{
	[Route("api/response")]
	[ApiController]
	public class ResponseController(ISoapService soapService) : ControllerBase
	{
		private readonly ISoapService _soapService = soapService;

		[HttpPost]
		[Consumes("application/soap+xml")]
		[Produces("application/soap+xml")]
		public IActionResult HandleSoap([FromBody] string soapMessage)
		{
			// 🔹 Parse the incoming SOAP request
			var doc = XDocument.Parse(soapMessage);

			// Extract the SOAP Body
			var soapBody = doc.Root?
				.Element(XName.Get("Body", "http://www.w3.org/2003/05/soap-envelope"));

			if (soapBody is not null)
			{
				// TODO: validate WS-Security headers (signatures, timestamp, etc.)
				
				_soapService.Process(soapBody.ToString());
			}

			// Example: generate SOAP response
			var responseXml =
$@"<s:Envelope xmlns:s=""http://www.w3.org/2003/05/soap-envelope"">
    <s:Body>
        <HelloResponse xmlns=""http://tempuri.org/"">
            <Message>Hello from ASP.NET Core SOAP endpoint!</Message>
        </HelloResponse>
    </s:Body>
</s:Envelope>";

			return Content(responseXml, "application/soap+xml");
		}
	}
}
