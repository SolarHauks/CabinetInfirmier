using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace CabinetInfirmier;

[Serializable]
public class InfirmierRO
{
    [XmlAttribute("id")]
    public int Id { get; init; }

    [XmlElement("nom")]
    public string Nom { get; init; }

    [XmlElement("prénom")]
    public string Prenom { get; init; }

    [XmlElement("photo")]
    public string Photo { get; init; }

    public InfirmierRO(int id, string nom, string prenom, string photo)
    {
        if (id <= 0)
            throw new ArgumentException("L'id doit être positif");
        if (string.IsNullOrWhiteSpace(nom) || !Regex.IsMatch(nom, "^[A-Z][a-zéèêëîïôö]+$"))
            throw new ArgumentException("Le nom doit commencer par une majuscule suivie de minuscules");
        if (string.IsNullOrWhiteSpace(prenom) || !Regex.IsMatch(prenom, "^[A-Z][a-zéèêëîïôö]+$"))
            throw new ArgumentException("Le prénom doit commencer par une majuscule suivie de minuscules");
        if (string.IsNullOrWhiteSpace(photo) || !Regex.IsMatch(photo, @"^[a-zA-Z_-]+[.](jpg|JPG|gif|GIF|png|PNG|svg|SVG)$"))
            throw new ArgumentException("Une photo doit etre une chaine de caractère de la forme 'filename.extension' (parmis jpg, gif, png, svg).");

        Id = id;
        Nom = nom;
        Prenom = prenom;
        Photo = photo;
    }

    public override string ToString()
    {
        return "Infirmier{" +
               "id=" + Id +
               ", nom='" + Nom + '\'' +
               ", prenom='" + Prenom + '\'' +
               ", photo='" + Photo + '\'' +
               '}';
    }
}