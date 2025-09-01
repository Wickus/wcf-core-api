namespace Example.WCF.Core.Domain.Interfaces;

public interface ISoapService
{
	Task Process(string message);
}
