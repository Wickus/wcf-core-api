
using System.Xml.Serialization;

namespace Example.WCF.Core.Domain.Contracts;


[XmlRoot("Body", Namespace = "http://SecureX.ProviderSubmitService/V1")]
[XmlType(TypeName = "IDXProviderSubmitMessage", Namespace = "http://IDX.Contract/V1")]
public class IDXProviderSubmitMessage
{
  [XmlElement("AccountName", Namespace = "http://IDX.Contract/V1")]
  public string AccountName { get; set; } = string.Empty;

  [XmlElement("AccountNumber", Namespace = "http://IDX.Contract/V1")]
  public string AccountNumber { get; set; } = string.Empty;

  [XmlElement("AccountType", Namespace = "http://IDX.Contract/V1")]
  public string AccountType { get; set; } = string.Empty;

  [XmlElement("Data", Namespace = "http://IDX.Contract/V1")]
  public Data Data { get; set; } = new();
}

public class Data
{
  [XmlElement("StatementData", Namespace = "http://IDX.Contract/V1")]
  public List<StatementData> StatementData { get; set; } = [];
}

public class StatementData
{
  [XmlElement("DateFrom", Namespace = "http://IDX.Contract/V1")]
  public DateTime DateFrom { get; set; }
  [XmlElement("DateTo", Namespace = "http://IDX.Contract/V1")]
  public DateTime DateTo { get; set; }
  [XmlElement("Transactions", Namespace = "http://IDX.Contract/V1")]
  public List<Transaction> Transactions { get; set; } = [];
}

public class Transaction
{
  [XmlElement("Balance", Namespace = "http://IDX.Contract/V1")]
  public decimal Balance { get; set; }
  [XmlElement("Description", Namespace = "http://IDX.Contract/V1")]
  public string Description { get; set; } = string.Empty;
  [XmlElement("PaidIn", Namespace = "http://IDX.Contract/V1")]
  public decimal PaidIn { get; set; }
  [XmlElement("PaidOut", Namespace = "http://IDX.Contract/V1")]
  public decimal PaidOut { get; set; }
  [XmlElement("TransactionCode", Namespace = "http://IDX.Contract/V1")]
  public Guid TransactionCode { get; set; }
  [XmlElement("TransactionDate", Namespace = "http://IDX.Contract/V1")]
  public DateTime TransactionDate { get; set; }
}
