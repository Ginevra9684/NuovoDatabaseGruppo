public class CategoryView : BaseView
{
    public void ShowCategoryMenu()
    {
        Stampa("11 - inserire una categoria");
        Stampa("12 - eliminare una categoria");
        Stampa("13 - inserire un prodotto in una categoria");
    }

    public void VisualizzaCategorie(List<Categoria> categorie)
    {
        Console.WriteLine("Categorie disponibili:");
        foreach (var categoria in categorie)
        {
            Stampa($"Id: {categoria.Id}, Nome: {categoria.Nome}");
        }
    }

    public int InserisciIdCategoria()
    {
        Stampa("Inserisci l'ID della categoria:");
        return int.Parse(GetInput());
    }

    public string InserisciNomeCategoria()
    {
        Stampa("Inserisci il nome della categoria:");
        return GetInput();
    }
}
