public class ProductController
{
    private Model _model;
    private ProductView _productView;

    private CategoryController _categoryController;

    public ProductController(Model model, ProductView productView, CategoryController categoryController)
    {
        _model = model;
        _productView = productView;
        _categoryController = categoryController;
    }

    public void ProductsMenu()
    {
        while(true)
        {
            _productView.ShowProductMenu();
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
                    VisualizzaProdottiCategoria();
                    break;
                case "11":
                    InserisciProdottoCategoria();
                    break;
                case "12":
                    return;
                default:
                    Console.WriteLine("scelta non valida");
                    break;
            }
        }
    }

    private void VisualizzaProdotti()
{
    // Crea una lista vuota per memorizzare i prodotti
    var prodotti = _model.CaricaProdotti();

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
                    Giacenza = Convert.ToInt32(reader["giacenza"]),
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
                    Giacenza = Convert.ToInt32(reader["giacenza"]),
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
                    Giacenza = Convert.ToInt32(reader["giacenza"]),
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
                    Giacenza = Convert.ToInt32(reader["giacenza"]),
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
        int giacenza = Int32.Parse(Console.ReadLine()!);
        // Visualizza le categorie disponibili
        Console.WriteLine("Categorie disponibili:");
        _model.VisualizzaCategorie(); // Chiamata al metodo che visualizza le categorie con i loro ID
        Console.WriteLine("inserisci l'id della categoria del prodotto");
        int id_categoria = Int32.Parse(Console.ReadLine()!);
        _model.InserisciProdotto(nome, prezzo, giacenza, id_categoria);
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
                    Giacenza = Convert.ToInt32(reader["giacenza"]),
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
    private void VisualizzaProdottiCategoria()    // Menu opzione 10
    {
        _categoryController.VisualizzaCategorie();
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
                    Giacenza = Convert.ToInt32(reader["giacenza"]),
                    Id_categoria = Convert.ToInt32(reader["id_categoria"])
                };
                prodottiCategoria.Add(prodotto);
            }
        }

        // Visualizza i prodotti della categoria usando `ProductView`
        _productView.VisualizzaProdottiCategoria(prodottiCategoria);
    }

     public void InserisciProdottoCategoria()    // Menu opzione 13
    {
        // Chiama il metodo per visualizzare le categorie
        _categoryController.VisualizzaCategorie();
        //seleziona categoria
        // Chiede l'inserimento dell'ID categoria
        int id_categoria = _productView.InserisciIdCategoria();
        //inserisci prodotto
        string nome = _productView.InserisciNomeProdotto();
        decimal prezzo = _productView.InserisciPrezzoProdotto();
        int quantita = _productView.InserisciQuantitaProdotto();
        _model.InserisciProdottoCategoria(id_categoria, nome, prezzo, quantita);
    }
}