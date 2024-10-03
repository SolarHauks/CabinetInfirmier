namespace CabinetInfirmier;

public class Adresse(int? etage, int? numero, string rue, int codePostal, string? ville)
{
    public int? etage = etage;
    public int? numero = numero;
    public string rue = rue;
    public int codePostal = codePostal;
    public string? ville = ville;
}