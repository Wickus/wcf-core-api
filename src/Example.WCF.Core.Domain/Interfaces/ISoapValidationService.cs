using System.Xml.Linq;

namespace Example.WCF.Core.Domain.Interfaces;

public interface ISoapValidationService
{
	void Validate(XDocument soapMessage);
}
