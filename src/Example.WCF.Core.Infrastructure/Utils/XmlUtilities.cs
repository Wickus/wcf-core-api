using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

using Example.WCF.Core.Infrastructure.Soap;

namespace Example.WCF.Core.Infrastructure.Utils;

public static class XmlUtilities
{
  public static object Deserialize(this XDocument xDocument, Type messageType)
  {
    if (xDocument == null || xDocument.Root == null)
      throw new SoapFaultException("env:Receiver", $@"XDocument is empty or has no root element to deserialize to type ""{messageType.Name}"".");

    XmlSerializer serializer = new(messageType);
    XElement cleanedXml = xDocument.Root;

    using XmlReader reader = cleanedXml.CreateReader();

    try
    {
      return serializer.Deserialize(reader)!;
    }
    catch (InvalidOperationException ex)
    {
      throw new SoapFaultException("env:Receiver", $@"The XML could not be deserialized to type ""{messageType.Name}"" {ex.Message}.");
    }
  }

  public static string GetMessageType(this XElement xElement)
  {
    XAttribute typeAttr = xElement.Attribute(XName.Get("type", "http://www.w3.org/2001/XMLSchema-instance")) ??
      throw new SoapFaultException("env:Sender", @"Message could not be processed, can not find ""Message Type""");

    string rawType = typeAttr.Value;
    string typeLocalName = rawType.Contains(':') ? rawType.Split(':')[1] : rawType;

    return typeLocalName;
  }

  private static XElement RemoveAllNamespaces(XElement xmlElement)
  {
    if (!xmlElement.HasElements)
    {
      XElement xElement = new XElement(xmlElement.Name.LocalName)
      {
        Value = xmlElement.Value
      };

      foreach (var attribute in xmlElement.Attributes()
                                          .Where(a => !a.IsNamespaceDeclaration))
      {
        xElement.Add(new XAttribute(attribute.Name.LocalName, attribute.Value));
      }

      return xElement;
    }

    return new XElement(xmlElement.Name.LocalName,
        xmlElement.Elements().Select(e => RemoveAllNamespaces(e)));
  }
}
