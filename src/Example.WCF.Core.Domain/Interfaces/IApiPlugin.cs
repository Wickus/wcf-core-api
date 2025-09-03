namespace Example.WCF.Core.Domain.Interfaces;

public interface IApiPlugin
{
  public Task HandleResponse(object providerResponseMessage);
}
