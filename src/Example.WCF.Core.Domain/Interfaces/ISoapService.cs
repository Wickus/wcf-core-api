using System.Xml.Linq;

namespace Example.WCF.Core.Domain.Interfaces;

public interface ISoapService
{
	Task<string> Process(string message);
}
