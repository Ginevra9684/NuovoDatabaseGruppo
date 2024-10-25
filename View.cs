public class View
{
   // public View() { } costruttore non necessario

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

    // Stampa un testo passato da una funzione del controller (il controller prende questo testo a sua volta da una funzione del model)
    public void Stampa(string testo)    // QUESTO LO POSSIAMO USARE IN MANIERA GENERICA QUANDO DOBBIAMO SOLO STAMPARE
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
        Console.WriteLine("inserisci il nuovo prezzo");
        try
        {
            return Convert.ToDecimal(Console.ReadLine()!);
        }
        catch(Exception) 
        {
            Console.WriteLine("Prezzo non valido.\nImposto a 0.");
            return 0;
        }
    }
}
