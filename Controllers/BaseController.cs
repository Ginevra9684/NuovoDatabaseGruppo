using System.Data.Common;

public class BaseController
{
    private Model _model;
    private ProductView _productView;

    private CategoryView _categoryView;

    private BaseView _baseView;
    private CustomerView _customerView;
    private CategoryController _categoryController;
    private CustomerController _customerController;

    // Costruttore del Controller riceve il Model e la View
    public BaseController(Model model, ProductView productView, CategoryView categoryView, BaseView baseView, CustomerView customerView)
    {
        _model = model;
        _productView = productView;
        _categoryView = categoryView;
        _baseView = baseView;
        _customerView = customerView;
        _categoryController = new CategoryController(model, categoryView, productView);
        _customerController = new CustomerController(model, customerView);  // Inizializza il CustomerController
    }

    public void MainMenu()
    {
        while (true)
        {
            ShowMainMenu();

            var input = _productView.GetInput();
            switch (input)
            {
                case "1":
                    VisualizzaProdotti();
                    break;
                case "2":
                    VisualizzaProdottiOrdinatiPerPrezzo();
                    break;
                case "3":
                    VisualizzaProdottiOrdinatiPerQuantita();
                    break;
                case "4":
                    ModificaPrezzoProdotto();   // Prende dalla View il nome e il prezzo
                    break;
                case "5":
                    EliminaProdotto();
                    break;
                case "6":
                    VisualizzaProdottoPiuCostoso();
                    break;
                case "7":
                    VisualizzaProdottoMenoCostoso();
                    break;
                case "8":
                    InserisciProdotto();
                    break;
                case "9":
                    VisualizzaProdotto();
                    break;
                case "10":
                    _categoryController.VisualizzaCategorie();
                    break;
                case "11":
                    _categoryController.InserisciCategoria();
                    break;
                case "12":
                    _categoryController.EliminaCategoria();
                    break;
                case "13":
                    _categoryController.InserisciProdottoCategoria();
                    break;
                case "14":
                    _customerController.InserisciCliente();
                    break;
                case "15":
                    _customerController.VisualizzaClienti();
                    break;
                case "16":
                _customerController.ModificaCliente();
                break;
                case "17":
                _customerController.EliminaCliente();
                break;
                case "18":
                    Console.WriteLine("Uscita in corso...");
                    // BISOGNA FAR IN MODO DI CHIUDERE LA CONNESSIONE AL DATABSE QUI
                    return; // Uscita dal ciclo 
                default:
                    Console.WriteLine("scelta non valida");
                    break;
            }

        }
    }

    private void ShowMainMenu()
    {
        _productView.ShowProductMenu();
        _categoryView.ShowCategoryMenu();
        _customerView.ShowClientMenu();
        _baseView.ShowEndMenu();
    }
    private void VisualizzaProdotti()
    {
        // Crea una lista vuota per memorizzare i prodotti

        var prodotti = new List<Prodotto>();
        // Apre il data reader all'interno di un blocco 'using'
        using (var reader = _model.CaricaProdotti())
        {
            // Cicla attraverso ogni riga nel data reader
            while (reader.Read())
            {
                // Crea un nuovo oggetto Prodotto e assegna i valori letti dal reader
                var prodotto = new Prodotto
                {
                    Id = Convert.ToInt32(reader["id"]),
                    Nome = reader["nome"].ToString(),
                    Prezzo = Convert.ToDecimal(reader["prezzo"]),
                    Quantita = Convert.ToInt32(reader["quantita"]),
                    Id_categoria = Convert.ToInt32(reader["id_categoria"])
                };
                // Aggiunge il prodotto alla lista
                prodotti.Add(prodotto);
            }
        }  // Fine del blocco 'using' il reader viene automaticamente chiuso e liberato qui

        // Passa la lista dei prodotti alla vista per visualizzarla
        _productView.VisualizzaProdotti(prodotti);
    }

    private void VisualizzaProdottiOrdinatiPerPrezzo() // Menu option 2
    {
        var prodottiOrdinati = new List<Prodotto>();

        using (var reader = _model.CaricaProdottiOrdinatiPerPrezzo())
        {
            while (reader.Read())
            {
                var prodotto = new Prodotto
                {
                    Id = Convert.ToInt32(reader["id"]),
                    Nome = reader["nome"]?.ToString() ?? "Nome sconosciuto",
                    Prezzo = Convert.ToDecimal(reader["prezzo"]),
                    Quantita = Convert.ToInt32(reader["quantita"]),
                    Id_categoria = Convert.ToInt32(reader["id_categoria"])
                };
                prodottiOrdinati.Add(prodotto);
            }
        }

        _productView.VisualizzaProdottiOrdinatiPerPrezzo(prodottiOrdinati);
    }

