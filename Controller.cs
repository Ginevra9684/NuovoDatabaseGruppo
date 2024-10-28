public class Controller
{
    private Model _model;
    private ProductView _productView;

    private CategoryView _categoryView;

    private BaseView _baseView;

    public List<Prodotto> prodotti { get; set; } = new List<Prodotto>();

// Costruttore del Controller riceve il Model e la View
    public Controller(Model model, ProductView productView, CategoryView categoryView, BaseView baseView)
    {
        _model = model;
        _productView = productView; 
        _categoryView = categoryView;
        _baseView = baseView;
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
                    VisualizzaProdottiCategoria(); 
                    break;
                case "11":
                    InserisciCategoria(); 
                    break;
                case "12":
                    EliminaCategoria();
                    break;
                case "13":
                    InserisciProdottoCategoria(); 
                    break;
                case "14":
                    VisualizzaProdottiAdvanced(); 
                    break;
                case "15":
                    Console.WriteLine("Uscita in corso...");
                    // BISOGNA FAR IN MODO DI CHIUDERE LA CONNESSIONE AL DATABSE QUI
                    return; // Uscita dal ciclo 
                default:
                    Console.WriteLine("scelta non valida");
                    break;
            }
            
        }
    }

  /*  private void VisualizzaProdotti()   // Menu opzione 1
    {
        var reader = _model.CaricaProdotti();
        /*  METODO VECCHIO
        string stringa="";
        while (reader.Read())
        {
            stringa = stringa + $"id: {reader["id"]}, nome: {reader["nome"]}, prezzo: {reader["prezzo"]}, quantita: {reader["quantita"]}, id_categoria: {reader["id_categoria"]}\n";
        }
        
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
            prodotti.Add(prodotto);
        };
//!!!CREARE VISUALIZZAPRODOTTI PER SOSTITUIRE STAMPA - Deve avere come parametro un List<Prodotto> e utilizzare un foreach
        _productView.VisualizzaProdotti(prodotti);
    } */

    private void ShowMainMenu()
    {
        _productView.ShowProductMenu();
        _categoryView.ShowCategoryMenu(); 
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

    _productView.VisualizzaProdotti(prodottiOrdinati);
}

