using System.Xml.Linq;

namespace Example.WCF.Core.Domain.Interfaces.Soap;

public interface ISoapService
{
  public Task<string> Process(string message);
}
