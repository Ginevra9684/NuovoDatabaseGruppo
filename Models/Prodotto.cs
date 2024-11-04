public class Prodotto : General, IComparable<Prodotto>
{
    // Prezzo del prodotto
    public double Prezzo { get; set; }
    
    // Quantità disponibile in magazzino
    public int Giacenza { get; set; }
    
    // Identificatore della categoria a cui appartiene il prodotto
    public int? Id_categoria { get; set; }

     public Categoria? Categoria { get; set; }  // Relazione con la categoria

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