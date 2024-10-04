using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace CabinetInfirmier;

[Serializable]
public class Infirmier
{
    private int id;
    private string nom;
    private string prenom;
    private string photo;

    [XmlIgnore]
    public int Id
    {
        get => id;
        set
        {
            if (value < 0)
                throw new ArgumentException("L'id doit être positif");
            id = value;
        }
    }
    
    [XmlAttribute("id")]
    public string IdString
    {
        get => id.ToString("D3");
        set
        {
            if (!int.TryParse(value, out int parsedId) || parsedId < 0)
                throw new ArgumentException("L'id doit être un nombre positif");
            id = parsedId;
        }
    }


    [XmlElement("nom")]
    public string Nom
    {
        get => nom;
        set
        {
            if (string.IsNullOrWhiteSpace(value) || !Regex.IsMatch(value, "^[A-Z][a-zéèêëîïôö]+$"))
                throw new ArgumentException("Le nom doit commencer par une majuscule suivie de minuscules");
            nom = value;
        }
    }

    [XmlElement("prénom")]
    public string Prenom
    {
        get => prenom;
        set
        {
            if (string.IsNullOrWhiteSpace(value) || !Regex.IsMatch(value, "^[A-Z][a-zéèêëîïôö]+$"))
                throw new ArgumentException("Le prénom doit commencer par une majuscule suivie de minuscules");
            prenom = value;
        }
    }

    [XmlElement("photo")]
    public string Photo
    {
        get => photo;
        set
        {
            if (string.IsNullOrWhiteSpace(value) || !Regex.IsMatch(value, @"^[a-zA-Z_-]+[.](jpg|JPG|gif|GIF|png|PNG|svg|SVG)$"))
                throw new ArgumentException("Une photo doit etre une chaine de caractère de la forme 'filename.extension' (parmis jpg, gif, png, svg).");
            photo = value;
        }
    }

    public Infirmier(int id, string nom, string prenom, string photo)
    {
        Id = id;
        Nom = nom;
        Prenom = prenom;
        Photo = photo;
    }
    
    public Infirmier()
    {
        Id = 0;
        Nom = "Inconnue";
        Prenom = "Inconnue";
        Photo = "inconnue.jpg";
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