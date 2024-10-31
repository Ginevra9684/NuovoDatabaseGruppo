public class OrderView : BaseView
{
    // Visualizza il menu delle opzioni per la gestione degli ordini
    public void ShowOrderMenu()
    {
        Stampa("MENU ORDINE");
        Stampa("1 - Aggiungere un nuovo ordine");
        Stampa("2 - Visualizzare tutti gli ordini");
        Stampa("3 - Modifica un ordine");
        Stampa("4 - Elimina un ordine");
        Stampa("5 - Torna al menu principale");
    }

    // Crea un nuovo ordine basato sugli input forniti dall'utente (Menu opzione 1)
    public Ordine AggiungiOrdine()
    {
        Ordine ordine = new Ordine();

        // Chiede l'ID del cliente e lo assegna all'ordine
        // Stampa("Inserisci l'ID del cliente:");
        int idCliente = GetIntInput("Inserisci l'ID del cliente:");
        ordine.Cliente = new Cliente { Id = idCliente };

        // Imposta la data di acquisto come data e ora attuale
        ordine.DataAcquisto = DateTime.Now;

        // Chiede l'ID del prodotto e lo assegna all'ordine
        // Stampa("Inserisci l'ID del prodotto:");
        int idProdotto = GetIntInput("Inserisci l'ID del prodotto:");
        ordine.Prodotto = new Prodotto { Id = idProdotto };

        // Chiede la quantità e la assegna all'ordine
        // Stampa("Inserisci la quantità:");
        ordine.Quantita = GetIntInput("Inserisci la quantità:");

        return ordine;
    }

    // Visualizza tutti gli ordini presenti nella lista fornita (Menu opzione 2)
    public void VisualizzaOrdini(List<Ordine> ordini)   
    {
        if (ordini.Count == 0)
        {
            Stampa("Nessun ordine trovato."); // Messaggio se non ci sono ordini
            return;
        }

        foreach (var ordine in ordini)
        {
            // Visualizza i dettagli di ogni ordine
            Stampa($"ID Ordine: {ordine.Id}");
            Stampa($"Data Acquisto: {ordine.DataAcquisto.ToString("yyyy-MM-dd HH:mm:ss")}");

            // Visualizza il nome del cliente associato, se presente
            if (ordine.Cliente != null)
            {
                Stampa($"Cliente: {ordine.Cliente.Nome}");
            }
            else
            {
                Stampa("Cliente: N/A"); // Messaggio se non c'è cliente associato
            }

            // Visualizza il nome del prodotto associato, se presente
            if (ordine.Prodotto != null)
            {
                Stampa($"Prodotto: {ordine.Prodotto.Nome}");
            }
            else
            {
                Stampa("Prodotto: N/A"); // Messaggio se non c'è prodotto associato
            }

            Stampa($"Quantità: {ordine.Quantita}");
            Stampa("---------------------------------");
        }
    }

    // Modifica un ordine esistente (Menu opzione 3)
    public Ordine ModificaOrdine()
    {
        Ordine ordine = new Ordine();   // Istanza di un oggetto ordine
        // Stampa("Inserisci l'ID dell'ordine di riferimento:");
        int id = GetIntInput("Inserisci l'ID dell'ordine di riferimento:");  // Richiede l'id dell'ordine di riferimento
        ordine.Id = id;

        // Stampa("Inserisci l'ID del nuovo prodotto :");
        int idProdotto = GetIntInput("Inserisci l'ID del nuovo prodotto :"); // Richiede l'id del nuovo prodotto
        ordine.Prodotto = new Prodotto { Id = idProdotto };

        // Stampa("Inserisci la quantità:");
        ordine.Quantita = GetIntInput("Inserisci la quantità:");    // Richiede la nuova quantità

        return ordine;
    }

    //Elimina un ordine (Menu opzione 4)
    public int EliminaOrdine()
    {
        // Stampa("Inserisci l'ID dell'ordine di riferimento:");
        int id = GetIntInput("Inserisci l'ID dell'ordine di riferimento:");
        return id;
    }
}