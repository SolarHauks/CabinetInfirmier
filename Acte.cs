using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace CabinetInfirmier;


[Serializable]
public class Acte
{
    private int id;

    [XmlAttribute("id")]
    public int Id
    {
        get => id;
        set
        {
            if (value < -1) // -1 pour indiquer que l'identifiant n'est pas renseigné
                throw new ArgumentException("L'identifiant doit être un entier positif");
            id = value;
        }
    }
    
    public Acte(int id)
    {
        Id = id;
    }
    
    public Acte()
    {
        Id = -1; // Valeur négative, pour indiquer que l'identifiant n'est pas renseigné
    }

    public override string ToString()
    {
        return "Acte n°" + Id;
    }
}