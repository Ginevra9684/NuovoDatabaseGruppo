public class CategoryController
{

    // Riferimenti ai modelli e alla vista, iniettati tramite il costruttore
    private Database _database;
    private CategoryView _categoryView;

    // Costruttore che inizializza il controller con il modello e la vista
    public CategoryController(Database database, CategoryView categoryView )
    {
        _database = database;
        _categoryView = categoryView;
    }

     // Metodo per gestire il menu delle categorie

    public void CategoryMenu()
    {
        Console.Clear();
        while(true)
        {
            _categoryView.ShowCategoryMenu();
            var input = _categoryView.GetInput();
            switch (input)
            {
                case "1":
                    VisualizzaCategorie();
                    break;
                case "2":
                    InserisciCategoria();
                    break;
                case "3":
                    EliminaCategoria();
                    break;
                case "4":
                    return;
                default:
                    _categoryView.Errore();
                    break;
            }
            if (input != "4")
            {
                _categoryView.Proseguimento();
            }
        }    
    }
/*
  // Visualizza tutte le categorie (Opzione 1 nel menu)
    public void VisualizzaCategorie()   // Menu opzione 1
    {
        using var reader = _model.VisualizzaCategorie(); // Ottiene i dati delle categorie dal modello
        var categorie = new List<Categoria>();

        // Crea una lista di oggetti Categoria dai dati recuperati dal database
        while (reader.Read())
        {
            var categoria = new Categoria
            {
                Id = Convert.ToInt32(reader["id"]),  // Converte il valore dell'ID  in un intero e lo assegna alla proprietà Id
                // Assegna il valore del nome della categoria controllando per eventuali valori null
                Nome = reader["nome"]?.ToString() ?? "Nome sconosciuto"  
            };
            categorie.Add(categoria);
        }
        // Passa la lista completa di categorie alla vista per la visualizzazione
        _categoryView.VisualizzaCategorie(categorie);
    }  */


    // Visualizza tutte le categorie (Opzione 1 nel menu)
    public void VisualizzaCategorie()   // Menu opzione 1
    {
        // Ottiene tutte le categorie dal database e le converte in una lista
        var categorie = _database.Categorie.ToList();

        // Passa la lista completa di categorie alla vista per la visualizzazione
        _categoryView.VisualizzaCategorie(categorie);
    }

/*
    public void InserisciCategoria()    // Menu opzione 2
    {
        _categoryView.VisualizzaCategorie(new List<Categoria>());  // Visualizza le categorie attuali
        string nome = _categoryView.InserisciNomeCategoria();  // Usa la view per ottenere il nome
        _model.InserisciCategoria(nome); //inserisce la categoria nel database tramite il modello
    }

    */

//Metodo per inserire una  categoria al databse
    public void InserisciCategoria()    // Menu opzione 2
    {
        // Visualizza le categorie attuali (opzionale)
        VisualizzaCategorie();

        // Ottiene il nome della nuova categoria dalla vista
        string nome = _categoryView.InserisciNomeCategoria();

        // Crea una nuova istanza di Categoria con il nome inserito
        var nuovaCategoria = new Categoria { Nome = nome };

        // Aggiunge la nuova categoria al database
        _database.Categorie.Add(nuovaCategoria);
        _database.SaveChanges();  // Salva i cambiamenti nel database

        // Conferma l'inserimento
        _categoryView.Stampa("Categoria inserita con successo.");
    }

/*

// Metodo per eliminare una categoria esistente (Opzione 3 nel menu)
    public void EliminaCategoria()  // Menu opzione 3
    {
        VisualizzaCategorie();  // Mostra le categorie disponibili
        string nome = _categoryView.InserisciNomeCategoria();  // Usa la view per ottenere il nome da eliminare
        _model.EliminaCategoria(nome);    // Rimuove la categoria dal database tramite il modello
    } */

// Menu opzione 3 Metodo per eliminare la categoria
/*
    public void EliminaCategoria()  // Menu opzione 3
    {
        // Visualizza le categorie disponibili per aiutare l'utente a selezionare quella corretta
        VisualizzaCategorie();

        // Ottiene il nome della categoria da eliminare
        string nome = _categoryView.InserisciNomeCategoria();

        // Cerca la categoria nel database in base al nome
        //esamina tutti gli elementi in Categorie e cerca il primo elemento che soddisfa la condizione specificata (che il nome della categoria corrisponda al nome fornito)

        var categoria = _database.Categorie.FirstOrDefault( c => c.Nome == nome);

        // Verifica se la categoria esiste

        if(categoria != null){
            //rimuove la categoria trovata dal database

            _database.Categorie.Remove(categoria);
            _database.SaveChanges();
            _categoryView.Stampa("Categoria eliminata con successo.");
        }
        else
        {
            // Visualizza un messaggio di errore se la categoria non è stata trovata
            _categoryView.Stampa("Categoria non trovata.");
        }
    }
*/
}
