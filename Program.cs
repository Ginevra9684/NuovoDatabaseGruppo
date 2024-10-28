﻿class Program
{
    static void Main(string[] args)
    {
        // Creazione delle istanze del Model e delle Views
        var model = new Model();
        var productView = new ProductView();   // Istanza della vista dei prodotti
        var categoryView = new CategoryView(); // Istanza della vista delle categorie

        // Creazione del Controller, passando le dipendenze al costruttore
        var controller = new Controller(model, productView, categoryView);

        // Avvio del menu principale
        controller.MainMenu();
    }
}
