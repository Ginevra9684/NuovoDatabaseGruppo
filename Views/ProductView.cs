public class ProductView : BaseView
{
    public void VisualizzaProdotti(List<Prodotto> prodotti)
    {
        foreach (var prodotto in prodotti)
        {
            Stampa($"ID: {prodotto.Id}, Nome: {prodotto.Nome ?? "Nome sconosciuto"}, Prezzo: {prodotto.Prezzo}, Quantità: {prodotto.Quantita}, Categoria ID: {prodotto.Id_categoria}");
        }
    }

    public void ShowProductMenu()
    {
        Stampa("1 - Visualizzare i prodotti");
        Stampa("2 - Visualizzare i prodotti ordinati per prezzo");
        Stampa("3 - Visualizzare i prodotti ordinati per quantità");
        Stampa("4 - Modificare il prezzo di un prodotto");
        Stampa("5 - Eliminare un prodotto");
        Stampa("6 - Visualizzare il prodotto più costoso");
        Stampa("7 - Visualizzare il prodotto meno costoso");
        Stampa("8 - Inserire un prodotto");
        Stampa("9 - Visualizzare un prodotto");
        Stampa("10 - Visualizzare i prodotti di una categoria");
      
    }

   

    public int InserisciIdCategoria()
    {
        Stampa("Inserisci l'ID della categoria:");
        return int.Parse(GetInput());
    }

    public string InserisciNomeProdotto()
    {
        Stampa("Inserisci il nome del prodotto:");
        return GetInput();
    }

    public decimal InserisciPrezzoProdotto()
    {
        Stampa("Inserisci il prezzo del prodotto:");
        return decimal.Parse(GetInput());
    }

    public int InserisciQuantitaProdotto()
    {
        Stampa("Inserisci la quantità del prodotto:");
        return int.Parse(GetInput());
    }
}
