public class Prodotto
{
    // Identificatore univoco del prodotto
    public int Id { get; set; }
    
    // Nome del prodotto, può essere null
    public string ?Nome { get; set; }
    
    // Prezzo del prodotto
    public decimal Prezzo { get; set; }
    
    // Quantità disponibile in magazzino
    public int Giacenza { get; set; }
    
    // Identificatore della categoria a cui appartiene il prodotto
    public int Id_categoria { get; set; }
}