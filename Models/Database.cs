using Microsoft.EntityFrameworkCore;

public class Database : DbContext
{

  //---TABELLE DATABASE-----------------------------------------------------------------------------------------------------------------
  // TABELLE DATABASE
  public DbSet<Prodotto> Prodotti { get; set; }
  public DbSet<Categoria> Categorie { get; set; }
  public DbSet<Cliente> Clienti { get; set; }
  public DbSet<Ordine> Ordini { get; set; }
  public DbSet<Marca> Marche { get; set; }
  public DbSet<Materiale> Materiali { get; set; }
  public DbSet<Genere> Generi { get; set; }
  public DbSet<Tipologia> Tipologie { get; set; }
  //------------------------------------------------------------------------------------------------------------------------------------
  protected override void OnConfiguring(DbContextOptionsBuilder options)
  {
    // options.UseSqlite($"Data Source = database.db");  // Usa un database SQLite
    options.UseSqlite($"Data Source = {AppContext.BaseDirectory}..\\..\\..\\database.db");  // Usa un database SQLite, utile per il debug
  }

  /*  protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Prodotto>()
            .HasOne(p => p.Categoria)
            .WithMany()
            .HasForeignKey(p => p.Id_categoria)
            .OnDelete(DeleteBehavior.SetNull); // Imposta il riferimento a null quando la categoria viene eliminata
    }*/

  /*  public void SaveChanges()
    {
        base.SaveChanges(); // Salva le modifiche
    }  */
}