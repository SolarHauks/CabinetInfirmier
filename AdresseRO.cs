using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace CabinetInfirmier;

[XmlRoot("adresse", Namespace = "http://www.univ-grenoble-alpes.fr/l3miage/medical")]
[Serializable]
public class AdresseRO
{
    [XmlElement("étage")]
    public int? Etage { get; init; }

    [XmlElement("numéro")]
    public int? Numero { get; init; }

    [XmlElement("rue")]
    public string Rue { get; init; }

    [XmlElement("codePostal")]
    public int CodePostal { get; init; }

    [XmlElement("ville")]
    public string? Ville { get; init; }

    public AdresseRO(int? etage, int? numero, string rue, int codePostal, string? ville)
    {
        if (etage < -999 || etage > 999)
            throw new ArgumentException("Le numéro de l'étage doit être compris entre -999 et 999");
        if (numero < 0)
            throw new ArgumentException("Le numéro de rue ne peut pas être négatif");
        if (string.IsNullOrWhiteSpace(rue))
            throw new ArgumentException("Rue ne peut pas être vide");
        if (codePostal <= 0 || !Regex.IsMatch(codePostal.ToString(), @"[0-9]{5}$"))
            throw new ArgumentException("Le code postal doit être un nombre à 5 chiffres");
        if (string.IsNullOrWhiteSpace(ville) || !Regex.IsMatch(ville, @"^[a-zA-Z -éèêëïîôö]+$"))
            throw new ArgumentException("Ville doit contenir uniquement des lettres et des espaces");

        Etage = etage;
        Numero = numero;
        Rue = rue;
        CodePostal = codePostal;
        Ville = ville;
    }

    public override string ToString()
    {
        return "(" + Etage + " étage, " + Numero + " " + Rue + ", " + CodePostal + " " + Ville + ")\n";
    }
}