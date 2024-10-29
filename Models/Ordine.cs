public class Ordine
{
    // Identificatore univoco dell'ordine
    public int Id { get; set; }

    // Data in cui è stato effettuato l'acquisto
    public DateTime DataAcquisto { get; set; }

    // Quantità del prodotto acquistato
    public string ?Quantita { get; set; } 

    // Cliente associato all'ordine
    public Cliente ?cliente { get; set; } 

    // Prodotto associato all'ordine
    public Prodotto ?prodotto { get; set; }
}