    private void VisualizzaProdottiOrdinatiPerQuantita() // Menu option 3
    {
        var prodottiOrdinati = new List<Prodotto>();

        using (var reader = _model.CaricaProdottiOrdinatiPerQuantita())
        {
            while (reader.Read())
            {
                var prodotto = new Prodotto
                {
                    Id = Convert.ToInt32(reader["id"]),
                    Nome = reader["nome"].ToString(),
                    Prezzo = Convert.ToDecimal(reader["prezzo"]),
                    Quantita = Convert.ToInt32(reader["quantita"]),
                    Id_categoria = Convert.ToInt32(reader["id_categoria"])
                };
                prodottiOrdinati.Add(prodotto);
            }
        }

        // Passa la lista dei prodotti ordinati per quantità alla vista
        _productView.VisualizzaProdottiOrdinatiPerQuantita(prodottiOrdinati);
    }

    private void ModificaPrezzoProdotto()    // Menu opzione 4
    {
        string nome = _productView.InserisciNomeProdotto();
        decimal prezzo = _productView.InserisciPrezzoProdotto();
        _model.ModificaPrezzoProdotto(nome, prezzo);    // Passa al Model il nome e il prezzo
    }

    public void EliminaProdotto()   // Menu opzione 5
    {
        string nome = _productView.InserisciNomeProdotto();
        _model.EliminaProdotto(nome);
    }

    private void VisualizzaProdottoPiuCostoso() // Menu opzione 6
    {
        Prodotto? prodotto = null; // Inizializza una variabile `Prodotto` come `null`  per indicare che al momento non è stato trovato alcun prodotto se lo trova verrà popolato

        // Esegue il caricamento del prodotto più costoso utilizzando un blocco `using` per gestire automaticamente la chiusura del `DataReader`
        using (var reader = _model.CaricaProdottoPiuCostoso())
        {
            // Verifica se ci sono risultati nel `DataReader`
            if (reader.Read())
            {

                prodotto = new Prodotto
                {
                    Id = Convert.ToInt32(reader["id"]),
                    Nome = reader["nome"]?.ToString() ?? "Nome sconosciuto",
                    Prezzo = Convert.ToDecimal(reader["prezzo"]),
                    Quantita = Convert.ToInt32(reader["quantita"]),
                    Id_categoria = Convert.ToInt32(reader["id_categoria"])
                };
            }
        } // chiude automaticamente il `DataReader`

        // Verifica se il prodotto più costoso è stato trovato
        if (prodotto != null)
        {
            // Se il prodotto esiste, passa l'oggetto `Prodotto` alla vista per visualizzare i dettagli
            _productView.VisualizzaProdottoPiuCostoso(prodotto);
        }
        else
        {
            // Se nessun prodotto è stato trovato, visualizza un messaggio di avviso
            _productView.Stampa("Nessun prodotto trovato.");
        }
    }

    private void VisualizzaProdottoMenoCostoso() // Menu opzione 7
    {
        Prodotto? prodotto = null;

        using (var reader = _model.CaricaProdottoMenoCostoso())
        {
            // Se esiste un record, popola l'oggetto `Prodotto`
            if (reader.Read())
            {
                prodotto = new Prodotto
                {
                    Id = Convert.ToInt32(reader["id"]),
                    Nome = reader["nome"]?.ToString() ?? "Nome sconosciuto",
                    Prezzo = Convert.ToDecimal(reader["prezzo"]),
                    Quantita = Convert.ToInt32(reader["quantita"]),
                    Id_categoria = Convert.ToInt32(reader["id_categoria"])
                };
            }
        }

        // Passa il prodotto alla vista se è stato trovato, altrimenti mostra un messaggio di avviso
        if (prodotto != null)
        {
            _productView.VisualizzaProdottoMenoCostoso(prodotto);
        }
        else
        {
            _productView.Stampa("Nessun prodotto trovato.");
        }
    }


    private void InserisciProdotto()    // Menu opzione 8
    {

        Console.WriteLine("inserisci il nome del prodotto");
        string nome = Console.ReadLine()!;
        Console.WriteLine("inserisci il prezzo del prodotto");
        decimal prezzo = Decimal.Parse(Console.ReadLine()!);
        Console.WriteLine("inserisci la quantità del prodotto");
        int quantita = Int32.Parse(Console.ReadLine()!);
        // Visualizza le categorie disponibili
        Console.WriteLine("Categorie disponibili:");
        _model.VisualizzaCategorie(); // Chiamata al metodo che visualizza le categorie con i loro ID
        Console.WriteLine("inserisci l'id della categoria del prodotto");
        int id_categoria = Int32.Parse(Console.ReadLine()!);
        _model.InserisciProdotto(nome, prezzo, quantita, id_categoria);
    }

