public class Prodotto : General, IComparable<Prodotto>
{
    // Prezzo del prodotto
    public decimal Prezzo { get; set; }

    // Quantità disponibile in magazzino
    public int Giacenza { get; set; }

    // Relazione con Marca
    public Marca? Marca { get; set; }

    // Colore del prodotto
    public string? Colore { get; set; }

    // Relazione con Categoria
    public Categoria? Categoria { get; set; }

public int CompareTo(Object? x, Object? y)
    {
        Prodotto p1 = (Prodotto)x!;
        Prodotto p2 = (Prodotto)y!;
        if (p1.Giacenza != p2.Giacenza)
            return 1;
        return 0;
    }
    int IComparable<Prodotto>.CompareTo(Prodotto? other)
    {
        return Giacenza.CompareTo(other!.Giacenza);
    }
}

