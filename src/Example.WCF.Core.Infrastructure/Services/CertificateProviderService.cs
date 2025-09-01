using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using Example.WCF.Core.Domain.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Example.WCF.Core.Infrastructure.Services;

public class CertificateProviderService(IConfiguration configuration) : ICertificateProviderService
{
	private readonly IConfiguration _configuration = configuration;

	public X509Certificate2 GetServiceCertificate()
	{
		if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
		{
			return LoadFromWindowsStore();
		}
		else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX) || RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
		{
			return LoadFromPfx();
		}
		else
		{
			throw new PlatformNotSupportedException("Unsupported OS for certificate loading.");
		}
	}

	private X509Certificate2 LoadFromWindowsStore()
	{
		string thumbprint = _configuration["CertificateSettings:Thumbprint"] ?? throw new Exception("Certificate thumbprint not set");

		using X509Store store = new(StoreName.My, StoreLocation.LocalMachine);
		store.Open(OpenFlags.ReadOnly);
		X509Certificate2 cert = store.Certificates
						.Find(X509FindType.FindByThumbprint, thumbprint, validOnly: false)
						.OfType<X509Certificate2>()
						.FirstOrDefault() ?? throw new Exception($"Service certificate with thumbprint {thumbprint} not found.");
		return cert;
	}

	private X509Certificate2 LoadFromPfx()
	{
		string pfxPath = _configuration["CertificateSettings:PfxPath"] ?? throw new Exception("PfxPath not set");
		string pfxPassword = _configuration["CertificateSettings:PfxPassword"] ?? throw new Exception("PfxPassword not set");

		if (string.IsNullOrEmpty(pfxPath))
			throw new ArgumentException("PFX path is not provided.");

		// Load the PFX file using X509CertificateLoader
		byte[] certData = File.ReadAllBytes(pfxPath);
		X509Certificate2 cert = X509CertificateLoader.LoadPkcs12(certData, pfxPassword, X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.Exportable);

		return cert;
	}
}
