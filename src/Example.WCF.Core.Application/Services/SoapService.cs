using System;
using Example.WCF.Core.Domain.Interfaces;

namespace Example.WCF.Core.Application.Services;

public class SoapService(IEnumerable<IApiPlugin> plugins) : ISoapService
{
	private readonly IApiPlugin? _apiPlugin = plugins.FirstOrDefault();

	public async Task Process(string message)
	{
		if (_apiPlugin is not null)
		{
			await _apiPlugin.HandleResponse(message);
		}

		await Task.Run(() => null);
	}
}
