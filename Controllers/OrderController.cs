public class OrderController
{
    private Database _database;  // Riferimento al modello per l'accesso ai dati degli ordini
    private OrderView _orderView;  // Riferimento alla vista per visualizzare l'interfaccia degli ordini
    private ProductController _productController; // Riferimento al controller prodotti per richiamarne i metodi
    private CustomerController _customerController; // Riferimento al controller clienti per richiamarne i metodi

     // Costruttore che inizializza il controller degli ordini con il modello, la vista e i controller di prodotti e clienti
    public OrderController(Database database, OrderView orderView, ProductController productController, CustomerController customerController)
    {
        _database = database;
        _orderView = orderView;
        _productController = productController;
        _customerController = customerController;
    }

    // Metodo per mostrare il menu degli ordini
    public void OrderMenu()
    {
        Console.Clear();
        while (true)
        {
            _orderView.ShowOrderMenu();
            var input = _orderView.GetInput();

            switch (input)
            {
                case "1":
                    AggiungiOrdine();
                    break;
                case "2":
                    VisualizzaOrdini();
                    break;
                case "3":
                    ModificaOrdine();
                    break;
                case "4":
                    EliminaOrdine();
                    break;
                case "5":
                    return; // Torna al menu principale
                default:
                    _orderView.Errore();
                    break;
            }
        }
    }

// Metodo per aggiungere un ordine (Menu opzione 1)
    private void AggiungiOrdine()
    {
        _productController.VisualizzaProdotti();
        _customerController.VisualizzaClienti();
        // Richiede i dettagli dell'ordine dall'utente tramite la vista e crea un nuovo ordine
        Ordine nuovoOrdine = _orderView.AggiungiOrdine();

        if (_database.Clienti != null)
        {
            // Recupera il cliente esistente dal database utilizzando l'ID specificato nell'ordine
            // In questo modo si assicura che l'entità cliente sia tracciata dal contesto di Entity Framework
            nuovoOrdine.Cliente = _database.Clienti.Find(nuovoOrdine.Cliente!.Id);

            // Recupera il prodotto esistente dal database utilizzando l'ID specificato nell'ordine
            // Anche qui si garantisce che l'entità prodotto sia tracciata dal contesto di Entity Framework
            nuovoOrdine.Prodotto = _database.Prodotti.Find(nuovoOrdine.Prodotto!.Id);

            // Verifica che il cliente e il prodotto siano validi non null prima di aggiungere l'ordine
            if (nuovoOrdine.Cliente != null && nuovoOrdine.Prodotto != null && nuovoOrdine.Quantita <= nuovoOrdine.Prodotto.Giacenza)
            {
                // Aggiunge il nuovo ordine 
                _database.Ordini.Add(nuovoOrdine);
                nuovoOrdine.Prodotto.Giacenza -= nuovoOrdine.Quantita;

                // Salva le modifiche nel database, inclusa la creazione del nuovo ordine
                _database.SaveChanges();

                // Conferma l'aggiunta dell'ordine tramite la vista
                _orderView.Stampa("Ordine aggiunto con successo.");
            }
            else if (nuovoOrdine.Quantita > nuovoOrdine.Prodotto!.Giacenza)
            {
                _orderView.Stampa("Giacenza prodotto non sufficiente");
            }
            else
            {
                // Mostra un messaggio di errore se il cliente o il prodotto non sono stati trovati nel database
                _orderView.Stampa("Errore: Cliente o prodotto non trovato.");
            }
        }
        else
        {
            Console.WriteLine("Nessun cliente risulta registrato");
        }
    }



private void VisualizzaOrdini()
{
    // Carica gli ordini con i dettagli del cliente e del prodotto collegati
    var ordini = new List<Ordine>();
    foreach (var ordine in _database.Ordini)
    {
        // Carica il cliente e il prodotto associati all'ordine
        ordine.Cliente = TrovaClientePerId(ordine.Cliente!.Id);
        ordine.Prodotto = TrovaProdottoPerId(ordine.Prodotto!.Id);
        ordini.Add(ordine);
    }

    // Passa la lista degli ordini alla vista per la visualizzazione
    _orderView.VisualizzaOrdini(ordini);
}

private void ModificaOrdine()
{
    VisualizzaOrdini();
    _productController.VisualizzaProdotti();
    Ordine ordineDaModificare = _orderView.ModificaOrdine();

    Ordine? ordine = TrovaOrdinePerId(ordineDaModificare.Id);  // Cerca un ordine tramite Id cliente
    Prodotto? prodotto = TrovaProdottoPerId(ordineDaModificare.Prodotto!.Id);  // Cerca il nuovo prodotto tramite id nei prodotti
    
    if (ordine != null && prodotto != null && ordineDaModificare.Quantita <= (prodotto.Giacenza + ordine.Quantita - ordineDaModificare.Quantita))
    {
        ordine.Prodotto = prodotto; // Aggiorna il prodotto nell'ordine
        prodotto.Giacenza = prodotto.Giacenza + ordine.Quantita - ordineDaModificare.Quantita;
        ordine.Quantita = ordineDaModificare.Quantita;  // Aggiorna la quantità nell'ordine
        _database.SaveChanges();    // Salva le modifiche
        _orderView.Stampa("Ordine modificato con successo.");
    }
    else if (ordineDaModificare.Quantita > (prodotto!.Giacenza + ordine!.Quantita - ordineDaModificare.Quantita))
    {
        _orderView.Stampa("Giacenza prodotto non sufficiente");
    }
    else
    {
        _orderView.Stampa("Ordine non trovato");
    }
}

private void EliminaOrdine()
{
    VisualizzaOrdini();
    int id = _orderView.EliminaOrdine();
    Ordine? ordine = TrovaOrdinePerId(id);  // Cerca l'ordine tramite id
    if (ordine != null)
    {
        ordine.Prodotto!.Giacenza += ordine.Quantita;
        _database.Remove(ordine);   // Elimina l'ordine
        _database.SaveChanges();    // Salva le modifiche
        _orderView.Stampa("Ordine eliminato con successo.");
    }
    else
    {
        _orderView.Stampa("Ordine non trovato");
    }
}

// Metodi di supporto per cercare oggetti senza LINQ o Lambda
private Ordine? TrovaOrdinePerId(int id)
{
    foreach (var ordine in _database.Ordini)
    {
        if (ordine.Id == id)
            return ordine;
    }
    return null;
}

private Prodotto? TrovaProdottoPerId(int id)
{
    foreach (var prodotto in _database.Prodotti)
    {
        if (prodotto.Id == id)
            return prodotto;
    }
    return null;
}

private Cliente? TrovaClientePerId(int id)
{
    foreach (var cliente in _database.Clienti)
    {
        if (cliente.Id == id)
            return cliente;
    }
    return null;
}

}
