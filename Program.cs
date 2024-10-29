class Program
{
    static void Main(string[] args)
    {
        // Creazione delle istanze del Model e delle Views
        var model = new Model();
        
        var categoryView = new CategoryView();
        var categoryController = new CategoryController(model, categoryView); 

        var productView = new ProductView();   // Istanza della vista dei prodotti
        var productController = new ProductController(model, productView, categoryController);

        var customerView = new CustomerView();
        var customerController = new CustomerController(model, customerView);

        var baseView = new BaseView();
        // Creazione del Controller, passando le dipendenze al costruttore
        var baseController = new BaseController(model, baseView, categoryController, productController, customerController);

        // Avvio del menu principale
        baseController.MainMenu();
    }
}
