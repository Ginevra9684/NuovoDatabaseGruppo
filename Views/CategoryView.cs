public class CategoryView : BaseView
{
    public void ShowCategoryMenu()
    {
        Stampa("MENU CATEGORIA");
        Stampa("1 - visualizza categorie");
        Stampa("2 - inserire una categoria");
        Stampa("3 - eliminare una categoria");
        Stampa("4 - ritorna al menu principale");
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
