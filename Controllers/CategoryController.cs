public class CategoryController
{

    // Riferimenti ai modelli e alla vista, iniettati tramite il costruttore
    private Model _model;
    private CategoryView _categoryView;

    // Costruttore che inizializza il controller con il modello e la vista
    public CategoryController(Model model, CategoryView categoryView )
    {
        _model = model;
        _categoryView = categoryView;
    }

     // Metodo per gestire il menu delle categorie

    public void CategoryMenu()
    {
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
                    _categoryView.Stampa("Opzione non valida");
                    break;
            }
        }    
    }

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
    }

    public void InserisciCategoria()    // Menu opzione 2
    {
        _categoryView.VisualizzaCategorie(new List<Categoria>());  // Visualizza le categorie attuali
        string nome = _categoryView.InserisciNomeCategoria();  // Usa la view per ottenere il nome
        _model.InserisciCategoria(nome); //inserisce la categoria nel database tramite il modello
    }

// Metodo per eliminare una categoria esistente (Opzione 3 nel menu)
    public void EliminaCategoria()  // Menu opzione 3
    {
        VisualizzaCategorie();  // Mostra le categorie disponibili
        string nome = _categoryView.InserisciNomeCategoria();  // Usa la view per ottenere il nome da eliminare
        _model.EliminaCategoria(nome);    // Rimuove la categoria dal database tramite il modello
    }

}
