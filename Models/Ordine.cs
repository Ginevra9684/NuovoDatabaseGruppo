public class Ordine : General
{
    // Proprietà che restituisce il nome
    public new string? Nome { get { return $"BRT-{Id}_{Cliente!.Id}"; } }

    // Data in cui è stato effettuato l'acquisto
    public DateTime DataAcquisto { get; set; }

    // Quantità del prodotto acquistato
    public int Quantita { get; set; }

    // Cliente associato all'ordine
    public Cliente Cliente { get; set; }

    // Prodotto associato all'ordine
    public Prodotto Prodotto { get; set; }
}
