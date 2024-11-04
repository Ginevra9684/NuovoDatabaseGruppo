public class Prodotto : General
{
    // Prezzo del prodotto
    public double Prezzo { get; set; }
    
    // Quantità disponibile in magazzino
    public int Giacenza { get; set; }
    
    // Identificatore della categoria a cui appartiene il prodotto
    public int? Id_categoria { get; set; }

     public Categoria? Categoria { get; set; }  // Relazione con la categoria
}