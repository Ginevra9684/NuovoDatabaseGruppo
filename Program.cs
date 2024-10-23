using System.Data.SQLite; 
// comando per installare il pacchetto System.Data.SQLite
// dotnet add package System.Data.SQLite

class Program
{
    static void Main(string[] args)
    {
        Model model = new Model();
        Controller controller = new Controller(model);
        View view = new View(model, controller);
    }
}