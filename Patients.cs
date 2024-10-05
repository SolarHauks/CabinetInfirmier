using System.Xml.Serialization;

namespace CabinetInfirmier;

[Serializable]
public class Patients
{
    [XmlElement("patient")] public List<Patient> Patient { get; set; }

    public Patients()
    {
        Patient = new List<Patient>();
    }
    
    public void AddPatient(Patient patient)
    {
        Patient.Add(patient);
    }

    public override string ToString()
    {
        string s = "Patients :\n";
        foreach (Patient patient in Patient)
        {
            s += patient + "\n"; // le .ToString() est implicite
        }

        return s;
    }
}