namespace Example.WCF.Core.Domain.Interfaces;

public interface IApiPlugin
{
	Task HandleResponse(string result);
}
