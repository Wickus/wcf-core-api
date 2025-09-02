using Example.WCF.Core.Domain.Interfaces;
using Example.WCF.Core.Tests.Fixtures;

using Microsoft.Extensions.DependencyInjection;

namespace Example.WCF.Core.Tests.InfrastructureServices;

[Collection("DI collection")]
public class DecryptionServiceTest(GlobalFixture globalFixture)
{
  private readonly IDecryptionService _decryptionService = globalFixture.Services.GetRequiredService<IDecryptionService>();

  [Theory]
  [InlineData(
    "N8r1x9lG1SVwQtux1SzuOEgocZ3aazm47x47WN5Qt5tSOUW5kW1NYegQcwsmJHqIRSP13Pkq+pCVjEb5nYN8Z6s56N7m+daasY81TneLmOjC5IohFbtpwB1knJkcpu8LfcVlxGDapJPyTbl3u/9tT/UAEWe9t3axPX1y+NDjujwf25/fyS3cIwPi9a+SXWEOvyafYUfNofjqhvHrY3n3lwnN1xG2wO5XoTQeD/hPxyjW6cU6cwhXq7jkZlJk+pxkg45L5wtNFXhKLWc7rW/qVVXiZYjaLtgXjFITwNiULiUgdtRhSxa0uIBTWCyFiUleglVN9q1qNDR0THa59t2l8g==",
    "uU4sQVHvGmiz84erYS2GmZ3GwKxPE5YmoMrniItKJ8pqzst+ezEtW+HZrsYZQJeF",
    @"<Body></Body>")]
  public void ReturnCorrectDecryptedData(string encryptedAESKey, string encryptedData, string decryptedData)
  {
    // Act
    string decryptedResult = _decryptionService.DecryptBase64String(encryptedBase64: encryptedData, aesKeyBase64: encryptedAESKey);

    // Assert
    Assert.Equal(decryptedData, decryptedResult);
  }
}