    private void VisualizzaProdotto()  // Menu opzione 9
    {
        // Richiede il nome del prodotto dalla vista
        string nome = _productView.InserisciNomeProdotto();
        Prodotto? prodotto = null;
        // Recupera i dati del prodotto dal database
        using (var reader = _model.CaricaProdotto(nome))
        {
            // Se esiste un prodotto con il nome specificato, lo carica
            if (reader.Read())
            {
                prodotto = new Prodotto
                {
                    Id = Convert.ToInt32(reader["id"]),
                    Nome = reader["nome"]?.ToString() ?? "Nome sconosciuto",
                    Prezzo = Convert.ToDecimal(reader["prezzo"]),
                    Quantita = Convert.ToInt32(reader["quantita"]),
                    Id_categoria = Convert.ToInt32(reader["id_categoria"])
                };
            }
        }
        // Verifica se è stato trovato un prodotto e lo visualizza, altrimenti stampa un messaggio di errore
        if (prodotto != null)
        {
            _productView.VisualizzaProdotto(prodotto); // Passa il prodotto alla vista per la visualizzazione
        }
        else
        {
            _productView.Stampa("Prodotto non trovato.");
        }
    }

    // metodo per ottenere i prodotti in base alla categoria specificata
 /*   private void VisualizzaProdottiCategoria()    // Menu opzione 10
    {
        VisualizzaCategorie();
        int id_categoria = _productView.InserisciIdCategoria();
         // Ottiene i prodotti della categoria specificata
        var prodottiCategoria = new List<Prodotto>();
        using (var reader = _model.VisualizzaProdottiCategoria(id_categoria))
        {
            while (reader.Read())
            {
                var prodotto = new Prodotto
                {
                    Id = Convert.ToInt32(reader["id"]),
                    Nome = reader["nome"]?.ToString() ?? "Nome sconosciuto",
                    Prezzo = Convert.ToDecimal(reader["prezzo"]),
                    Quantita = Convert.ToInt32(reader["quantita"]),
                    Id_categoria = Convert.ToInt32(reader["id_categoria"])
                };
                prodottiCategoria.Add(prodotto);
            }
        }

        // Visualizza i prodotti della categoria usando `ProductView`
        _productView.VisualizzaProdottiCategoria(prodottiCategoria);
    }

    private void InserisciCategoria()   // Menu opzione 11
    {
        
        VisualizzaCategorie();
        Console.WriteLine("inserisci il nome della nuova categoria");
        string nome = Console.ReadLine()!;
        _model.InserisciCategoria(nome);
    }

    private void EliminaCategoria() // Menu opzione 12
    {
        
        VisualizzaCategorie();
        Console.WriteLine("inserisci il nome della categoria da eliminare");
        string nome = Console.ReadLine()!;
        _model.EliminaCategoria(nome);
    }

    public void InserisciProdottoCategoria()    // Menu opzione 13
    {
        // Chiama il metodo per visualizzare le categorie
        VisualizzaCategorie();
        //seleziona categoria
        // Chiede l'inserimento dell'ID categoria
        int id_categoria = _productView.InserisciIdCategoria();
        //inserisci prodotto
        string nome = _productView.InserisciNomeProdotto();
        decimal prezzo = _productView.InserisciPrezzoProdotto();
        int quantita = _productView.InserisciQuantitaProdotto();
        _model.InserisciProdottoCategoria(id_categoria, nome, prezzo, quantita);
    }
    private void VisualizzaCategorie() 
    {
        var reader = _model.VisualizzaCategorie();
        List<Categoria> categorie = new List<Categoria>();
        while (reader.Read())   // Visualizza ogni categoria con ID e nome
        {
            var categoria = new Categoria
            {
                Id = Convert.ToInt32(reader["id"]),
                Nome = reader["nome"]?.ToString() ?? "Nome sconosciuto"
            };
            categorie.Add(categoria);
        }
        _categoryView.VisualizzaCategorie(categorie);
    }  */

/*    private void InserisciCliente()    // Menu opzione 14
    {
        Cliente cliente = new Cliente();
        cliente.Nome = _clienteView.InserisciCliente();
        _model.InserisciCliente(cliente);
    }

    public void VisualizzaClienti() // Menu opzione 15
    {
        // Crea una lista vuota per memorizzare i prodotti

        var clienti = new List<Cliente>();
        // Apre il data reader all'interno di un blocco 'using'
        using (var reader = _model.VisualizzaClienti())
        {
            // Cicla attraverso ogni riga nel data reader
            while (reader.Read())
            {
                // Crea un nuovo oggetto Prodotto e assegna i valori letti dal reader
                var cliente = new Cliente
                {
                    Id = Convert.ToInt32(reader["id"]),
                    Nome = reader["nome"].ToString(),
                };
                // Aggiunge il prodotto alla lista
                clienti.Add(cliente);
            }
        }  
        _clienteView.VisualizzaClienti(clienti);
    }

    private void ModificaCliente()    // Menu opzione 16
    {
        VisualizzaClienti();
        var (id, nuovoNome) = _clienteView.ModificaCliente();
        Cliente cliente = new Cliente { Id = id }; // Create the Cliente object with the id
        _model.ModificaCliente(cliente, nuovoNome);
    }

    private void EliminaCliente()   // Menu opzione 17
    {
        Cliente cliente = new Cliente();
        VisualizzaClienti();
        cliente.Id = _clienteView.EliminaCliente();
        _model.EliminaCliente(cliente);
    } */
}