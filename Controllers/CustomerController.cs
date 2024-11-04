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


    // Metodo per visualizzare tutti i clienti (Menu opzione 2)
    public void VisualizzaClienti()
    {
        var clienti = _database.Clienti.ToList();
        if (!VerificaClientiPresenti(clienti)) return;

        _customerView.VisualizzaClienti(clienti);
    }

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
            List<Ordine> ordini = _database.Ordini.Include(nameof(Ordine.Cliente)).ToList();
            foreach (var ordine in ordini)
            {
                if (ordine.Cliente!.Id == cliente.Id) _database.Ordini.Remove(ordine);
            }
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
