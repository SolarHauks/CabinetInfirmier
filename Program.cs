namespace CabinetInfirmier // Note: actual namespace depends on the project name.

{
    internal class Program
    {
        static void Main(string[] args)
        {
            // XMLUtils.ValidateXmlFile("http://www.univ-grenoble-alpes.fr/l3miage/medical", "../../../data/xsd/cabinet.xsd", "../../../data/xml/cabinet.xml");
            
            // Sert à forcer la résolution des ressources externes (fonction document() et les URI externes)
            AppContext.SetSwitch("Switch.System.Xml.AllowDefaultResolver", true);
            
            // XMLUtils.XslTransform("../../../data/xml/cabinet.xml", "../../../data/xslt/infirmiere.xslt", "../../../data/html/output.html");
            XMLUtils.XslTransform("../../../data/xml/cabinet.xml", "../../../data/xslt/extractionPatient.xslt", "../../../data/xml/Pourferlavésel.xml");
        }
    }
}