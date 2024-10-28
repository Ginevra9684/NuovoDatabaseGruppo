class Program
{
    static void Main(string[] args)
    {
        // Creazione delle istanze del Model e delle Views
        var model = new Model();
        var baseView = new BaseView();
        var productView = new ProductView();   // Istanza della vista dei prodotti
        var categoryView = new CategoryView(); // Istanza della vista delle categorie
        var userView = new UserView();

        // Creazione del Controller, passando le dipendenze al costruttore
        var controller = new Controller(model, productView, categoryView, baseView, userView);

        // Avvio del menu principale
        controller.MainMenu();
    }
}
