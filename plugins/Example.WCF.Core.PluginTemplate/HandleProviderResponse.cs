using Example.WCF.Core.Domain.Interfaces;

namespace Example.WCF.Core.PluginTemplate;

public class Plugin : IApiPlugin
{
	public async Task HandleResponse(string result)
	{
		Console.WriteLine("The result is: " + result);
		await Task.Run(() => null);
	}
}
