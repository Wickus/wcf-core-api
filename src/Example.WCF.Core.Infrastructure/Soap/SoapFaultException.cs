namespace Example.WCF.Core.Infrastructure.Soap;

public class SoapFaultException(string faultCode, string message): Exception(message)
{
  public string FaultCode { get; } = faultCode;
}
