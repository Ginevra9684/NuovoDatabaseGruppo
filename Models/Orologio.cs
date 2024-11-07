using System.Security.Cryptography.X509Certificates;

public class Orologio : Prodotto{
    public Materiale? Materiale { get; set; }
    public string? Modello{ get; set; }
    public Tipologia? Tipologia { get; set; }
    public string? Referenza{ get; set; }
    public int Diametro { get; set; }
    
    public Genere? Genere {get; set; }
}

