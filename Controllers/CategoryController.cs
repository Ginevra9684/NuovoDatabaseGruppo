public class CategoryController
{
    private Model _model;
    private CategoryView _categoryView;
    public CategoryController(Model model, CategoryView categoryView )
    {
        _model = model;
        _categoryView = categoryView;
    }

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

    public void VisualizzaCategorie()   // Menu opzione 1
    {
        using var reader = _model.VisualizzaCategorie();
        var categorie = new List<Categoria>();
        while (reader.Read())
        {
            var categoria = new Categoria
            {
                Id = Convert.ToInt32(reader["id"]),
                Nome = reader["nome"]?.ToString() ?? "Nome sconosciuto"
            };
            categorie.Add(categoria);
        }
        _categoryView.VisualizzaCategorie(categorie);
    }

    public void InserisciCategoria()    // Menu opzione 2
    {
        _categoryView.VisualizzaCategorie(new List<Categoria>());  // Visualizza le categorie attuali
        string nome = _categoryView.InserisciNomeCategoria();  // Usa la view per ottenere il nome
        _model.InserisciCategoria(nome);
    }

    public void EliminaCategoria()  // Menu opzione 3
    {
        VisualizzaCategorie();  // Mostra le categorie disponibili
        string nome = _categoryView.InserisciNomeCategoria();  // Usa la view per ottenere il nome da eliminare
        _model.EliminaCategoria(nome);
    }

}
