using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace Example.WCF.Core.Domain.Interfaces;

public interface ICertificateProviderService
{
  public X509Certificate2 GetServiceCertificate();
  public RSA? GetPrivateKey();
  public RSA? GetPublicKey();
}
