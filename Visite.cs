using System.Xml.Serialization;

namespace CabinetInfirmier;


[Serializable]
public class Visite
{
    private DateTime date;
    private int intervenant;
    
    [XmlElement("acte")] public List<Acte> Acte { get; set; }

    [XmlElement("date")]
    public DateTime Date
    {
        get => date;
        set => date = value;
    }
    
    [XmlElement("intervenant")]
    public int Intervenant
    {
        get => intervenant;
        set
        {
            if (value < -1) // -1 pour indiquer que l'intervenant n'est pas renseigné
                throw new ArgumentException("L'intervenant doit être un entier positif");
            intervenant = value;
        }
    }
    
    public Visite(DateTime date, int intervenant)
    {
        Date = date;
        Intervenant = intervenant;
        Acte = new List<Acte>();
    }
    
    public Visite()
    {
        Date = DateTime.MinValue; // Date minimale, pour indiquer que la date n'est pas renseignée
        Intervenant = -1; // Valeur négative, pour indiquer que l'intervenant n'est pas renseigné
        Acte = new List<Acte>();
    }

    public override string ToString()
    {
        string s = "Visite du " + Date + " par l'intervenant " + Intervenant + " :\n";
        foreach (Acte acte in Acte)
        {
            s += acte + "\n"; // le .ToString() est implicite
        }

        return s;
    }
}