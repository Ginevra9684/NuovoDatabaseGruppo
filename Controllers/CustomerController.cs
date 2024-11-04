using Microsoft.EntityFrameworkCore;
public class CustomerController
{
    // Riferimenti al modello e alla vista passati tramite il costruttore 
    //private Model _model;
    private Database _database;
    private CustomerView _customerView;

    // Costruttore per inizializzare il controller con un modello e una vista specifici
    public CustomerController(Database database, CustomerView customerView)
    {
        _database = database;
        _customerView = customerView;
    }

    // Metodo principale per la gestione del menu clienti
    public void CustomerMenu()
    {
        Console.Clear();
        while (true)
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
                    _customerView.Errore();
                    break;
            }
            if (input != "5")
            {
                _customerView.Proseguimento();
            }
        }
    }
    /*
      // Metodo per inserire un nuovo cliente (Menu Opzione 1)
        public void InserisciCliente()    
        {
            Cliente cliente = new Cliente();        // Crea un nuovo oggetto Cliente
            cliente.Nome = _customerView.InserisciCliente();     // Usa la vista per ottenere il nome del cliente
            _model.InserisciCliente(cliente);       // Passa il cliente al modello per inserirlo nel database
        }
    */
    private bool VerificaClientiPresenti(List<Cliente> cliente)
    {

        if (cliente.Count == 0)
        {
            _customerView.Stampa("Nessun cliente presente.");
            return false;
        }
        return true;

    }
    private Cliente? TrovaClienteById(int id)
    {
        foreach (Cliente cliente in _database.Clienti)
        {
            if (cliente.Id == id)
            {
                return cliente;
            }
        }
        return null;
    }


    // Metodo per inserire un nuovo cliente (Menu Opzione 1)
    public void InserisciCliente()
    {
        Cliente cliente = new Cliente();
        cliente.Nome = _customerView.InserisciCliente();
        _database.Clienti.Add(cliente);
        _database.SaveChanges();
    }

    /*
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
    */

    // Metodo per visualizzare tutti i clienti (Menu opzione 2)
    public void VisualizzaClienti()
    {
        var clienti = _database.Clienti.ToList();
        if (!VerificaClientiPresenti(clienti)) return;

        _customerView.VisualizzaClienti(clienti);
    }
    // Estrapola una lista clienti dalla tabella corrispondente
    // Passa la lista alla view
    /*
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
    */

    // Metodo per modificare i dati di un cliente (Menu opzione 3)
    // Chiama il metodo `ModificaCliente` nella vista, che richiede all'utente di inserire l'ID del cliente
    // da modificare e il nuovo nome. Il metodo restituisce una tupla con l'ID e il nuovo nome che viene
    // immediatamente destrutturata nelle variabili `id` e `nuovoNome`
    public void ModificaCliente()
    {
        var clienti = _database.Clienti.ToList();
        if (!VerificaClientiPresenti(clienti)) return;

        VisualizzaClienti();
        var (id, nuovoNome) = _customerView.ModificaCliente();
        var cliente = TrovaClienteById(id);

        if (cliente != null)
        {
            cliente.Nome = nuovoNome;
            _database.SaveChanges();
            _customerView.Stampa("Nome cliente aggiornato con successo.");
        }
        else
        {
            _customerView.Stampa("Cliente non trovato.");
        }
    }

    /*
        public void EliminaCliente()   // Menu opzione 4
        {
            Cliente cliente = new Cliente();
            VisualizzaClienti();
            cliente.Id = _customerView.EliminaCliente();
            _model.EliminaCliente(cliente);
        }
    */

    // Metodo per eliminare un cliente (Menu opzione 4)
    public void EliminaCliente()
    {
        var clienti = _database.Clienti.ToList();
        if (!VerificaClientiPresenti(clienti)) return;

        VisualizzaClienti();
        var id = _customerView.EliminaCliente();
        var cliente = TrovaClienteById(id);

        if (cliente != null)
        {
            _database.Clienti.Remove(cliente);
            _database.SaveChanges();
            _customerView.Stampa("Cliente eliminato con successo.");
        }
        else
        {
            _customerView.Stampa("Cliente non trovato.");
        }
    }
}
