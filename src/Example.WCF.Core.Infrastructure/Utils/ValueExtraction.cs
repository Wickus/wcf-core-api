using System.Xml.Linq;

using Example.WCF.Core.Infrastructure.Constants;
using Example.WCF.Core.Infrastructure.Soap;

namespace Example.WCF.Core.Infrastructure.Utils;

public static class ValueExtraction
{
  public static string ExtractEncryptedValue(this XElement element)
  {
    XNamespace encNs = SoapNamespace.Encryption;

    XElement encryptedData = element.Element(encNs + "EncryptedData") ??
      throw new SoapFaultException("env:Sender", $@"Message could not be decrypted. Could not locate ""EncryptedData"" element inside {element.Name.LocalName}.");

    return encryptedData.ExtractCipherValue();
  }

  public static string ExtractEncryptedAesKeyValue(this XElement element)
  {
    XNamespace encNs = SoapNamespace.Encryption;

    XElement encryptedKey = element.Element(encNs + "EncryptedKey") ??
      throw new SoapFaultException("env:Sender", $@"Message could not be decrypted. Could not locate ""EncryptedKey"" element inside {element.Name.LocalName}.");

    return encryptedKey.ExtractCipherValue();
  }

  private static string ExtractCipherValue(this XElement element)
  {
    XNamespace encNs = SoapNamespace.Encryption;

    XElement? cipherDataElement = element.Element(encNs + "CipherData") ??
      throw new SoapFaultException("env:Sender", $@"Message could not be decrypted. Could not locate ""CipherData"" element inside {element.Name.LocalName}.");

    XElement? cipherValueElement = (cipherDataElement?.Element(encNs + "CipherValue")) ??
      throw new SoapFaultException("env:Sender", @"Message could not be decrypted. Could not locate ""CipherValue"" element inside CipherData.");

    string? cypherValue = (cipherValueElement?.Value) ??
      throw new SoapFaultException("env:Sender", @"Message could not be decrypted. ""CipherValue"" is empty.");

    return cypherValue;
  }
}
