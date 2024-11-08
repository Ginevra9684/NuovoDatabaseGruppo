public class Ordine : General
{
    private DateTime dataAcquisto;
    public override string Nome => $"BRT-{Id}_{Cliente!.Id}";

    // Data in cui è stato effettuato l'acquisto
    public DateTime DataAcquisto { get => dataAcquisto; set => dataAcquisto = value; }

    // Quantità del prodotto acquistato
    public int Quantita { get; set; }

    // Cliente associato all'ordine
    public Cliente? Cliente { get; set; }

    // Prodotto associato all'ordine
    public Prodotto? Prodotto { get; set; }
}
