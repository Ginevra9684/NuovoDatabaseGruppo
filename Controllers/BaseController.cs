using System.Data.Common;

public class BaseController
{
    private Model _model;

    private BaseView _baseView;
    private CategoryController _categoryController;
    private ProductController _productController;
    private CustomerController _customerController;

    private OrderController _orderController;

    // Costruttore del Controller riceve il Model e la View
    public BaseController(Model model, BaseView baseView, CategoryController categoryController, ProductController productController, CustomerController customerController, OrderController orderController)
    {
        _model = model;
        _baseView = baseView;
        _categoryController = categoryController;
        _productController = productController;
        _customerController = customerController ; 
        _orderController = orderController ;
    }

    public void MainMenu()
    {
        while (true)
        {
            _baseView.ShowMainMenu();
            var input = _baseView.GetInput();
            switch (input)
            {
                case "1":
                    _productController.ProductsMenu();
                    break;
                case "2":
                    _categoryController.CategoryMenu();
                    break;
                case "3":
                    _customerController.CustomerMenu();
                    break;
                case "4":
                    _orderController.OrderMenu();
                    break;
                case "5":
                    _baseView.Stampa("Esci dal programma");
                    // BISOGNA AGGIUNGERE CHIUSURA CONNESSIONE AL DATABASE
                    return;
                default:
                    _baseView.Stampa("Scelta non valida");
                    break;
            }
        }
    }
}