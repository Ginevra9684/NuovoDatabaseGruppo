public class CategoryView : BaseView
{
    public void ShowCategoryMenu()
    {
        Stampa("11 - inserire una categoria");
        Stampa("12 - eliminare una categoria");
        Stampa("13 - inserire un prodotto in una categoria");
        Stampa("14 - Visualizzare i prodotti con la categoria");
    }

    public void VisualizzaCategorie(List<Categoria> categorie)
    {
        Console.WriteLine("Categorie disponibili:");
        foreach (var categoria in categorie)
        {
            Stampa($"Id: {categoria.Id}, Nome: {categoria.Nome}");
        }
    }
}
