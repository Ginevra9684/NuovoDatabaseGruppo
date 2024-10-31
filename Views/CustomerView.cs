public class CustomerView : BaseView
{
    // Metodo per visualizzare il menu relativo ai clienti
    public void ShowCustomerMenu()
    {
        Stampa("CUSTOMER MENU");
        Stampa("1 - Aggiungere un nuovo cliente");
        Stampa("2 - Visualizzare tutti i clienti");
        Stampa("3 - Modificare un cliente esistente");
        Stampa("4 - Eliminare un cliente");
        Stampa("5 - Torna al menu principale");
    }

 // Metodo che riceve una lista di clienti e li visualizza
    public void VisualizzaClienti(List<Cliente> clienti)
    {
        Stampa("Elenco Clienti:");
        foreach (var cliente in clienti)
        {
            Stampa($"ID: {cliente.Id}, Nome: {cliente.Nome}");
        }
    }

     // Metodo per aggiungere un nuovo cliente con il nome
    public string InserisciCliente()
    {
        Stampa("Inserisci il nome del cliente:");
        return GetInput();
    }

      // Metodo per eliminare un cliente restituisce l'ID del cliente da eliminare
    public int EliminaCliente()
    {
        // Stampa("Inserisci l'ID del cliente da eliminare:");
        return GetIntInput("Inserisci l'ID del cliente da eliminare:");
    }

      // Metodo per modificare un cliente, restituisce un nuovo nome da aggiornare
    public (int id, string nuovoNome) ModificaCliente()
    {
        // Stampa("Inserisci l'ID del cliente da modificare:");
        int id = GetIntInput("Inserisci l'ID del cliente da modificare:");
        
        Stampa("Inserisci il nuovo nome del cliente:");
        string nuovoNome = GetInput();

        return (id, nuovoNome);
    }

    
}