public class BaseView
{
    public void ShowMainMenu()
    {
        // Visualizza il menu principale con le opzioni disponibili
        Stampa("MAIN MENU");
        Stampa("1 - Vai al menu prodotti");
        Stampa("2 - Vai al menu categorie");
        Stampa("3 - Vai al menu customers");
        Stampa("4 - Vai al menu ordini");
        Stampa("5 - Esci");
    }

    public string GetInput()
    {
        // Continua a richiedere l'input finché non viene fornito un valore valido
        while (true)
        {
            try
            {
                // Legge l'input dell'utente dalla console
                string? input = Console.ReadLine();

                // Verifica che l'input non sia vuoto, nullo o composto solo da spazi
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Errore: l'input non può essere vuoto. Riprova:");
                    continue; // Richiede nuovamente l'input
                }

                // Ritorna l'input valido
                return input;
            }
            catch (Exception ex)
            {
                // Gestisce eccezioni che potrebbero verificarsi durante la lettura dell'input
                Console.WriteLine($"Errore durante la lettura dell'input: {ex.Message}. Riprova:");
                // Il ciclo riparte, chiedendo un nuovo input
            }
        }
    }

    public void Stampa(string testo)
    {
        // Stampa il testo fornito sulla console
        Console.WriteLine(testo);
    }

    public void Proseguimento()
    {
        Stampa("Premere un tasto per proseguire...");
        Console.ReadKey();
        Console.Clear();
    }

    public void Errore()
    {
        Stampa("Opzione non valida\n");
    }
}