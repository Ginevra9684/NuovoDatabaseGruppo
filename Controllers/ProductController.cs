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
                    EliminaProdotto();
                    break;
                case "6":
                    VisualizzaProdottoPiuCostoso();
                    break;
                case "7":
                    VisualizzaProdottoMenoCostoso();
                    break;
                case "8":
                    VisualizzaProdotto();
                    break;
                case "9":
                    VisualizzaProdottiCategoria();
                    break;
                case "10":
                    InserisciProdottoCategoria();
                    break;
                case "11":
                    return;
                default:
                    Console.WriteLine("scelta non valida");
                    break;
            }
        }
    }
    //metodo VisualizzaProdotti nuovo  entity
    public void VisualizzaProdotti()
    {
        // Ottiene tutti i prodotti dal database, inclusa la categoria associata
        var prodotti = _database.Prodotti.Include(p => p.Categoria).ToList();

        // Passa la lista dei prodotti alla vista per visualizzarla
        _productView.VisualizzaProdotti(prodotti);
    }

    /*
    //Metodo Menu opzione 1
        private void VisualizzaProdotti()
    {
        // Crea una lista vuota per memorizzare i prodotti
        var prodotti = _model.CaricaProdotti();

        // Passa la lista dei prodotti alla vista per visualizzarla
        _productView.VisualizzaProdotti(prodotti);
    }
    */
    //Metodo Menu opzione 2 per visualizzare i prodotti ordinati per prezzo
    /*  private void VisualizzaProdottiOrdinatiPerPrezzo() // Menu option 2
    {
        var prodottiOrdinati = new List<Prodotto>();
    // Usa un `DataReader` per leggere i prodotti ordinati per prezzo
        using (var reader = _model.CaricaProdottiOrdinatiPerPrezzo())
        {
              // Legge ogni prodotto e crea un oggetto `Prodotto` per ciascuno
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
                prodottiOrdinati.Add(prodotto);  // Aggiunge il prodotto alla lista ordinata
            }
        }

          _productView.VisualizzaProdottiOrdinatiPerPrezzo(prodottiOrdinati);   // Visualizza la lista ordinata nella vista
    }*/

//  Menu opzione 2 metodo per visualizzare i prodotti sortati per prezzo
    private void VisualizzaProdottiOrdinatiPerPrezzo()
    {
        // Ottiene tutti i prodotti ordinati per prezzo, inclusa la categoria associata
        var prodottiOrdinati = _database.Prodotti
                                        .Include(p => p.Categoria)   // Include la categoria associata
                                        .OrderBy(p => p.Prezzo)       // Ordina per prezzo
                                        .ToList();

        // Passa la lista ordinata dei prodotti alla vista per visualizzarla
        _productView.VisualizzaProdottiOrdinatiPerPrezzo(prodottiOrdinati);
    }

    /*
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
    */

    // Menu opzione 3 metodo per visualizzare i prodotti in base alla quantita
    private void VisualizzaProdottiOrdinatiPerQuantita()
    {
        // Ottiene tutti i prodotti ordinati per quantità (giacenza), inclusa la categoria associata
        var prodottiOrdinati = _database.Prodotti.Include(p => p.Categoria).OrderBy(p => p.Giacenza).ToList();
        // Passa la lista ordinata dei prodotti alla vista per visualizzarla
        _productView.VisualizzaProdottiOrdinatiPerQuantita(prodottiOrdinati);
    }

/*
    //Menu Opzione 4 Metodo per modificare il prezzo di un prodotto specifico
    private void ModificaPrezzoProdotto()    // Menu opzione 4
    {
        string nome = _productView.InserisciNomeProdotto();
        decimal prezzo = _productView.InserisciPrezzoProdotto();
        _model.ModificaPrezzoProdotto(nome, prezzo);    // Passa al Model il nome e il prezzo
    }*/


// Metodo per modificare il prezzo di un prodotto specifico con Entity Framework
//Menu Opzione 4
    private void ModificaPrezzoProdotto()
    {
        // Richiede il nome e il nuovo prezzo del prodotto dalla vista
        string nome = _productView.InserisciNomeProdotto();
        double nuovoPrezzo = _productView.InserisciPrezzoProdotto();
        //trova il prodotto nel database
        var prodotto = _database.Prodotti.FirstOrDefault(p => p.Nome == nome);
        // Se il prodotto esiste aggiorna il prezzo

        if(prodotto != null)
        {
            prodotto.Prezzo = nuovoPrezzo;  //// Imposta il nuovo prezzo
            _database.SaveChanges();   //salva le modifiche nel database
            _productView.Stampa("Prezzo aggiornato con successo.");
        }
        else
        {
            _productView.Stampa("Prodotto non trovato.");
        }
    }

