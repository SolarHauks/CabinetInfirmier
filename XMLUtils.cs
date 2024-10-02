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
    
    
    public static void XslTransform(string xmlFilePath, string xsltFilePath, string htmlFilePath)
    {
        XPathDocument xpathDoc = new XPathDocument(xmlFilePath);
        XslCompiledTransform xslt = new XslCompiledTransform();
        XsltSettings settings = new XsltSettings(true, true);
        
        // Permet de forcer la résolution des ressources externes (fonction document() et les URI externes)
        XmlResolver resolver = new XmlUrlResolver();

        XsltArgumentList argList = new XsltArgumentList();
        // Dans le cas où le fichier XSLT est infirmiere.xslt, on passe en paramètre le numéro de l'infirmerie
        if (xsltFilePath.EndsWith("infirmiere.xslt"))
        {
            // Passe à la transformation le numéro de l'infirmerie en paramètre
            argList.AddParam("destinedId", "", "001");
        } else if (xsltFilePath.EndsWith("extractionPatient.xslt"))
        {
            argList.AddParam("destinedName", "", "Pourferlavésel");
        }

        xslt.Load(xsltFilePath, settings, resolver);
        using (XmlTextWriter htmlWriter = new XmlTextWriter(htmlFilePath, null))
        {
            xslt.Transform(xpathDoc, argList, htmlWriter);
        }
    }
}