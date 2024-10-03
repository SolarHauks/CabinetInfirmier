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
    
    // Ajoute un nouvel infirmier
    public void AddNurse(string firstName, string lastName)
    {
        // Chargement du document xml
        XmlDocument doc = new XmlDocument();
        string filePath = "../../../data/xml/cabinet.xml";
        doc.Load(filePath);

        // Gestion du namespace
        XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
        nsmgr.AddNamespace("ca", "http://www.univ-grenoble-alpes.fr/l3miage/medical");

        // Récupération de la liste des infirmiers
        XmlNodeList nurseNodes = doc.SelectNodes("//ca:infirmier", nsmgr);
        int maxId = 0;

        // Recherche de l'id le plus grand
        foreach (XmlNode nurseNode in nurseNodes)
        {
            int id = int.Parse(nurseNode.Attributes["id"].Value);
            if (id > maxId)
            {
                maxId = id;
            }
        }

        int newId = maxId + 1; // Nouvel id
        string photoName = $"{firstName.ToLower()}.png"; // Nom de la photo

        // Création du nouvel infirmier
        XmlElement newNurse = doc.CreateElement("ca:infirmier", nsmgr.LookupNamespace("ca"));
        newNurse.SetAttribute("id", newId.ToString("D3")); // Ajout de l'id

        // Ajout du nom
        XmlElement lastnameElement = doc.CreateElement("ca:nom", nsmgr.LookupNamespace("ca"));
        lastnameElement.InnerText = lastName;
        newNurse.AppendChild(lastnameElement);
        
        // Ajout du prenom
        XmlElement firstnameElement = doc.CreateElement("ca:prénom", nsmgr.LookupNamespace("ca"));
        firstnameElement.InnerText = firstName;
        newNurse.AppendChild(firstnameElement);

        // Ajout de la photo
        XmlElement photoElement = doc.CreateElement("ca:photo", nsmgr.LookupNamespace("ca"));
        photoElement.InnerText = photoName;
        newNurse.AppendChild(photoElement);

        XmlNode root = doc.SelectSingleNode("//ca:infirmiers", nsmgr);
        root.AppendChild(newNurse);

        doc.Save(filePath);
    }
    
    // Ajoute un nouveau patient
    public void AddPatient(string firstName, string lastName, string sexe, string naissance, string nss, Adresse adresse)
    {
        // Chargement du document xml
        XmlDocument doc = new XmlDocument();
        string filePath = "../../../data/xml/cabinet.xml";
        doc.Load(filePath);

        // Gestion du namespace
        XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
        nsmgr.AddNamespace("ca", "http://www.univ-grenoble-alpes.fr/l3miage/medical");

        // Création du nouveau patient
        XmlElement newPatient = doc.CreateElement("ca:patient", nsmgr.LookupNamespace("ca"));

        // Ajout du nom
        XmlElement lastnameElement = doc.CreateElement("ca:nom", nsmgr.LookupNamespace("ca"));
        lastnameElement.InnerText = lastName;
        newPatient.AppendChild(lastnameElement);
        
        // Ajout du prenom
        XmlElement firstnameElement = doc.CreateElement("ca:prénom", nsmgr.LookupNamespace("ca"));
        firstnameElement.InnerText = firstName;
        newPatient.AppendChild(firstnameElement);

        // Ajout du sexe
        XmlElement photoElement = doc.CreateElement("ca:sexe", nsmgr.LookupNamespace("ca"));
        if (sexe != "M" && sexe != "F" && sexe != "A") // Vérification du sexe
        {
            throw new Exception("Le sexe doit être M ou F");
        }
        photoElement.InnerText = sexe;
        newPatient.AppendChild(photoElement);
        
        // Ajout de la date de naissance
        XmlElement naissanceElement = doc.CreateElement("ca:naissance", nsmgr.LookupNamespace("ca"));
        if (!DateTime.TryParse(naissance, out DateTime dateNaissance)) // Vérification de la date de naissance
        {
            throw new Exception("La date de naissance n'est pas valide");
        }
        naissanceElement.InnerText = dateNaissance.ToString("yyyy-MM-dd");
        newPatient.AppendChild(naissanceElement);
        
        // Ajout du NSS
        XmlElement nssElement = doc.CreateElement("ca:numéro", nsmgr.LookupNamespace("ca"));
        CheckHas check = new CheckHas();
        if (!check.NSSValide(nss, dateNaissance.Year, dateNaissance.Month, sexe[0])) // Vérification du NSS
        {
            throw new Exception("Le NSS n'est pas valide");
        }
        nssElement.InnerText = nss;
        newPatient.AppendChild(nssElement);
        
        // Ajout de l'adresse
        XmlElement adresseElement = doc.CreateElement("ca:adresse", nsmgr.LookupNamespace("ca"));
            
        // Ajout de l'etage
        if (adresse.etage != null)
        {
            XmlElement etageElement = doc.CreateElement("ca:étage", nsmgr.LookupNamespace("ca"));
            etageElement.InnerText = adresse.etage.ToString();
            adresseElement.AppendChild(etageElement);
        }
        
        // Ajout du numero
        if (adresse.numero != null)
        {
            XmlElement numeroElement = doc.CreateElement("ca:numéro", nsmgr.LookupNamespace("ca"));
            numeroElement.InnerText = adresse.numero.ToString();
            adresseElement.AppendChild(numeroElement);
        }
        
        // Ajout de la rue
        XmlElement rueElement = doc.CreateElement("ca:rue", nsmgr.LookupNamespace("ca"));
        rueElement.InnerText = adresse.rue;
        adresseElement.AppendChild(rueElement);
        
        // Ajout du code postal
        XmlElement codePostalElement = doc.CreateElement("ca:codePostal", nsmgr.LookupNamespace("ca"));
        codePostalElement.InnerText = adresse.codePostal.ToString();
        adresseElement.AppendChild(codePostalElement);
        
        // Ajout de la ville
        if (adresse.ville != null)
        {
            XmlElement villeElement = doc.CreateElement("ca:ville", nsmgr.LookupNamespace("ca"));
            villeElement.InnerText = adresse.ville;
            adresseElement.AppendChild(villeElement);
        }
        
        // Ajout de l'adresse au patient
        newPatient.AppendChild(adresseElement);
        
        XmlNode root = doc.SelectSingleNode("//ca:patients", nsmgr);
        root.AppendChild(newPatient);

        doc.Save(filePath);
    }
    
    // Ajoute une visite à un patient
    public void AddVisit(string patientNSS, string nurseId, string[] acteId, string date)
    {
        // Chargement du document xml
        XmlDocument doc = new XmlDocument();
        string filePath = "../../../data/xml/cabinet.xml";
        doc.Load(filePath);

        // Gestion du namespace
        XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
        nsmgr.AddNamespace("ca", "http://www.univ-grenoble-alpes.fr/l3miage/medical");

        // Création de la nouvelle visite
        XmlElement newVisit = doc.CreateElement("ca:visite", nsmgr.LookupNamespace("ca"));
        
        newVisit.SetAttribute("intervenant", nurseId); // Ajout de l'id de l'intervenant
        
        // Ajout de la date
        if (!DateTime.TryParse(date, out DateTime visiteDate)) // Vérification de la date
        {
            throw new Exception("La date n'est pas valide");
        }
        newVisit.SetAttribute("date", visiteDate.ToString("yyyy-MM-dd"));
        
        // Ajout de l'id de l'acte
        XmlElement acteElement;
        foreach (string acte in acteId)
        {
            acteElement = doc.CreateElement("ca:acte", nsmgr.LookupNamespace("ca"));
            acteElement.SetAttribute("id", acte); // Ajout de l'id de l'intervenant
            newVisit.AppendChild(acteElement);
        }
        
        // Recherche du patient auquel ajouter la visite
        XmlNodeList patientNodes = doc.SelectNodes("//ca:patient", nsmgr);
        int i = 0;
        while (i < patientNodes.Count 
               && patientNodes[i].SelectSingleNode("ca:numéro", nsmgr) != null 
               && patientNodes[i].SelectSingleNode("ca:numéro", nsmgr).InnerText != patientNSS)
        {
            Console.WriteLine(patientNodes[i].SelectSingleNode("ca:nom", nsmgr).InnerText);
            i++;
        }

        // Ajout de la visite
        if (i == patientNodes.Count)
        {
            Console.WriteLine("Le patient n'a pas été trouvé");
        }
        else
        {
            Console.WriteLine(patientNodes[i].SelectSingleNode("ca:nom", nsmgr).InnerText);
            Console.WriteLine(i);
            Console.WriteLine(patientNodes.Count);
            // patientNodes[i].AppendChild(newVisit);
        }

        // Sauvegarde du document
        doc.Save(filePath);
    }
}