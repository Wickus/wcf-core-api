namespace Example.WCF.Core.Domain.Interfaces;

public interface IApiPlugin
{
  public Task HandleResponse(string result);
}
