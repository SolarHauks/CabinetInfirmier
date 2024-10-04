using System.Xml.Serialization;

namespace CabinetInfirmier;

[XmlRoot("infirmiers", Namespace = "http://www.univ-grenoble-alpes.fr/l3miage/medical")]
[Serializable]
public class Infirmiers
{
    [XmlElement("infirmier")] public List<Infirmier> Infirmier { get; set; }

    public Infirmiers()
    {
        Infirmier = new List<Infirmier>();
    }
    
    public void AddInfirmier(Infirmier infirmier)
    {
        Infirmier.Add(infirmier);
    }

    public override string ToString()
    {
        string s = "Infirmiers:\n";
        foreach (Infirmier infirmier in Infirmier)
        {
            s += infirmier + "\n"; // le .ToString() est implicite
        }

        return s;
    }
}