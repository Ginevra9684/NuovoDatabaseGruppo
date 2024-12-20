public class ProductView : BaseView
{
    // Mostra il menu di gestione dei prodotti
    public void ShowProductMenu()
    {
        Stampa("MENU PRODOTTI");
        Stampa("1 - Visualizzare i prodotti");
        Stampa("2 - Visualizzare i prodotti ordinati per prezzo");
        Stampa("3 - Visualizzare i prodotti ordinati per quantità");
        Stampa("4 - Modificare il prezzo di un prodotto");
        Stampa("5 - Modificare la giacenza di un prodotto");
        Stampa("6 - Eliminare un prodotto");
        Stampa("7 - Visualizzare il prodotto più costoso");
        Stampa("8 - Visualizzare il prodotto meno costoso");
        Stampa("9 - Visualizzare un prodotto");
        Stampa("10 - Visualizzare i prodotti di una categoria");
        Stampa("11 - Inserire un prodotto");
        Stampa("12 - Torna al menu principale");
    }

    // Inserisce l'ID della categoria
    public int InserisciIdCategoria()
    {
        //Stampa("Inserisci l'ID della categoria:");
        return GetIntInput("Inserisci l'ID della categoria:");
    }

    // Inserisce il nome del prodotto
    public string InserisciNomeProdotto()
    {
        Stampa("Inserisci il nome del prodotto:");
        return GetInput();
    }

    // Inserisce l'Id del prodotto
    public int InserisciIdProdotto()
    {
        return GetIntInput("Inserisci l'Id del prodotto:");
    }

    // Inserisce il prezzo del prodotto
    public double InserisciPrezzoProdotto()
    {
        // Stampa("Inserisci il prezzo del prodotto:");
        return GetDoubleInput("Inserisci il prezzo del prodotto:");
    }

    // Inserisce la quantità del prodotto
    public int InserisciQuantitaProdotto()
    {
        // Stampa("Inserisci la quantità del prodotto:");
        return GetIntInput("Inserisci la quantità del prodotto:");
    }

    // Visualizza la lista completa dei prodotti
    public void VisualizzaProdotti(List<Prodotto> prodotti)
    {
        foreach (var prodotto in prodotti)
        {
            Stampa($"Id: {prodotto.Id}, Nome: {prodotto.Nome ?? "Nome sconosciuto"}, Prezzo: {prodotto.Prezzo}, Quantità: {prodotto.Giacenza}, Categoria: {prodotto.Categoria!.Nome}");
        }
    }

    // Visualizza i prodotti ordinati per prezzo
    public void VisualizzaProdottiOrdinatiPerPrezzo(List<Prodotto> prodotti)
    {
        foreach (var prodotto in prodotti)
        {
            Stampa($"Id: {prodotto.Id}, Nome: {prodotto.Nome}, Prezzo: {prodotto.Prezzo}, Quantità: {prodotto.Giacenza}, Categoria: {prodotto.Categoria!.Nome}");
        }
    }

    // Visualizza i prodotti ordinati per quantità
    public void VisualizzaProdottiOrdinatiPerQuantita(List<Prodotto> prodotti)
    {
        foreach (var prodotto in prodotti)
        {
            Stampa($"Id: {prodotto.Id}, Nome: {prodotto.Nome}, Prezzo: {prodotto.Prezzo}, Quantità: {prodotto.Giacenza}, Categoria: {prodotto.Categoria!.Nome}");
        }
    }

    // Modifica il prezzo di un prodotto, restituendo l'ID e il nuovo prezzo
    public (int id, double nuovoPrezzo) ModificaPrezzoProdotto()
    {
        
        int id = GetIntInput("Inserisci l'ID del prodotto da modificare:");
        
        // Stampa("Inserisci il nuovo prezzo del prodotto:");
        double nuovoPrezzo = GetDoubleInput("Inserisci il nuovo prezzo del prodotto:");

        return (id, nuovoPrezzo);
    }

    // Metodo per eliminare un prodotto, restituisce l'ID del prodotto da eliminare
    public int EliminaProdotto()
    {
        // Stampa("Inserisci l'ID del prodotto da eliminare:");
        return GetIntInput("Inserisci l'ID del prodotto da eliminare:");
    }

    // Visualizza il prodotto più costoso
    public void VisualizzaProdottoPiuCostoso(Prodotto prodotto)
    {
        Stampa($"Id: {prodotto.Id}, Nome: {prodotto.Nome}, Prezzo: {prodotto.Prezzo}, Quantità: {prodotto.Giacenza}, Categoria: {prodotto.Categoria!.Nome}");
    }

    // Visualizza il prodotto meno costoso
    public void VisualizzaProdottoMenoCostoso(Prodotto prodotto)
    {
        Stampa($"Id: {prodotto.Id}, Nome: {prodotto.Nome}, Prezzo: {prodotto.Prezzo}, Quantità: {prodotto.Giacenza}, Categoria: {prodotto.Categoria!.Nome}");
    }

    // Visualizza i dettagli di un prodotto specifico
    public void VisualizzaProdotto(Prodotto prodotto)
    {
        Stampa($"Id: {prodotto.Id}, Nome: {prodotto.Nome}, Prezzo: {prodotto.Prezzo}, Quantità: {prodotto.Giacenza}, Categoria: {prodotto.Categoria!.Nome}");
    }

    // Visualizza tutti i prodotti appartenenti a una categoria specifica
    public void VisualizzaProdottiCategoria(List<Prodotto> prodotti)
    {
        foreach (var prodotto in prodotti)
        {
            Stampa($"Id: {prodotto.Id}, Nome: {prodotto.Nome}, Prezzo: {prodotto.Prezzo}, Quantità: {prodotto.Giacenza}, Categoria: {prodotto.Categoria!.Nome}");
        }
    }
}