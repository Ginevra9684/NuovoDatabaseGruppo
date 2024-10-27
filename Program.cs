class Program
{
    static void Main(string[] args)
    {
        Model model = new Model();
        ProductView productView = new ProductView();   // Creazione di un'istanza della vista dei prodotti (ProductView) 
        CategoryView categoryView = new CategoryView(); // Creazione di un'istanza della vista per le categorie (CategoryView)
        Controller controller = new Controller(model, productView, categoryView);
        controller.MainMenu();
    }
}
