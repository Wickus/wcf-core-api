using Example.WCF.Core.Domain.Interfaces;
using Example.WCF.Core.Infrastructure.Contracts;
using Microsoft.Extensions.Logging;

namespace Example.WCF.Core.Application;

public class CalculatorService : ICalculatorService
{
	private readonly IEnumerable<IApiPlugin> _plugin;
	public CalculatorService(IEnumerable<IApiPlugin> plugin)
	{
		_plugin = plugin;
	}
	public int Add(int x, int y)
	{
		try
		{
			_plugin.FirstOrDefault()?.HandleProviderResponse(x + y);
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex.ToString());
		}

		return x + y;
	}
}
