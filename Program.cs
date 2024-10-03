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
            // XMLUtils.XslTransform("../../../data/xml/cabinet.xml", "../../../data/xslt/extractionPatient.xslt", "../../../data/xml/Pourferlavésel.xml");
            
            Cabinet cabinet = new Cabinet();
            
            // cabinet.AnalyseGlobale("../../../data/xml/cabinet.xml");
            
            // List<string> infirmiers = cabinet.RecupereText("../../../data/xml/cabinet.xml");
            // foreach (string infirmier in infirmiers)
            // {
            //     Console.WriteLine(infirmier);
            // }
            
            // Console.WriteLine(cabinet.TotalActes("../../../data/xml/cabinet.xml"));
            
            // CheckHas checkHas = new CheckHas();
            // Console.WriteLine(checkHas.Count("patient")); // affiche le nombre de patient
            // Console.WriteLine(checkHas.hasAdresse("cabinet")); // affiche si tous les infirmiers ont une adresse
            // Console.WriteLine(checkHas.NSSValide("198082205545843", 1998, 08, 'M')); // affiche si le NSS est valide
            // Console.WriteLine(checkHas.AllNSSValide()); // affiche si tous les NSS sont valides
            
            // cabinet.AddNurse("Jean", "Némard"); // Ajout d'un nouvel infirmier
            
            // Adresse adresse = new Adresse(1, 1, "rue de la rue", 38000, "Grenoble");
            // cabinet.AddPatient("Jean", "Némard", "M", "1998-08-25", "198082205545843", adresse); // Ajout d'un nouveau patient
            
            cabinet.AddVisit("198082205545843", "001", ["001"], "2017-09-01");
        }
    }
}