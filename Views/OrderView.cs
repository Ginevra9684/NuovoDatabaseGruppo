public class OrderView : BaseView
{
    public void ShowOrderMenu()
    {
        Stampa("MENU ORDINE");
        Stampa("1 - Aggiungere un nuovo ordine");
        Stampa("2 - Visualizzare tutti gli ordini");
        Stampa("3 - Torna al menu principale");
    }

    public void VisualizzaOrdini(List<Ordine> ordini)
    {
        if (ordini.Count == 0)
        {
            Stampa("Nessun ordine trovato.");
            return;
        }

        foreach (var ordine in ordini)
        {
            Stampa($"ID Ordine: {ordine.Id}");
            Stampa($"Data Acquisto: {ordine.DataAcquisto.ToString("yyyy-MM-dd HH:mm:ss")}");

            if (ordine.cliente != null)
            {
                Stampa($"Cliente: {ordine.cliente.Nome}");
            }
            else
            {
                Stampa("Cliente: N/A"); // Non c'è un oggetto Cliente associato a quell'ordine.
            }

            if (ordine.prodotto != null)
            {
                Stampa($"Prodotto: {ordine.prodotto.Nome}");
            }
            else
            {
                Stampa("Prodotto: N/A"); // Non c'è un oggetto Prodotto associato a quell'ordine.
            }

            Stampa($"Quantità: {ordine.Quantita}");
            Stampa("---------------------------------");
        }
    }

    public Ordine InserisciNuovoOrdine()
    {
        Ordine ordine = new Ordine();

        Stampa("Inserisci l'ID del cliente:");
        int idCliente = int.Parse(GetInput());
        ordine.cliente = new Cliente { Id = idCliente };

        // Imposta automaticamente la data di acquisto al momento dell'inserimento
        ordine.DataAcquisto = DateTime.Now;

        Stampa("Inserisci l'ID del prodotto:");
        int idProdotto = int.Parse(GetInput());
        ordine.prodotto = new Prodotto { Id = idProdotto };

        Stampa("Inserisci la quantità:");
        ordine.Quantita = GetInput();

        return ordine;
    }

}