/*
    public void EliminaProdotto()   // Menu opzione 5
    {
        string nome = _productView.InserisciNomeProdotto();
        _model.EliminaProdotto(nome);
    }  */


    // Menu opzione 5 Metodo per eliminare un prodotto con Entity Framework
    public void EliminaProdotto()
    {
        // Richiede il nome del prodotto da eliminare
        string nome = _productView.InserisciNomeProdotto();
        //trova il prodotto nel database
        var prodotto = _database.Prodotti.FirstOrDefault(p => p.Nome == nome);

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
/*
// Metodo per trovare il prodotto con il prezzo più alto nel database
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
*/

//Menu opzione 6 metodo per visualizzare il prodotto più costoso
    private void VisualizzaProdottoPiuCostoso() // Menu opzione 6
    {
        // Trova il prodotto con il prezzo più alto nel database
        // Include in questo caso include i dettagli della categoria
        //.FirstOrDefault(); in questo caso prende il primo prodotto con il prezzo più alto
        var prodotto = _database.Prodotti.OrderByDescending(p => p.Prezzo).Include(p => p.Categoria).FirstOrDefault();
        // Verifica se il prodotto più costoso è stato trovato
        if (prodotto != null)
        {
            // Se il prodotto esiste passa l'oggetto `Prodotto` alla vista per visualizzare i dettagli
            _productView.VisualizzaProdottoPiuCostoso(prodotto);
        }
        else
        {
            // Se nessun prodotto è stato trovato, visualizza un messaggio di avviso
            _productView.Stampa("Nessun prodotto trovato.");
        }
    }


/*

    // Metodo per visualizzare il prodotto meno costoso
    private void VisualizzaProdottoMenoCostoso() // Menu opzione 7
    {
        Prodotto? prodotto = null;       // Inizializza una variabile per indicare che inizialmente non è stato trovato ancora alcun prodotto 

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
    }  */

    //Menu opzione 7 metodo per visualizzare il prodotto meno costoso
    private void VisualizzaProdottoMenoCostoso() // Menu opzione 7
    {
        // Trova il prodotto con il prezzo più basso nel database
        //.OrderBy ordina i prodotti dal prezzo più basso a quello più alto
        var prodotto = _database.Prodotti.OrderBy(p => p.Prezzo).Include(p => p.Categoria) .FirstOrDefault();

        // Verifica se il prodotto meno costoso è stato trovato
        if (prodotto != null)
        {
            // Se il prodotto esiste passa l'oggetto `Prodotto` alla vista per visualizzare i dettagli
            _productView.VisualizzaProdottoMenoCostoso(prodotto);
        }
        else
        {
            // Se nessun prodotto è stato trovato, visualizza un messaggio di avviso
            _productView.Stampa("Nessun prodotto trovato.");
        }
    }


    /*
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
    */
/*
    //Metodo Visualizzaprodotto Menu 8
    private void VisualizzaProdotto()  // Menu opzione 8
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
    } */

//Menu opzione 8 metodo per visualizzare il singolo prodotto
    private void VisualizzaProdotto()  // Menu opzione 8
    {
        // Richiede il nome del prodotto dalla vista
        string nome = _productView.InserisciNomeProdotto();
        // Recupera i dati del prodotto dal database inclusa la categoria associata
        var prodotto = _database.Prodotti.Include(p => p.Categoria).FirstOrDefault(p => p.Nome == nome);// Cerca il prodotto con il nome specificato

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

/*
    // metodo per ottenere i prodotti in base alla categoria specificata Menu 9
    private void VisualizzaProdottiCategoria()    // Menu opzione 9
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
*/

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
        var prodottiCategoria = _database.Prodotti.Include(p => p.Categoria).Where(p=>p.Id_categoria ==id_categoria).ToList();

    // Visualizza i prodotti della categoria usando `ProductView`
        _productView.VisualizzaProdottiCategoria(prodottiCategoria);
    }

/*
    // Menu opzione 10: Metodo per inserire un nuovo prodotto in una categoria specifica
    public void InserisciProdottoCategoria()    // Menu opzione 10
    {
        // Chiama il metodo per visualizzare le categorie
        _categoryController.VisualizzaCategorie();
        //seleziona categoria
        // Chiede all'utente di inserire l'ID della categoria selezionata per associare il nuovo prodotto
        // a una specifica categoria.
        int id_categoria = _productView.InserisciIdCategoria();
        // Chiede all'utente di inserire il nome del nuovo prodotto
        string nome = _productView.InserisciNomeProdotto();
        decimal prezzo = _productView.InserisciPrezzoProdotto();
        int quantita = _productView.InserisciQuantitaProdotto();
        _model.InserisciProdottoCategoria(id_categoria, nome, prezzo, quantita);
    }  */

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
        string nome = _productView.InserisciNomeProdotto();
        double prezzo = _productView.InserisciPrezzoProdotto();
        int quantita = _productView.InserisciQuantitaProdotto();

        // Cerca la categoria nel database per verificare che esista
        // quindi confronta l’ID di ciascuna Categoria con l’id_categoria fornito dall’utente
        //Se una categoria con Id corrispondente a id_categoria esiste FirstOrDefault restituirà quella categoria

        var categoria = _database.Categorie.FirstOrDefault(c => c.Id == id_categoria);

        if (categoria != null)
        {
            // Crea un nuovo prodotto con le informazioni fornite dall'utente
            var nuovoProdotto = new Prodotto{
                Nome = nome,
                Prezzo = prezzo,
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