namespace Example.WCF.Core.Domain.Interfaces;

public interface IApiPlugin
{
	Task HandleProviderResponse(int result);
}
