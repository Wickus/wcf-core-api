using System.Security.Cryptography;
using System.Text;

using Example.WCF.Core.Domain.Interfaces;
using Example.WCF.Core.Infrastructure.Soap;

namespace Example.WCF.Core.Infrastructure.Services;

public class DecryptionService(ICertificateProviderService certificateProviderService): IDecryptionService
{
  private readonly ICertificateProviderService _certificateProviderService = certificateProviderService;

  private byte[] DecryptAesKeyWithPrivateKey(byte[] encryptedAesKey)
  {
    RSA? privateKey = _certificateProviderService.GetPrivateKey() ?? throw new SoapFaultException("env:Receiver", "Private key could not be found.");
    return privateKey.Decrypt(encryptedAesKey, RSAEncryptionPadding.Pkcs1);
  }

  private static string DecryptDataWithAes(byte[] encryptedData, byte[] aesKey)
  {
    using Aes aes = Aes.Create();
    aes.Key = aesKey;

    aes.IV = [.. encryptedData.Take(16)];

    byte[] actualEncryptedData = [.. encryptedData.Skip(16)];

    aes.Mode = CipherMode.CBC;
    aes.Padding = PaddingMode.ISO10126;

    using ICryptoTransform decryptor = aes.CreateDecryptor();
    byte[] decryptedBytes = decryptor.TransformFinalBlock(actualEncryptedData, 0, actualEncryptedData.Length);
    return Encoding.UTF8.GetString(decryptedBytes).Trim();
  }

  public string DecryptBase64String(string encryptedBase64, string aesKeyBase64)
  {
    byte[] encryptedBytes = Convert.FromBase64String(encryptedBase64);
    byte[] aesKey = DecryptAesKeyWithPrivateKey(Convert.FromBase64String(aesKeyBase64));

    return DecryptDataWithAes(encryptedBytes, aesKey);
  }
}
