using System.Xml;

namespace CabinetInfirmier;

public class Cabinet
{
    private string nom;
    private Adresse adresse;

    public void AnalyseGlobale(string filepath)
    {
        var settings = new XmlReaderSettings();
        using (var reader = XmlReader.Create(filepath, settings)) {
            reader.MoveToContent();
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Document:
                        // instructions à exécuter au début du document
                        Console.Write("Entering the document ");
                        break;
                    case XmlNodeType.Element:
                        // instructions à executer quand on entre dans un élément
                        Console.WriteLine("Starts the element {0}, which have {1} attributs", reader.Name, reader.AttributeCount);
                        break;
                    case XmlNodeType.EndElement:
                        // instructions à executer quand on sort d’un élément
                        Console.WriteLine("Ends the element {0}", reader.Name);
                        break;
                    case XmlNodeType.Text:
                        // instructions à executer quand on trouve du texte
                        Console.WriteLine("Text node value = {0}", reader.Value);
                        break;
                    case XmlNodeType.Attribute:
                        // instructions à executer quand on trouve un attribut
                        Console.WriteLine("Attribute {0} : Content {1}", reader.Name, reader.Value);
                        break;
                    default:
                        // instructions à executer sinon
                        if (reader.NodeType != XmlNodeType.Whitespace)
                            Console.WriteLine("Other node of type {0} with value {1}", reader.NodeType, reader.Value);
                        break;
                }
            }
        }
    }
    
    // Renvoit la liste des noms des infirmiers
    public List<string> RecupereText(string filepath)
    {
        List<string> infirmiers = new List<string>();
        var settings = new XmlReaderSettings();
        using (var reader = XmlReader.Create(filepath, settings)) {
            reader.MoveToContent();
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        // instructions à executer quand on entre dans un élément
                        if (reader.Name == "ca:infirmier")
                        {
                            reader.Read(); // Skip the @id attribut tag
                            reader.Read(); // Skip the Whitespace tag
                            reader.Read(); // Skip the nom element tag
                            infirmiers.Add(reader.Value);
                        }
                        break;
                }
            }
        }
        return infirmiers;
    }
    
    // Renvoit le nombre d'actes différents qui devront etre effectuer, tous patients confondus
    public int TotalActes(string filepath)
    {
        List<int> actes = new List<int>();
        int totalActes = 0;
        var settings = new XmlReaderSettings();
        using (var reader = XmlReader.Create(filepath, settings)) {
            reader.MoveToContent();
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        // instructions à executer quand on entre dans un élément
                        if (reader.Name == "ca:acte")
                        {
                            if (!actes.Contains(int.Parse(reader.GetAttribute("id"))))
                            {
                                actes.Add(int.Parse(reader.GetAttribute("id")));
                                totalActes++;
                            }
                        }
                        break;
                }
            }
        }
        return totalActes;
    }
    
}