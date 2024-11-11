public class CategoryController
{
    // Riferimenti ai modelli e alla vista, iniettati tramite il costruttore
    private Database _database;
    private CategoryView _categoryView;

    // Costruttore che inizializza il controller con il modello e la vista
    public CategoryController(Database database, CategoryView categoryView)
    {
        _database = database;
        _categoryView = categoryView;
    }

    // Metodo per gestire il menu delle categorie

    public void CategoryMenu()
    {
        Console.Clear();
        while (true)
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
                    ModificaCategoria();
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

    // Visualizza tutte le categorie (Opzione 1 nel menu)
    public void VisualizzaCategorie()   // Menu opzione 1
    {
        // Ottiene tutte le categorie dal database e le converte in una lista
        var categorie = _database.Categorie.ToList();

        // Passa la lista completa di categorie alla vista per la visualizzazione
        _categoryView.VisualizzaCategorie(categorie);
    }

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

    private void ModificaCategoria()
    {
        VisualizzaCategorie();
        int id = _categoryView.InserisciIdCategoria();

        Categoria? categoria = TrovaCategoriaPerId(id);;
        // foreach (var c in _database.Categorie)
        // {
        //     if (c.Id == id)
        //     {
        //         categoria = c;
        //         break;
        //     }
        // }

        // Se il prodotto esiste aggiorna il prezzo

        if (categoria != null)
        {
            string nuovoNome = _categoryView.InserisciNomeCategoria();
            categoria.Nome = nuovoNome;  //// Imposta il nuovo prezzo
            _database.SaveChanges();   //salva le modifiche nel database
            _categoryView.Stampa("Nome aggiornato con successo.");
        }
        else
        {
            _categoryView.Stampa("Categoria non trovato.");
        }
    }

    private Categoria? TrovaCategoriaPerId(int id)
    {
        foreach (var categoria in _database.Categorie)
        {
            if (categoria.Id == id)
                return categoria;
        }
        return null;
    }
}
