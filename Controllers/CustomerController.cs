public class CustomerController
{
     // Riferimenti al modello e alla vista passati tramite il costruttore 
    private Model _model;
    private CustomerView _customerView;

// Costruttore per inizializzare il controller con un modello e una vista specifici
    public CustomerController(Model model, CustomerView customerView)
    {
        _model = model;
        _customerView = customerView;
    }

// Metodo principale per la gestione del menu clienti
    public void CustomerMenu()
    {
        while(true)
        {
            _customerView.ShowCustomerMenu(); // Visualizza il menu delle opzioni relative ai clienti
            var input = _customerView.GetInput();       //ottinee input
            switch (input)
            {
                case "1":
                    InserisciCliente();
                    break;
                case "2":
                    VisualizzaClienti();
                    break;
                case "3":
                    ModificaCliente();
                    break;
                case "4":
                    EliminaCliente();
                    break;
                case "5":
                    return;
                default:
                    _customerView.Stampa("opzione non valida");
                    break;
            }
        }
    }

  // Metodo per inserire un nuovo cliente (Menu Opzione 1)
    public void InserisciCliente()    
    {
        Cliente cliente = new Cliente();        // Crea un nuovo oggetto Cliente
        cliente.Nome = _customerView.InserisciCliente();     // Usa la vista per ottenere il nome del cliente
        _model.InserisciCliente(cliente);       // Passa il cliente al modello per inserirlo nel database
    }

 // Metodo per visualizzare tutti i clienti (Menu opzione 2)
    public void VisualizzaClienti() // Menu opzione 2
    {
        var clienti = new List<Cliente>();           // Inizializza una lista vuota per contenere i clienti

        using (var reader = _model.VisualizzaClienti())   // Chiama il metodo del modello per ottenere i dati dei clienti
        {
            // Scorre ogni riga dei risultati e crea un oggetto Cliente per ciascuno
            while (reader.Read())
            {
                var cliente = new Cliente
                {
                    Id = Convert.ToInt32(reader["id"]),    // Converte il valore dell'ID in intero
                    Nome = reader["nome"].ToString(),      // Ottiene il nome del cliente

                };
                clienti.Add(cliente);     // Aggiunge l'oggetto Cliente alla lista
            }
        }

        _customerView.VisualizzaClienti(clienti);      // Passa la lista completa dei clienti alla vista per visualizzarli all'utente
    }

 // Metodo per modificare i dati di un cliente (Menu opzione 3)
 // Chiama il metodo `ModificaCliente` nella vista, che richiede all'utente di inserire l'ID del cliente
    // da modificare e il nuovo nome. Il metodo restituisce una tupla con l'ID e il nuovo nome che viene
    // immediatamente destrutturata nelle variabili `id` e `nuovoNome`
    public void ModificaCliente()   
    {
        VisualizzaClienti();      // Mostra l'elenco attuale dei clienti per consentire la selezione
        var (id, nuovoNome) = _customerView.ModificaCliente();  // Usa la vista per ottenere l'ID e il nuovo nome del cliente
        Cliente cliente = new Cliente { Id = id };          // Crea un nuovo oggetto Cliente con l'ID selezionato
        _model.ModificaCliente(cliente, nuovoNome);         // Passa il cliente e il nuovo nome al modello per aggiornare i dati nel database
    }

    public void EliminaCliente()   // Menu opzione 4
    {
        Cliente cliente = new Cliente();
        VisualizzaClienti();
        cliente.Id = _customerView.EliminaCliente();
        _model.EliminaCliente(cliente);
    }
}
