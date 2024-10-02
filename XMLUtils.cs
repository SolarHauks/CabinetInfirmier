namespace CabinetInfirmier;
using System.Xml;
using System.Xml.Schema;
using System.Xml.XPath;
using System.Xml.Xsl;

public class XMLUtils
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
    
    
    public static void XslTransform2 (string xmlFilePath, string xsltFilePath, string htmlFilePath) {
        XPathDocument xpathDoc = new XPathDocument(xmlFilePath);
        XslCompiledTransform xslt = new XslCompiledTransform();
        xslt.Load(xsltFilePath);
        XmlTextWriter htmlWriter = new XmlTextWriter(htmlFilePath, null);
        xslt.Transform(xpathDoc, null, htmlWriter);
    }
    
    public static void XslTransform(string xmlFilePath, string xsltFilePath, string htmlFilePath)
    {
        XPathDocument xpathDoc = new XPathDocument(xmlFilePath);
        XslCompiledTransform xslt = new XslCompiledTransform();
        XsltSettings settings = new XsltSettings(true, true);
        // settings.EnableDocumentFunction = true; // Enable the document() function

        // Use XmlResolver.ThrowingResolver to restrict the resources that the XSLT can access
        XmlResolver resolver = new XmlUrlResolver();

        xslt.Load(xsltFilePath, settings, resolver);
        using (XmlTextWriter htmlWriter = new XmlTextWriter(htmlFilePath, null))
        {
            xslt.Transform(xpathDoc, null, htmlWriter);
        }
    }
}