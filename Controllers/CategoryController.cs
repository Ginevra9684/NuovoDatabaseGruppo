public class CategoryController
{
    private Model _model;
    private CategoryView _categoryView;
    private ProductView _productView;
    public CategoryController(Model model, CategoryView categoryView,ProductView productView)
    {
        _model = model;
        _categoryView = categoryView;
        _productView = productView;
    }

    public void VisualizzaCategorie()
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

    public void InserisciCategoria()
    {
        _categoryView.VisualizzaCategorie(new List<Categoria>());  // Visualizza le categorie attuali
        string nome = _categoryView.InserisciNomeCategoria();  // Usa la view per ottenere il nome
        _model.InserisciCategoria(nome);
    }

    public void EliminaCategoria()
    {
        VisualizzaCategorie();  // Mostra le categorie disponibili
        string nome = _categoryView.InserisciNomeCategoria();  // Usa la view per ottenere il nome da eliminare
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

  



}
