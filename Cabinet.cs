using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace CabinetInfirmier;

[Serializable]
[XmlRoot("cabinet", Namespace = "http://www.univ-grenoble-alpes.fr/l3miage/medical")]
public class Cabinet
{
    private string nom;
    private Adresse adresse;
    private Infirmiers infirmiers;
    private Patients patients;
    
    [XmlElement("nom")]
    public string Nom
    {
        get => nom;
        set
        {
            if (string.IsNullOrWhiteSpace(value) || !Regex.IsMatch(value, "^[a-zA-Z -éèêëïîôö]+$"))
                throw new ArgumentException("Le nom doit commencer par une majuscule suivie de minuscules");
            nom = value;
        }
    }
    
    [XmlElement("adresse")]
    public Adresse Adresse
    {
        get => adresse;
        set => adresse = value;
    }
    
    [XmlElement("infirmiers")]
    public Infirmiers Infirmiers
    {
        get => infirmiers;
        set => infirmiers = value;
    }
    
    [XmlElement("patients")]
    public Patients Patients
    {
        get => patients;
        set => patients = value;
    }

    public Cabinet(string nom, Adresse adresse, Infirmiers infirmiers, Patients patients)
    {
        Nom = nom;
        Adresse = adresse;
        Infirmiers = infirmiers;
        Patients = patients;
    }
    
    public Cabinet()
    {
        Nom = "Inconnu";
        Adresse = new Adresse();
        Infirmiers = new Infirmiers();
        Patients = new Patients();
    }
    
    public override string ToString()
    {
        return $"Cabinet : {Nom}\nAdresse : {Adresse}\nInfirmiers : {Infirmiers}\nPatients : {Patients}";
    }
}