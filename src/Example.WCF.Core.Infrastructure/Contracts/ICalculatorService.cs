using CoreWCF;

namespace Example.WCF.Core.Infrastructure.Contracts;

[ServiceContract]
public interface ICalculatorService
{
	[OperationContract]
	int Add(int x, int y);
}