public class Ordini
{
    public int Id { get; set; }
    public DateTime DataAcquisto { get; set; }
    public decimal TotImport { get; set; } // Calcolerà il totale dell'ordine (TotImport = Prodotto * Qty)
   
}
