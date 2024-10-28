public class Cliente : Utente
{
    public int CodiceCliente{get; set;}
    public required string Azienda { get; set; }
    public string? Indirizzo { get; set; }
    
}