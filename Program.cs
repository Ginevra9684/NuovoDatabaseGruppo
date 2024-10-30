/*class Program
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

        var orderView = new OrderView();
        var orderController = new OrderController(model, orderView);

        var baseView = new BaseView();
        // Creazione del Controller, passando le dipendenze al costruttore
        var baseController = new BaseController(model, baseView, categoryController, productController, customerController, orderController);

        // Avvio del menu principale
        baseController.MainMenu();
    }
}
*/

using Microsoft.EntityFrameworkCore;

class Program
{
    static void Main(string[] args)
    {
        // Creazione dell'istanza di Database (contesto di Entity Framework)
        using (var database = new Database())
        {
            // Assicura che il database sia creato se non esiste
            database.Database.EnsureCreated();
            
            // Creazione delle istanze delle Views
            var categoryView = new CategoryView();
            var productView = new ProductView();
            var customerView = new CustomerView();
            var orderView = new OrderView();
            var baseView = new BaseView();

            // Creazione dei Controller, passando le dipendenze al costruttore
            var categoryController = new CategoryController(database, categoryView);
            var productController = new ProductController(database, productView, categoryController);
            var customerController = new CustomerController(database, customerView);
            var orderController = new OrderController(database, orderView, productController, customerController);
            var baseController = new BaseController(database, baseView, categoryController, productController, customerController, orderController);

            // Avvio del menu principale
            baseController.MainMenu();
        }
    }
}
