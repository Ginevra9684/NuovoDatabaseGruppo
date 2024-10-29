public class CategoryView : BaseView
{
    public void ShowCategoryMenu()
    {
        // Visualizza il menu per la gestione delle categorie
        Stampa("MENU CATEGORIA");
        Stampa("1 - visualizza categorie");
        Stampa("2 - inserire una categoria");
        Stampa("3 - eliminare una categoria");
        Stampa("4 - ritorna al menu principale");
    }

    public void VisualizzaCategorie(List<Categoria> categorie)
    {
        // Mostra tutte le categorie disponibili
        Console.WriteLine("Categorie disponibili:");
        foreach (var categoria in categorie)
        {
            Stampa($"Id: {categoria.Id}, Nome: {categoria.Nome}"); // Stampa ID e Nome di ogni categoria
        }
    }

    public int InserisciIdCategoria()
    {
        // Chiede l'ID della categoria e lo restituisce come intero
        Stampa("Inserisci l'ID della categoria:");
        return int.Parse(GetInput());
    }

    public string InserisciNomeCategoria()
    {
        // Chiede il nome della categoria e lo restituisce come stringa
        Stampa("Inserisci il nome della categoria:");
        return GetInput();
    }
}