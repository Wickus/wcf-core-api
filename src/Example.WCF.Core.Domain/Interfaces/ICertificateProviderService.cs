using System.Security.Cryptography.X509Certificates;

namespace Example.WCF.Core.Domain.Interfaces;

public interface ICertificateProviderService
{
	X509Certificate2 GetServiceCertificate();
}
