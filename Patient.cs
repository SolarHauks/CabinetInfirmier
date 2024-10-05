using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace CabinetInfirmier;

[Serializable]
public class Patient
{
    private string nom;
    private string prenom;
    private string sexe;
    private DateTime naissance;
    private string numSecu;
    
    private Adresse adresse;
    private Visite visite;


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
    
    [XmlElement("sexe")]
    public string Sexe
    {
        get => sexe;
        set
        {
            if (value != "M" && value != "F" && value != "A" && value != "I") // I pour inconnu, valeur temporaire
                throw new ArgumentException("Le sexe doit être M ou F ou A");
            sexe = value;
        }
    }

    [XmlElement("naissance")]
    public DateTime Naissance
    {
        get => naissance;
        set
        {
            if (value > DateTime.Now)
                throw new ArgumentException("La date de naissance doit être antérieure à la date actuelle");
            naissance = value;
        }
    }
    
    [XmlElement("numéro")]
    public string NumSecu
    {
        get => numSecu;
        set
        {
            CheckHas check = new CheckHas();
            if (value != "000000000000000" && !check.NSSValide(value, Naissance.Year, Naissance.Month, Sexe))
                throw new ArgumentException("Le numéro de sécurité sociale n'est pas valide");
            numSecu = value;
        }
    }
    
    [XmlElement("adresse")]
    public Adresse Adresse
    {
        get => adresse;
        set => adresse = value;
    }

    public Patient(string nom, string prenom, string sexe, DateTime naissance, string numSecu, Adresse adresse)
    {
        Nom = nom;
        Prenom = prenom;
        Sexe = sexe;
        Naissance = naissance;
        NumSecu = numSecu;
        Adresse = adresse;
    }
    
    public Patient()
    {   
        Nom = "Inconnue";
        Prenom = "Inconnue";
        Sexe = "I";
        Naissance = DateTime.MinValue; // Date minimale, jour 1 de l'an 1
        NumSecu = "000000000000000";
        Adresse = new Adresse();
    }

    public override string ToString()
    {
        return $"Nom : {Nom}\nPrénom : {Prenom}\nSexe : {Sexe}\nDate de naissance : {Naissance:yyyy-MM-dd}\nNuméro de sécurité sociale : {NumSecu}\nAdresse : {Adresse}";
    }
}