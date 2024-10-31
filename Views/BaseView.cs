using System;

public class BaseView
{
    // Metodo per visualizzare il menu principale con le opzioni disponibili
    public void ShowMainMenu()
    {
        Stampa("MAIN MENU");
        Stampa("1 - Vai al menu prodotti");
        Stampa("2 - Vai al menu categorie");
        Stampa("3 - Vai al menu customers");
        Stampa("4 - Vai al menu ordini");
        Stampa("5 - Esci");
    }

    // Metodo per stampare un messaggio sulla console
    public void Stampa(string testo)
    {
        Console.WriteLine(testo);
    }

    // Metodo per ottenere un input dall'utente, gestendo le eccezioni
    public string GetInput()
    {
        while (true)
        {
            try
            {
                // Legge l'input dell'utente dalla console
                string? input = Console.ReadLine();

                // Verifica che l'input non sia vuoto, nullo o composto solo da spazi
                if (string.IsNullOrWhiteSpace(input))
                {
                    Stampa("Errore: l'input non può essere vuoto. Riprova:");
                    continue; // Richiede nuovamente l'input
                }

                // Ritorna l'input valido
                return input;
            }
            catch (Exception ex)
            {
                // Gestisce eccezioni che potrebbero verificarsi durante la lettura dell'input
                Stampa($"Errore durante la lettura dell'input: {ex.Message}. Riprova:");
            }
        }
    }

    // Metodo per ottenere un input numerico intero, gestendo le eccezioni
    public int GetIntInput(string prompt)
    {
        while (true)
        {
            // Stampa il messaggio di richiesta di input
            Stampa(prompt);
            try
            {
                // Ottiene l'input dall'utente e lo converte in un intero
                string input = GetInput();
                return int.Parse(input);
            }
            catch (FormatException)
            {
                // Gestisce l'eccezione se l'input non è un numero intero valido
                Stampa("Errore: l'input deve essere un numero intero. Riprova:");
            }
            catch (Exception ex)
            {
                // Gestisce altre eccezioni che potrebbero verificarsi
                Stampa($"Errore durante la lettura dell'input: {ex.Message}. Riprova:");
            }
        }
    }

    // Metodo per ottenere un input numerico decimale, gestendo le eccezioni
    public double GetDoubleInput(string prompt)
    {
        while (true)
        {
            // Stampa il messaggio di richiesta di input
            Stampa(prompt);
            try
            {
                // Ottiene l'input dall'utente e lo converte in un numero decimale
                string input = GetInput();
                return double.Parse(input);
            }
            catch (FormatException)
            {
                // Gestisce l'eccezione se l'input non è un numero decimale valido
                Stampa("Errore: l'input deve essere un numero decimale. Riprova:");
            }
            catch (Exception ex)
            {
                // Gestisce altre eccezioni che potrebbero verificarsi
                Stampa($"Errore durante la lettura dell'input: {ex.Message}. Riprova:");
            }
        }
    }

    // Metodo per gestire il proseguimento dopo un'operazione
    public void Proseguimento()
    {
        // Chiede all'utente di premere un tasto per continuare
        Stampa("Premere un tasto per proseguire...");
        Console.ReadKey();
        // Pulisce la console per prepararsi alla prossima operazione
        Console.Clear();
    }

    // Metodo per gestire un errore generico
    public void Errore()
    {
        // Stampa un messaggio di errore generico
        Stampa("Opzione non valida\n");
    }
}