/*
    private void VisualizzaProdottiOrdinatiPerQuantita()  // Menu opzione 3
    {
        var reader = _model.CaricaProdottiOrdinatiPerQuantita();
        string stringa="";
        while (reader.Read())
        {
            stringa = stringa + $"id: {reader["id"]}, nome: {reader["nome"]}, prezzo: {reader["prezzo"]}, quantita: {reader["quantita"]}, id_categoria: {reader["id_categoria"]}\n";
        }
//!!!CREARE VISUALIZZAPRODOTTIORDINATIPERQUANTITA PER SOSTITUIRE STAMPA
        _productView.Stampa(stringa);
    }   */


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

    _productView.VisualizzaProdotti(prodottiOrdinati); // Pass the list for display
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

    private void VisualizzaProdottoPiuCostoso()   // Menu opzione 6
    {
        var reader = _model.CaricaProdottoPiuCostoso();
        string stringa="";
        while (reader.Read())
        {
            stringa = stringa + $"id: {reader["id"]}, nome: {reader["nome"]}, prezzo: {reader["prezzo"]}, quantita: {reader["quantita"]}, id_categoria: {reader["id_categoria"]}\n";
        }
//!!!CREARE VISUALIZZAPRODOTTOPIUCOSTOSO PER SOSTITUIRE STAMPA
        _productView.Stampa(stringa);
    }

    private void VisualizzaProdottoMenoCostoso()  // Menu opzione 7
    {
        var reader = _model.CaricaProdottoMenoCostoso();
        string stringa="";
        while (reader.Read())
        {
            stringa = stringa + $"id: {reader["id"]}, nome: {reader["nome"]}, prezzo: {reader["prezzo"]}, quantita: {reader["quantita"]}, id_categoria: {reader["id_categoria"]}\n";
        }
//!!!CREARE VISUALIZZAPRODOTTOMENOCOSTOSO PER SOSTITUIRE STAMPA
        _productView.Stampa(stringa);
    }

    private void InserisciProdotto()    // Menu opzione 8
    {
//!!!SPOSTARE NELLA PRODUCTVIEW TITTI I WRITELINE
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
        string nome = _productView.InserisciNomeProdotto(); 
        var reader = _model.CaricaProdotto(nome);
        string stringa="";
        while (reader.Read())
        {
            stringa = stringa + $"id: {reader["id"]}, nome: {reader["nome"]}, prezzo: {reader["prezzo"]}, quantita: {reader["quantita"]}, id_categoria: {reader["id_categoria"]}\n";
        }
//!!!CREARE VISUALIZZAPRODOTTO PER SOSTITUIRE STAMPA
        _productView.Stampa(stringa);
    }

    private void VisualizzaProdottiCategoria()    // Menu opzione 10
    {
//!!!SPOSTARE NELLA PRODUCTVIEW TITTI I WRITELINE
        _model.VisualizzaCategorie();
        Console.WriteLine("inserisci l'id della categoria");
        int id_categoria = Int32.Parse(Console.ReadLine()!);
        var reader = _model.VisualizzaProdottiCategoria(id_categoria);
        string stringa="";
        while (reader.Read())
        {
            stringa = stringa + $"id: {reader["id"]}, nome: {reader["nome"]}, prezzo: {reader["prezzo"]}, quantita: {reader["quantita"]}, id_categoria: {reader["id_categoria"]}\n";
        }
//!!!CREARE VISUALIZZACATEGORIE PER SOSTITUIRE STAMPA
        _categoryView.Stampa(stringa);
    }

    private void InserisciCategoria()   // Menu opzione 11
    {
//!!!SPOSTARE NELLA PRODUCTVIEW TITTI I WRITELINE
        _model.VisualizzaCategorie();
        Console.WriteLine("inserisci il nome della nuova categoria");
        string nome = Console.ReadLine()!;
        _model.InserisciCategoria(nome);
    }

    private void EliminaCategoria() // Menu opzione 12
    {
//!!!SPOSTARE NELLA PRODUCTVIEW TITTI I WRITELINE
        _model.VisualizzaCategorie();
        Console.WriteLine("inserisci il nome della categoria da eliminare");
        string nome = Console.ReadLine()!;
        _model.EliminaCategoria(nome);
    }

    public void InserisciProdottoCategoria()    // Menu opzione 13
    {
//!!!SPOSTARE NELLA PRODUCTVIEW TITTI I WRITELINE
        // Chiama il metodo per visualizzare le categorie
        _model.VisualizzaCategorie();
        //seleziona categoria
        Console.WriteLine("inserisci l'id della categoria");
        int id_categoria = Int32.Parse(Console.ReadLine()!);
        //inserisci prodotto
        Console.WriteLine("inserisci il nome del prodotto");
        string nome = Console.ReadLine()!;
        Console.WriteLine("inserisci il prezzo del prodotto");
        decimal prezzo = Decimal.Parse(Console.ReadLine()!);
        Console.WriteLine("inserisci la quantità del prodotto");
        int quantita = Int32.Parse(Console.ReadLine()!);
        _model.InserisciProdottoCategoria(id_categoria, nome, prezzo, quantita);
    }

    private void VisualizzaProdottiAdvanced() // Menu opzione 14
    {
        var reader = _model.CaricaProdottiAdvanced();
        string stringa="";
        while (reader.Read())
        {
            stringa = stringa + $"id: {reader["id"]}, nome: {reader["nome"]}, prezzo: {reader["prezzo"]}, quantita: {reader["quantita"]}, categoria: {reader["nome_categoria"]}\n";
        }
//!!!CREARE VISUALIZZAPRODOTTIADVANCED PER SOSTITUIRE STAMPA
        _productView.Stampa(stringa);
    }
}