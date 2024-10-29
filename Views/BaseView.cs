public class BaseView
{
    public void ShowMainMenu()
    {
        Stampa("MAIN MENU");
        Stampa("1 - Vai al menu prodotti");
        Stampa("2 - Vai al menu categorie");
        Stampa("3 - Vai al menu customers");
        Stampa("4 - Esci");
    }

    public string GetInput()
    {
        // Richiedo l'input finché non è valido
        while (true)
        {
            try
            {
                // Legge l'input dell'utente da console
                string? input = Console.ReadLine();
                // Controlla se l'input è null, vuoto o composto solo da spazi bianchi
                if (string.IsNullOrWhiteSpace(input))
                {
                    // Messaggio d'errore per avvisare l'utente che l'input non è valido
                    Console.WriteLine("Errore: l'input non può essere vuoto. Riprova:");
                    continue; // Ritorna all'inizio del ciclo per chiedere nuovamente l'input
                }

                // Se l'input è valido, lo restituisce
                return input;
            }
            catch (Exception ex)
            {
                // Gestisce eventuali eccezioni durante la lettura dell'input
                Console.WriteLine($"Errore durante la lettura dell'input: {ex.Message}. Riprova:");
                // Continua il ciclo per chiedere nuovamente l'input

            }
        }
    }

    public void Stampa(string testo)
    {
        Console.WriteLine(testo);
    }

    // Metodo per mostrare l'opzione di uscita e il messaggio per scegliere un'opzione
    public void ShowEndMenu()
    {
        Console.WriteLine("18 - uscire");
        Console.WriteLine("scegli un'opzione");
    }
}

