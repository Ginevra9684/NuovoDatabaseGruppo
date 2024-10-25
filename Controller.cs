

// COMMENTO IN MAIUSCOLO NUOVA DIRETTIVA PER DIVIDERE CON PATTERN MVC : ESEMPIO CON OPZIONE 1 DEL MENU

public class Controller
{
    private Model _model;
    private View _view;

// Costruttore del Controller riceve il Model e la View
    public Controller(Model model, View view)
    {
        _model = model;   // Associa il Model passato al campo privato
        _view = view;      // Associa la View passata al campo privato
    }

    public void MainMenu()
    {
        while (true)
        {
            _view.ShowMainMenu();
            var input = _view.GetInput();
            switch (input)
            {
                case "1":
                    _view.Stampa(VisualizzaProdotti()); // Stampa un testo passato da VisualizzaProdotti e preso dalla stessa funzione nel Model
                    // METODO APPROPRIATO VisualizzaProdotti();
                    break;
                case "2":
                    _view.Stampa(VisualizzaProdottiOrdinatiPerPrezzo()); 
                    break;
                case "3":
                    _view.Stampa(VisualizzaProdottiOrdinatiPerQuantita()); 
                    break;
                case "4":
                    ModificaPrezzoProdotto(_view.NomeProdotto(), _view.PrezzoProdotto());   // Prende dalla View il nome e il prezzo
                    break;
                case "5":
                    _model.EliminaProdotto(_view.NomeProdotto());
                    break;
                case "6":
                    _view.Stampa(VisualizzaProdottoPiuCostoso()); 
                    break;
                case "7":
                    _view.Stampa(VisualizzaProdottoMenoCostoso()); 
                    break;
                case "8":
                    _model.InserisciProdotto(); 
                    break;
                case "9":
                    _view.Stampa(VisualizzaProdotto(_view.NomeProdotto()));
                    break;
                case "10":
                    _view.Stampa(VisualizzaProdottiCategoria()); 
                    break;
                case "11":
                    _model.InserisciCategoria(); 
                    break;
                case "12":
                    _model.EliminaCategoria();
                    break;
                case "13":
                    _model.InserisciProdottoCategoria(); 
                    break;
                case "14":
                    _view.Stampa(VisualizzaProdottiAdvanced()); 
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

/*
    private string VisualizzaProdotti() // Menu opzione 1
    {
        return _model.VisualizzaProdotti();
    }
*/

    //METODO APPROPRIATO
    private void VisualizzaProdotti()   // Menu opzione 1
    {
        var reader = _model.CaricaProdotti();
        string stringa="";
        while (reader.Read())
        {
            stringa = stringa + $"id: {reader["id"]}, nome: {reader["nome"]}, prezzo: {reader["prezzo"]}, quantita: {reader["quantita"]}, id_categoria: {reader["id_categoria"]}\n";
        }
        _view.Stampa(stringa);
    }


    private string VisualizzaProdottiOrdinatiPerPrezzo()    // Menu opzione 2
    {
        return _model.VisualizzaProdottiOrdinatiPerPrezzo();
    }

    private string VisualizzaProdottiOrdinatiPerQuantita()  // Menu opzione 3
    {
        return _model.VisualizzaProdottiOrdinatiPerQuantita(); // correzione da prezzo a quantita
    }

    private void ModificaPrezzoProdotto(string nome, decimal prezzo)    // Menu Opzione 4
    {
        _model.ModificaPrezzoProdotto(nome, prezzo);    // Passa al Model il nome e il prezzo
    }

    private string VisualizzaProdottoPiuCostoso()   // Menu opzione 6
    {
        return _model.VisualizzaProdottoPiuCostoso();
    }

    private string VisualizzaProdottoMenoCostoso()  // Menu opzione 7
    {
        return _model.VisualizzaProdottoMenoCostoso();
    }

    private string VisualizzaProdotto(string nome)  // Menu opzione 9
    {
        return _model.VisualizzaProdotto(nome);
    }

    private string VisualizzaProdottiCategoria()    // Menu opzione 10
    {
        return _model.VisualizzaProdottiCategoria();
    }

    private string VisualizzaProdottiAdvanced() // Menu opzione 14
    {
        return _model.VisualizzaProdottiAdvanced();
    }

}