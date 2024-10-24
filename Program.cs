class Program
{
    static void Main(string[] args)
    {
        Model model = new Model();
        View view = new View();  // Creazione della View senza argomenti
        Controller controller = new Controller(model, view);
        controller.MainMenu();
    }
}
