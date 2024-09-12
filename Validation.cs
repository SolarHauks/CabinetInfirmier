namespace CabinetInfirmier;
using System.Xml;
using System.Xml.Schema;

public class Validation
{
    public static Task ValidateXmlFile (string schemaNamespace, string xsdFilePath, string xmlFilePath) {
        var settings = new XmlReaderSettings();
        settings.Schemas.Add (schemaNamespace, xsdFilePath);
        settings.ValidationType = ValidationType.Schema;
        Console.WriteLine("Nombre de schemas utilisés dans la validation : " + settings.Schemas.Count);
        settings.ValidationEventHandler += ValidationCallBack;
        var readItems = XmlReader.Create(xmlFilePath, settings);
        while (readItems.Read()) { }
        return Task.CompletedTask;
    }


    private static void ValidationCallBack(object? sender, ValidationEventArgs e) {
        if (e.Severity.Equals(XmlSeverityType.Warning)) {
            Console.Write("WARNING: ");
            Console.WriteLine(e.Message);
        }
        else if (e.Severity.Equals(XmlSeverityType.Error)) {
            Console.Write("ERROR: ");
            Console.WriteLine(e.Message);
        }
    }
}