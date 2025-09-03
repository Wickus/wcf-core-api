using System.Text.Json;

using Example.WCF.Core.Domain.Interfaces;

namespace Example.WCF.Core.PluginTemplate;

public class Plugin: IApiPlugin
{
  public async Task HandleResponse(object providerResponseMessage)
  {
    // TODO: Add logic to handle the response based on your business logic.

    #region Save response template
    string messageType = providerResponseMessage.GetType().Name;

    switch (messageType)
    {
      case string t when t.Contains("IDX"):
        string json = JsonSerializer.Serialize(providerResponseMessage);
        string responseFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Response.json");
        File.WriteAllText(responseFilePath, json);
        break;
      default:
        throw new Exception("Could not find message type");
    }
    #endregion

    await Task.Run(() => Task.CompletedTask);
  }
}
