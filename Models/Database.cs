using Microsoft.EntityFrameworkCore;

public class Database : DbContext
{

//---TABELLE DATABASE-----------------------------------------------------------------------------------------------------------------
   public DbSet<Prodotto> Prodotti { get; set; }
    public DbSet<Categoria> Categorie { get; set; }
    public DbSet<Cliente> Clienti { get; set; }
    public DbSet<Ordine> Ordini { get; set; }
//------------------------------------------------------------------------------------------------------------------------------------
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite("Data Source = database.db");  // Usa un database SQLite
    }
}