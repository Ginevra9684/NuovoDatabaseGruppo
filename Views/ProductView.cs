public class ProductView : BaseView
{
    public void ShowProductMenu()
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
        Console.WriteLine("14 - visualizzare i prodotti con la categoria");
        Console.WriteLine("15 - uscire");
        Console.WriteLine("scegli un'opzione");
    }

    public string NomeProdotto()
    {
        Console.WriteLine("inserisci il nome del prodotto");
        return GetInput();
    }

    public decimal PrezzoProdotto()
    {
        Console.WriteLine("inserisci il nuovo prezzo");
        try
        {
            return Convert.ToDecimal(GetInput());
        }
        catch(Exception)
        {
            Console.WriteLine("Prezzo non valido.\nImposto a 0.");
            return 0;
        }
    }
}
/*ciao*/