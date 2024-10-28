public class BaseView
{
    public string GetInput()
    {
        return Console.ReadLine()!;
    }

    public void Stampa(string testo)
    {
        Console.WriteLine(testo);
    }

       // Metodo per mostrare l'opzione di uscita e il messaggio per scegliere un'opzione
    public void ShowEndMenu()
    {
        Console.WriteLine("15 - uscire");
        Console.WriteLine("scegli un'opzione");
    }
}

