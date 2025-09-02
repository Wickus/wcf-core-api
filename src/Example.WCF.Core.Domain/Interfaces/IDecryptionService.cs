namespace Example.WCF.Core.Domain.Interfaces;

public interface IDecryptionService
{
  public string DecryptBase64String(string encryptedBase64, string aesKeyBase64);
}
