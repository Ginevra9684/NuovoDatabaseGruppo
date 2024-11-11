public class Prodotto : General
{
    // Prezzo del prodotto
    public decimal Prezzo { get; set; }

    // Quantità disponibile in magazzino
    public int Giacenza { get; set; }

    // // Relazione con Marca
    // public Marca Marca { get; set; }

    // // Colore del prodotto
    // public string Colore { get; set; }

    // Relazione con Categoria
    public Categoria Categoria { get; set; }
    public int Id_categoria { get; internal set; }
}

