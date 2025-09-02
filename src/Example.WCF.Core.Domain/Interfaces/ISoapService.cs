using System.Xml.Linq;

namespace Example.WCF.Core.Domain.Interfaces;

public interface ISoapService
{
  public Task<string> Process(string message);
}
