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
                    _view.Stampa(VisualizzaProdotti()); //TODO List di string
                    break;
                case "2":
                    _view.Stampa(VisualizzaProdottiOrdinatiPerPrezzo()); //TODO List di string
                    break;
                case "3":
                    _view.Stampa(VisualizzaProdottiOrdinatiPerQuantita()); //TODO List di string
                    break;
                case "4":
                    ModificaPrezzoProdotto(_view.NomeProdotto(), _view.PrezzoProdotto());
                    break;
                case "5":
                    _model.EliminaProdotto(_view.NomeProdotto());
                    break;
                case "6":
                    _view.Stampa(VisualizzaProdottoPiuCostoso()); //TODO List di string
                    break;
                case "7":
                    _view.Stampa(VisualizzaProdottoMenoCostoso()); //TODO List di string
                    break;
                case "8":
                    _model.InserisciProdotto(); //ok
                    break;
                case "9":
                    _view.Stampa(VisualizzaProdotto(_view.NomeProdotto()));
                    break;
                case "10":
                    _view.Stampa(VisualizzaProdottiCategoria()); //ok
                    break;
                case "11":
                    _model.InserisciCategoria(); // ok
                    break;
                case "12":
                    _model.EliminaCategoria(); //ok 
                    break;
                case "13":
                    _model.InserisciProdottoCategoria(); //ok
                    break;
                case "14":
                    _view.Stampa(VisualizzaProdottiAdvanced()); //TODO List di string
                    break;
                case "15":
                    Console.WriteLine("Uscita in corso...");
                    return; // Uscita dal ciclo 
                default:
                    Console.WriteLine("scelta non valida");
                    break;
            }
            
        }
    }

    private void ModificaPrezzoProdotto(string nome, decimal prezzo)
    {
        _model.ModificaPrezzoProdotto(nome, prezzo);
    }

    private string VisualizzaProdottiCategoria()
    {
        return _model.VisualizzaProdottiCategoria();
    }

    private string VisualizzaProdotto(string nome)
    {
        return _model.VisualizzaProdotto(nome);
    }

    private string VisualizzaProdottoMenoCostoso()
    {
        return _model.VisualizzaProdottoMenoCostoso();
    }

    private string VisualizzaProdottoPiuCostoso()
    {
        return _model.VisualizzaProdottoPiuCostoso();
    }

    private string VisualizzaProdottiOrdinatiPerQuantita()
    {
        return _model.VisualizzaProdottiOrdinatiPerQuantita(); // correzione da prezzo a quantita
    }

    private string VisualizzaProdottiOrdinatiPerPrezzo()
    {
        return _model.VisualizzaProdottiOrdinatiPerPrezzo();
    }

    private string VisualizzaProdottiAdvanced()
    {
        return _model.VisualizzaProdottiAdvanced();
    }

    private string VisualizzaProdotti()
    {
        return _model.VisualizzaProdotti();
    }
}