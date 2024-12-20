using Microsoft.EntityFrameworkCore;

public class ProductController
{
    // Riferimenti al Database e alla vista per gestire i dati e l'interfaccia dei prodotti
    //private Model _model;

    private Database _database;
    private ProductView _productView;

    private CategoryController _categoryController;   // Riferimento al controller delle categorie

    // Costruttore del controller dei prodotti
    public ProductController(Database database, ProductView productView, CategoryController categoryController)
    {
        _database = database;
        _productView = productView;
        _categoryController = categoryController;
    }

    // Metodo principale per gestire il menu dei prodotti
    public void ProductsMenu()
    {
        Console.Clear();
        while (true)
        {
            _productView.ShowProductMenu();  //richiama il metodo ShowProductMenu dalla view del prodotto
            var input = _productView.GetInput(); //richiama l'input
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
                    ModificaGiacenzaProdotto();
                    break;
                case "6":
                    EliminaProdotto();
                    break;
                case "7":
                    VisualizzaProdottoPiuCostoso();
                    break;
                case "8":
                    VisualizzaProdottoMenoCostoso();
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
                    _productView.Errore();
                    break;
            }
            if (input != "12")
            {
                _productView.Proseguimento();
            }
        }
    }
    //metodo VisualizzaProdotti nuovo  entity
    public void VisualizzaProdotti()
    {
        // Ottiene tutti i prodotti dal database, inclusa la categoria associata
        List<Prodotto> prodotti = _database.Prodotti.Include(nameof(Prodotto.Categoria)).ToList(); //Serve per fargli tirare su anche gli oggetti correlati

        // Passa la lista dei prodotti alla vista per visualizzarla
        _productView.VisualizzaProdotti(prodotti);
    }

    //  Menu opzione 2 metodo per visualizzare i prodotti sortati per prezzo
    private void VisualizzaProdottiOrdinatiPerPrezzo()
    {
        // Ottiene tutti i prodotti ordinati per prezzo, inclusa la categoria associata
        var prodottiOrdinati = _database.Prodotti
            .Include(nameof(Prodotto.Categoria))   // Include la categoria associata
            .ToList();

        for (int i = 0; i < prodottiOrdinati.Count - 1; i++)
        {
            for (int j = i + 1; j < prodottiOrdinati.Count; j++)
            {
                if (prodottiOrdinati[i].Prezzo > prodottiOrdinati[j].Prezzo)
                {
                    // Scambia i prodotti se il prezzo di i è maggiore del prezzo di j
                    var temp = prodottiOrdinati[i];
                    prodottiOrdinati[i] = prodottiOrdinati[j];
                    prodottiOrdinati[j] = temp;
                }
            }
        }
        // Passa la lista ordinata dei prodotti alla vista per visualizzarla
        _productView.VisualizzaProdottiOrdinatiPerPrezzo(prodottiOrdinati);
    }

    // Menu opzione 3 metodo per visualizzare i prodotti in base alla quantita
    private void VisualizzaProdottiOrdinatiPerQuantita()
    {
        // Ottiene tutti i prodotti ordinati per quantità (giacenza), inclusa la categoria associata
        List<Prodotto> prodottiOrdinati = _database.Prodotti.Include(nameof(Prodotto.Categoria)).ToList(); //Serve per fargli tirare su anche gli oggetti correlati
        for (int i = 0; i < prodottiOrdinati.Count - 1; i++)
        {
            for (int j = i + 1; j < prodottiOrdinati.Count; j++)
            {
                if (prodottiOrdinati[i].Giacenza > prodottiOrdinati[j].Giacenza)
                {
                    // Scambia i prodotti se il prezzo di i è maggiore del prezzo di j
                    var temp = prodottiOrdinati[i];
                    prodottiOrdinati[i] = prodottiOrdinati[j];
                    prodottiOrdinati[j] = temp;
                }
            }
        }
        // Passa la lista ordinata dei prodotti alla vista per visualizzarla
        _productView.VisualizzaProdottiOrdinatiPerQuantita(prodottiOrdinati);
    }

    // Metodo per modificare il prezzo di un prodotto specifico con Entity Framework
    //Menu Opzione 4
    private void ModificaPrezzoProdotto()
    {
        VisualizzaProdotti();
        // Richiede il nome e il nuovo prezzo del prodotto dalla vista
        int id = _productView.InserisciIdProdotto();
        //trova il prodotto nel database
        var prodotti = _database.Prodotti.ToList();

        Prodotto? prodotto = null;
        // Scorri la lista di prodotti per trovare quello con l'ID specificato
        foreach (var prod in prodotti)
        {
            if (prod.Id == id)
            {
                prodotto = prod;
            }
        }
        // Se il prodotto esiste aggiorna il prezzo

        if (prodotto != null)
        {
            double nuovoPrezzo = _productView.InserisciPrezzoProdotto();
            prodotto.Prezzo = (decimal)nuovoPrezzo;  // Imposta il nuovo prezzo
            _database.SaveChanges();   //salva le modifiche nel database
            _productView.Stampa("Prezzo aggiornato con successo.");
        }
        else
        {
            _productView.Stampa("Prodotto non trovato.");
        }
    }

    private void ModificaGiacenzaProdotto()
    {
        VisualizzaProdotti();
        // Richiede il nome e il nuovo prezzo del prodotto dalla vista
        int id = _productView.InserisciIdProdotto();
        var prodotti = _database.Prodotti.ToList();

        Prodotto? prodotto = null;
        // Scorri la lista di prodotti per trovare quello con l'ID specificato
        foreach (var prod in prodotti)
        {
            if (prod.Id == id)
            {
                prodotto = prod;
            }
        }
        // Se il prodotto esiste aggiorna il prezzo

        if (prodotto != null)
        {
            int nuovaGiacenza = _productView.InserisciQuantitaProdotto();
            prodotto.Giacenza = nuovaGiacenza;  //// Imposta il nuovo prezzo
            _database.SaveChanges();   //salva le modifiche nel database
            _productView.Stampa("Giacenza aggiornata con successo.");
        }
        else
        {
            _productView.Stampa("Prodotto non trovato.");
        }
    }

    // Menu opzione 5 Metodo per eliminare un prodotto con Entity Framework
    public void EliminaProdotto()
    {
        VisualizzaProdotti();
        // Richiede il nome del prodotto da eliminare
        int id = _productView.InserisciIdProdotto();
        var prodotti = _database.Prodotti.ToList();

        Prodotto? prodotto = null;
        // Scorri la lista di prodotti per trovare quello con l'ID specificato
        foreach (var prod in prodotti)
        {
            if (prod.Id == id)
            {
                prodotto = prod;
            }
        }

        // Se il prodotto esiste, lo elimina
        if (prodotto != null)
        {
            _database.Prodotti.Remove(prodotto);   //rimuove il prodotto dal dbset
            _database.SaveChanges();            //salva le modifiche nel database
            _productView.Stampa("Prodotto eliminato con successo.");
        }
        else
        {
            _productView.Stampa("Prodotto non trovato.");
        }
    }

    //Menu opzione 6 metodo per visualizzare il prodotto più costoso
    private void VisualizzaProdottoPiuCostoso() // Menu opzione 6
    {
        // Trova il prodotto con il prezzo più alto nel database
        // Include in questo caso include i dettagli della categoria
        //.FirstOrDefault(); in questo caso prende il primo prodotto con il prezzo più alto
        var prodotti = _database.Prodotti.Include(nameof(Prodotto.Categoria)).ToList(); //Serve per fargli tirare su anche gli oggetti correlati

        // Ordina manualmente i prodotti per prezzo in ordine decrescente
        var prodottoMaxPrezzo = prodotti.First();
        foreach (var prodotto in prodotti)
        {
            if (prodotto.Prezzo > prodottoMaxPrezzo.Prezzo)
            {
                prodottoMaxPrezzo = prodotto;
            }
        }
        // Seleziona il primo prodotto ordinato
        // Verifica se il prodotto più costoso è stato trovato
        if (prodottoMaxPrezzo != null)
        {
            // Se il prodotto esiste passa l'oggetto `Prodotto` alla vista per visualizzare i dettagli
            _productView.VisualizzaProdottoPiuCostoso(prodottoMaxPrezzo);
        }
        else
        {
            // Se nessun prodotto è stato trovato, visualizza un messaggio di avviso
            _productView.Stampa("Nessun prodotto trovato.");
        }
    }

    //Menu opzione 7 metodo per visualizzare il prodotto meno costoso
    private void VisualizzaProdottoMenoCostoso() // Menu opzione 7
    {
        // Trova il prodotto con il prezzo più basso nel database
        //.OrderBy ordina i prodotti dal prezzo più basso a quello più alto
        var prodotti = _database.Prodotti.Include(nameof(Prodotto.Categoria)).ToList(); //Serve per fargli tirare su anche gli oggetti correlati

        // Ordina manualmente i prodotti per prezzo in ordine decrescente
        var prodottoMinPrezzo = prodotti.First();
        foreach (var prodotto in prodotti)
        {
            if (prodotto.Prezzo < prodottoMinPrezzo.Prezzo)
            {
                prodottoMinPrezzo = prodotto;
            }
        }

        // Seleziona il primo prodotto ordinato

        // Verifica se il prodotto più costoso è stato trovato
        if (prodottoMinPrezzo != null)
        {
            // Se il prodotto esiste passa l'oggetto `Prodotto` alla vista per visualizzare i dettagli
            _productView.VisualizzaProdottoMenoCostoso(prodottoMinPrezzo);
        }
        else
        {
            // Se nessun prodotto è stato trovato, visualizza un messaggio di avviso
            _productView.Stampa("Nessun prodotto trovato.");
        }
    }

    //Menu opzione 8 metodo per visualizzare il singolo prodotto
    private void VisualizzaProdotto()  // Menu opzione 8
    {
        // Richiede il nome del prodotto dalla vista
        string nome = _productView.InserisciNomeProdotto();
        // Recupera i dati del prodotto dal database inclusa la categoria associata
        var prodotti = _database.Prodotti.Include(nameof(Prodotto.Categoria)).ToList(); //Serve per fargli tirare su anche gli oggetti correlati

        Prodotto? prodotto = null;
        // Scorri la lista di prodotti per trovare quello con l'ID specificato
        foreach (var prod in prodotti)
        {
            if (prod.Nome == nome)
            {
                prodotto = prod;
            }
        }
        // Verifica se è stato trovato un prodotto e lo visualizza altrimenti stampa un messaggio di errore
        if (prodotto != null)
        {
            _productView.VisualizzaProdotto(prodotto);
        }
        else
        {
            _productView.Stampa("Prodotto non trovato.");
        }
    }

    //Menu opzione 9 metodo per visualizzare tutti i prodotti associati ad una categoria specifica
    private void VisualizzaProdottiCategoria()    // Menu opzione 9
    {
        // visualizza le categorie disponibili
        _categoryController.VisualizzaCategorie();
        // Richiede l'ID della categoria
        int id_categoria = _productView.InserisciIdCategoria();

        // Ottiene i prodotti della categoria specificata, includendo i dettagli della categoria
        //Where(p => p.Id_categoria == id_categoria) filtra i prodotti in base all'ID della categoria specificata
        //ToList() esegue la query e converte i risultati in una lista da passare alla view
        var prodottiCategoria = _database.Prodotti.Include(nameof(Prodotto.Categoria)).ToList(); //Serve per fargli tirare su anche gli oggetti correlati
        foreach (Prodotto prod in prodottiCategoria)
        {
            if (prod.Categoria!.Id != id_categoria) prodottiCategoria.Remove(prod);
        }

        // Visualizza i prodotti della categoria usando `ProductView`
        _productView.VisualizzaProdottiCategoria(prodottiCategoria);
    }

    // Menu opzione 10 metodo per inserire il prodotto
    public void InserisciProdottoCategoria() // Menu opzione 10
    {

        // Verifica se esistono categorie nel database
        if (!_database.Categorie.Any())
        {
            _productView.Stampa("Non ci sono categorie disponibili. Inserisci una categoria prima di aggiungere un prodotto.");
            return; // Termina l'esecuzione del metodo
        }
        // Mostra le categorie disponibili
        _categoryController.VisualizzaCategorie();

        // Richiede l'ID della categoria da associare al nuovo prodotto
        int id_categoria = _productView.InserisciIdCategoria();


        // Cerca la categoria nel database per verificare che esista
        // quindi confronta l’ID di ciascuna Categoria con l’id_categoria fornito dall’utente
        //Se una categoria con Id corrispondente a id_categoria esiste FirstOrDefault restituirà quella categoria


        var categorie = _database.Categorie.ToList();

        Categoria? categoria = null;
        // Scorri la lista di prodotti per trovare quello con l'ID specificato
        foreach (var cat in categorie)
        {
            if (cat.Id == id_categoria)
            {
                categoria = cat;
            }
        }
        if (categoria != null)
        {
            string nome = _productView.InserisciNomeProdotto();
            double prezzo = _productView.InserisciPrezzoProdotto();
            int quantita = _productView.InserisciQuantitaProdotto();
            // Crea un nuovo prodotto con le informazioni fornite dall'utente
            var nuovoProdotto = new Prodotto
            {
                Nome = nome,
                Prezzo = (decimal)prezzo,
                Giacenza = quantita,
                Categoria = categoria, // Associa la categoria esistente
                Id_categoria = id_categoria
            };
            // Aggiunge il nuovo prodotto al database e salva le modifiche
            _database.Prodotti.Add(nuovoProdotto);
            _database.SaveChanges();

            // Conferma l'inserimento alla vista
            _productView.Stampa("Prodotto inserito con successo!");
        }
        else
        {
            _productView.Stampa("Categoria non trovata. Inserimento annullato.");
        }
    }
}