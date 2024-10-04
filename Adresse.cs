using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace CabinetInfirmier;

[XmlRoot("adresse", Namespace = "http://www.univ-grenoble-alpes.fr/l3miage/medical")]
[Serializable]
public class Adresse
{
    private int? etage;
    private int? numero;
    private string rue;
    private int codePostal;
    private string? ville;

    [XmlElement("étage")]
    public int? Etage
    {
        get => etage;
        set
        {
            if (value < -999 || value > 999)
                throw new ArgumentException("Le numéro de l'étage doit etre compris entre -999 et 999");
            etage = value;
        }
    }

    [XmlElement("numéro")]
    public int? Numero
    {
        get => numero;
        set
        {
            if (value < 0)
                throw new ArgumentException("Le numéro de rue ne peut pas être négatif");
            numero = value;
        }
    }

    [XmlElement("rue")]
    public string Rue
    {
        get => rue;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Rue ne peut pas être vide");
            rue = value;
        }
    }

    [XmlElement("codePostal")]
    public int CodePostal
    {
        get => codePostal;
        set
        {
            if (value <= 0)
                throw new ArgumentException("Le code postal doit être positif");
            if (!Regex.IsMatch(value.ToString(), @"[0-9]{5}$"))
                throw new ArgumentException("Le code postal doit être un nombre à 5 chiffres");
            codePostal = value;
        }
    }

    [XmlElement("ville")]
    public string? Ville
    {
        get => ville;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Ville ne peut pas être vide");
            if (!Regex.IsMatch(value, @"^[a-zA-Z -éèêëïîôö]+$"))
                throw new ArgumentException("Ville doit contenir uniquement des lettres et des espaces");
            ville = value;
        }
    }

    public Adresse(int? etage, int? numero, string rue, int codePostal, string? ville)
    {
        Etage = etage;
        Numero = numero;
        Rue = rue;
        CodePostal = codePostal;
        Ville = ville;
    }
    
    public Adresse()
    {
        Etage = 0;
        Numero = 0;
        Rue = "Inconnue";
        CodePostal = 99999;
        Ville = "Inconnue";
    }

    public override string ToString()
    {
        return "(" + Etage + " étage, " + Numero + " " + Rue + ", " + CodePostal + " " + Ville + ")\n";
    }
}