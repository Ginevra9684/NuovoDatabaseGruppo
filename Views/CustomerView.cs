public class CustomerView : BaseView
{
    // Metodo per visualizzare il menu relativo ai clienti
    public void ShowClientMenu()
    {
        Stampa("14 - Aggiungere un nuovo cliente");
        Stampa("15 - Visualizzare tutti i clienti");
        Stampa("16 - Modificare un cliente esistente");
        Stampa("17 - Eliminare un cliente");
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
        Stampa("Inserisci l'ID del cliente da eliminare:");
        return int.Parse(GetInput());
    }

      // Metodo per modificare un cliente, restituisce un nuovo nome da aggiornare
    public (int id, string nuovoNome) ModificaCliente()
    {
        Stampa("Inserisci l'ID del cliente da modificare:");
        int id = int.Parse(GetInput());
        
        Stampa("Inserisci il nuovo nome del cliente:");
        string nuovoNome = GetInput();

        return (id, nuovoNome);
    }

    
}