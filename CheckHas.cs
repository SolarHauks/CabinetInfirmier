using System.Xml;
using System.Xml.XPath;

namespace CabinetInfirmier;

// Cette classe contient des méthodes permettant de vérifier la présence de certains éléments dans un fichier XML
public class CheckHas
{
    // Compte le nombre d'éléments du nom donné dans le fichier XML
    public int Count(string elementName)
    {
        string xmlFilePath = "../../../data/xml/cabinet.xml"; // Chemin du fichier XML
        XmlDocument doc = new XmlDocument();
        doc.Load(xmlFilePath);
        
        XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
        nsmgr.AddNamespace("ca", "http://www.univ-grenoble-alpes.fr/l3miage/medical");
        
        XmlNodeList nodes = doc.SelectNodes("//ca:" + elementName, nsmgr); // Sélectionne tous les éléments du nom donné
        
        return nodes.Count; // Retourne le nombre d'éléments trouvés
    }

    // Vérifie si tous les éléments du nom donné ont un élément adresse complet
    public bool hasAdresse(string elementName)
    {
        string xmlFilePath = "../../../data/xml/cabinet.xml"; // Chemin du fichier XML
        XmlDocument doc = new XmlDocument();
        doc.Load(xmlFilePath);

        XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
        nsmgr.AddNamespace("ca", "http://www.univ-grenoble-alpes.fr/l3miage/medical");

        XmlNodeList addressNodes = doc.SelectNodes("//ca:" + elementName + "/ca:adresse", nsmgr); // Selectionne tous les noeuds adresse

        // Pour chaque noeud adresse, vérifie si il est complet (ie. contient 5 éléments)
        foreach (XmlNode addressNode in addressNodes)
        {
            if (addressNode.ChildNodes.Count != 5)
            {
                return false; // Si un noeud adresse ne contient pas exactement 5 éléments, retourne faux
            }
        }

        return true; // Si tous les noeuds adresse contiennent exactement 5 éléments, retourne vrai
    }

    // Vérifie qu'un numéro de sécurité sociale est valide
    public bool NSSValide(string nss, int anneeNaissance, int moisNaissance, char Sexe)
    {
        // Vérifie que le numéro de sécurité sociale est composé de 15 chiffres
        if (nss.Length != 15)
        {
            return false;
        }

        // Vérifie que le numéro de sécurité sociale est composé uniquement de chiffres
        foreach (char c in nss)
        {
            if (!char.IsDigit(c))
            {
                return false;
            }
        }
        
        int sexeDigit = int.Parse(nss.Substring(0, 1));
        if (!((sexeDigit == 1 && Sexe == 'M') || (sexeDigit == 2 && Sexe == 'F') || (sexeDigit == 3 || sexeDigit == 4 || sexeDigit == 7 || sexeDigit == 8)))
        {
            return false;
        }
        
        int annee = int.Parse(nss.Substring(1, 2));
        if (annee != anneeNaissance % 100)
        {
            return false;
        }
        
        int mois = int.Parse(nss.Substring(3, 2));
        if (!(mois == moisNaissance || mois is >= 20 and <= 42 || mois is >= 50 and <= 99))
        {
            return false;
        }
        
        if (!long.TryParse(nss.Substring(0, 13), out long nssNumber))
        {
            return false;
        }
        
        int cleSecu = int.Parse(nss.Substring(13, 2));
        int cleCalculee = 97 - (int)(nssNumber % 97);
        if (cleSecu != cleCalculee)
        {
            return false;
        }
        
        return true;
    }
    
    // Vérifie si tous les numéros de sécurité sociale sont valides
    public bool AllNSSValide()
    {
        string xmlFilePath = "../../../data/xml/cabinet.xml"; // Chemin du fichier XML
        XmlDocument doc = new XmlDocument();
        doc.Load(xmlFilePath);

        XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
        nsmgr.AddNamespace("ca", "http://www.univ-grenoble-alpes.fr/l3miage/medical");

        XmlNodeList patientNodes = doc.SelectNodes("//ca:patient", nsmgr); // Selectionne tous les noeuds patient

        // Pour chaque noeud patient, vérifie si le numéro de sécurité sociale est valide
        foreach (XmlNode patientNode in patientNodes)
        {
            // Le NSS etant optionnel, on passe au noeud suivant si le numéro de sécurité sociale n'existe pas
            XmlNode nssNode = patientNode.SelectSingleNode("ca:numéro", nsmgr);
            if (nssNode == null)
            {
                continue; // Passe au nœud suivant si le numéro de sécurité sociale n'existe pas
            }
            
            string nss = patientNode.SelectSingleNode("ca:numéro", nsmgr).InnerText;
            int anneeNaissance = int.Parse(patientNode.SelectSingleNode("ca:naissance", nsmgr).InnerText.Substring(0, 4));
            int moisNaissance = int.Parse(patientNode.SelectSingleNode("ca:naissance", nsmgr).InnerText.Substring(5, 2));
            char sexe = char.Parse(patientNode.SelectSingleNode("ca:sexe", nsmgr).InnerText);
            if (!NSSValide(nss, anneeNaissance, moisNaissance, sexe))
            {
                return false; // Si un numéro de sécurité sociale n'est pas valide, retourne faux
            }
        }

        return true; // Si tous les numéros de sécurité sociale sont valides, retourne vrai
    }
}