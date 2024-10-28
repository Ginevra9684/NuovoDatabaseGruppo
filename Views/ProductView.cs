public class ProductView : BaseView
{
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

    public void VisualizzaProdotti(List<Prodotto> prodotti) // Menu opzione 1
    {
        foreach (var prodotto in prodotti)
        {
            Stampa($"Id: {prodotto.Id}, Nome: {prodotto.Nome ?? "Nome sconosciuto"}, Prezzo: {prodotto.Prezzo}, Quantità: {prodotto.Quantita} Id categoria: {prodotto.Id_categoria}");
        }
    }

    public void VisualizzaProdottiOrdinatiPerPrezzo(List<Prodotto> prodotti)    // Menu opzione 2
    {
        foreach (var prodotto in prodotti)
        {
            Stampa($"Id: {prodotto.Id}, Nome: {prodotto.Nome}, Prezzo: {prodotto.Prezzo}, Quantità: {prodotto.Quantita} Id categoria: {prodotto.Id_categoria}");
        }
    }

    public void VisualizzaProdottiOrdinatiPerQuantita(List<Prodotto> prodotti) // Menu opzione 3
    {
        foreach (var prodotto in prodotti)
        {
            Stampa($"Id: {prodotto.Id}, Nome: {prodotto.Nome}, Prezzo: {prodotto.Prezzo}, Quantità: {prodotto.Quantita} Id categoria: {prodotto.Id_categoria}");
        }
    }

    public void VisualizzaProdottoPiuCostoso(Prodotto prodotto) // Menu opzione 6
    {
        Stampa($"Id: {prodotto.Id}, Nome: {prodotto.Nome}, Prezzo: {prodotto.Prezzo}, Quantità: {prodotto.Quantita} Id categoria: {prodotto.Id_categoria}");
    }

    public void VisualizzaProdottoMenoCostoso(Prodotto prodotto)  // Menu opzione 7
    {
        Stampa($"Id: {prodotto.Id}, Nome: {prodotto.Nome}, Prezzo: {prodotto.Prezzo}, Quantità: {prodotto.Quantita} Id categoria: {prodotto.Id_categoria}");
    }

    public void VisualizzaProdotto(Prodotto prodotto)  // Menu opzione 9
    {
        Stampa($"Id: {prodotto.Id}, Nome: {prodotto.Nome}, Prezzo: {prodotto.Prezzo}, Quantità: {prodotto.Quantita} Id categoria: {prodotto.Id_categoria}");
    }

    public void VisualizzaProdottiCategoria(List<Prodotto> prodotti)    // Menu opzione 10
    {
        foreach (var prodotto in prodotti)
        {
            Stampa($"Id: {prodotto.Id}, Nome: {prodotto.Nome}, Prezzo: {prodotto.Prezzo}, Quantità: {prodotto.Quantita} Id categoria: {prodotto.Id_categoria}");
        }
    }

    public void VisualizzaProdottiAdvanced(List<Prodotto> prodotti, List<Categoria> Categorie) // Menu opzione 14
    {
        foreach (var prodotto in prodotti)
        {
            Stampa($"Id: {prodotto.Id}, Nome: {prodotto.Nome}, Prezzo: {prodotto.Prezzo}, Quantità: {prodotto.Quantita} Categoria: {Categorie[prodotto.Id_categoria]}");
        }
    }
}
