public class BaseController
{
    // Riferimento al modello dell'applicazione, che gestisce l'accesso e le operazioni sui dati
    // private Model _model;

    // Riferimento alla vista principale dell'applicazione utilizzata per visualizzare il menu principale e i messaggi generali
    private BaseView _baseView;

    // Controller specifici per gestire le diverse sezioni dell'applicazione
    private CategoryController _categoryController;
    private ProductController _productController;
    private CustomerController _customerController;

    private OrderController _orderController;

    // Costruttore del controller principale, che riceve come parametri il modello, la vista di base e i controller per categorie prodotti clienti e ordini
    public BaseController(BaseView baseView, CategoryController categoryController, ProductController productController, CustomerController customerController, OrderController orderController)
    {
        _baseView = baseView;         // Inizializza il riferimento alla vista principale
        _categoryController = categoryController;  // Inizializza il controller delle categorie, che sarà chiamato per gestire tutte le operazioni relative alle categorie
        _productController = productController; // Inizializza il controller dei prodotti che sarà chiamato per gestire tutte le operazioni relative ai prodotti
        _customerController = customerController ; // Inizializza il controller dei clienti che sarà chiamato per gestire tutte le operazioni relative ai clienti
        _orderController = orderController ;   // Inizializza il controller degli ordini che sarà chiamato per gestire tutte le operazioni relative agli ordini
    }

// Metodo principale per gestire il menu dell'applicazione
    public void MainMenu()
    {
        while (true)
        {
            Console.Clear();
            _baseView.ShowMainMenu(); // Mostra il menu principale all'utente
            var input = _baseView.GetInput();  // Ottiene l'input dell'utente
            switch (input)
            {
                case "1":
                    _productController.ProductsMenu(); // Gestisce il menu dei prodotti
                    break;
                case "2":
                    _categoryController.CategoryMenu();  // Gestisce il menu delle categorie
                    break;
                case "3":
                    _customerController.CustomerMenu();  // Gestisce il menu dei clienti
                    break;
                case "4":
                    _orderController.OrderMenu();   // Gestisce il menu degli ordini
                    break;
                case "5":
                    _baseView.Stampa("Esci dal programma");
                    return;
                default:
                    _baseView.Errore();
                    _baseView.Proseguimento();
                    break;
            }
        }
    }
}