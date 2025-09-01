using System.Text;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace Example.WCF.Core.Api.Formatters;

public class SoapInputFormatter : TextInputFormatter
{
	public SoapInputFormatter()
	{
		SupportedMediaTypes.Add("application/soap+xml");
		SupportedEncodings.Add(Encoding.UTF8);
		SupportedEncodings.Add(Encoding.Unicode);
	}

	protected override bool CanReadType(Type type)
	{
		// You could restrict to certain types, but here we just allow string
		return type == typeof(string);
	}

	public override async Task<InputFormatterResult> ReadRequestBodyAsync(
		InputFormatterContext context, Encoding encoding)
	{
		using var reader = new StreamReader(context.HttpContext.Request.Body, encoding);
		var content = await reader.ReadToEndAsync();
		return await InputFormatterResult.SuccessAsync(content);
	}
}
