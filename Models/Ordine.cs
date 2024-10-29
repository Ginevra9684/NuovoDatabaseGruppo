public class Ordine
{
    public int Id { get; set; }
    public DateTime DataAcquisto { get; set; }
    public string Quantita { get; set; }

    public Cliente cliente { get; set; }
    public Prodotto prodotto { get; set; }

}
