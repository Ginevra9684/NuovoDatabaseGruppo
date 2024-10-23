



public class Controller
{
    private Model _model;
    private View _view;

    public Controller(Model model, View view)
    {
        _model = model;
        _view = view;
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
                    _view.Stampa(VisualizzaProdotti());
                    break;
                case "2":
                    _view.Stampa(VisualizzaProdottiOrdinatiPerPrezzo());
                    break;
                case "3":
                    _view.Stampa(VisualizzaProdottiOrdinatiPerQuantita());
                    break;
                case "4":
                    _model.ModificaPrezzoProdotto();
                    break;
                case "5":
                    _model.EliminaProdotto();
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
                default:
                    Console.WriteLine("scelta non valida");
                    break;
            }
            
        }
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
        return _model.VisualizzaProdottiOrdinatiPerPrezzo();
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