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
        options.UseSqlite($"Data Source = {AppContext.BaseDirectory}..\\..\\..\\database.db");  // Usa un database SQLite
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Prodotto>()
            .HasOne(p => p.Categoria)
            .WithMany()
            .HasForeignKey(p => p.Id_categoria)
            .OnDelete(DeleteBehavior.SetNull); // Imposta il riferimento a null quando la categoria viene eliminata
    }
}