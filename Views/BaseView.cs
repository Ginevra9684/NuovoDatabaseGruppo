public abstract class BaseView
{
    public string GetInput()
    {
        return Console.ReadLine()!;
    }

    public void Stampa(string testo)
    {
        Console.WriteLine(testo);
    }
}
