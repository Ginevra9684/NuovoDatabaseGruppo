

public class View
{
    private Model _db;

    public View(Model db)
    {
        _db = db;
    }

    public void ShowMainMenu()
    {
        Console.WriteLine("1 - visualizzare i prodotti");
        Console.WriteLine("2 - visualizzare i prodotti ordinati per prezzo");
        Console.WriteLine("3 - visualizzare i prodotti ordinati per quantità");
        Console.WriteLine("4 - modificare il prezzo di un prodotto");
        Console.WriteLine("5 - eliminare un prodotto");
        Console.WriteLine("6 - visualizzare il prodotto più costoso");
        Console.WriteLine("7 - visualizzare il prodotto meno costoso");
        Console.WriteLine("8 - inserire un prodotto");
        Console.WriteLine("9 - visualizzare un prodotto");
        Console.WriteLine("10 - visualizzare i prodotti di una categoria");
        Console.WriteLine("11 - inserire una categoria");
        Console.WriteLine("12 - eliminare una categoria");
        Console.WriteLine("13 - inserire un prodotto in una categoria");
        Console.WriteLine("14 - visualizzare i prodotti con la categoria");
        Console.WriteLine("15 - uscire");
        Console.WriteLine("scegli un'opzione");
    }

    public string GetInput()
    {
        return Console.ReadLine()!;
    }

    public void Stampa(string testo)
    {
        Console.WriteLine(testo);
    }

    public string NomeProdotto()
    {
        Console.WriteLine("inserisci il nome del prodotto");
        return Console.ReadLine()!;
    }

    public decimal PrezzoProdotto()
    {
        Console.WriteLine("inserisci il nuovo prezzo"); // chiede il nuovo prezzo del prodotto da modificare
        try
        {
            return Convert.ToDecimal(Console.ReadLine()!); // legge il nuovo prezzo del prodotto da modificare
        }
        catch(Exception) 
        {
            Console.WriteLine("Prezzo non valido.\nImposto a 0.");
            return 0;
        }
    }
}