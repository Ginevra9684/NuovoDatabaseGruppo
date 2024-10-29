using Microsoft.EntityFrameworkCore;

public class Database : DbContext
{

//---TABELLE DATABASE-----------------------------------------------------------------------------------------------------------------
    private DbSet<Prodotto> _prodotti { get; set; }
    private DbSet<Categoria> _categorie { get; set; }
    private DbSet<Cliente> _clienti { get; set; }
    private DbSet<Ordine> _ordini { get; set; }
//------------------------------------------------------------------------------------------------------------------------------------
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite("Data Source = database.db");  // Usa un database SQLite
    }
}