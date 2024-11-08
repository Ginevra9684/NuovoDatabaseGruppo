# NuovoDatabaseGruppo

## DIVISIONE BRANCH

- branch supervisioneDaReadme

funzione : supervisionare i vari brench

- branch modificaDatabase

- [x] funzione : aggiungere tabella utente e cliente
- [x] campi utente : nome e cognome
- [x] campi cliente : id utente e codice cliente

<details>
<summary>Dettagli</summary>
La tabella cliente farà riferimento alla tabella utente tramite id univoco
</details>

- branch divisioneMVC

- [x] funzione : suddividere applicazione in metodi

<details>
<summary>Dettagli</summary>
L'applicazione deve essere suddivisa utilizzando il pattern MVC in modo che:
- il Model contenga il database e i propri metodi
- il controller contenga la logica del main e i richiami ai vari metodi
- la view faccia visualizzare i risultati di tutti i metodi richiamati dal menu del controller
</details>

<details>
<summary>Procedure</summary>

## Task sucessivi

- [x] creare un file ViewProdotti e un file ViewCategorie
- [x] sostituire il metodo Stampa di View con i metodi corrispondenti ai metodi del controller
- [x] i parametri dei metodi di View non prenderanno una variabile stringa ma un oggetto Prodotto (Prodotto prodotto) o un oggetto Categoria ( Categoria categoria), fare attenzione se è una lista o un oggetto singolo
- [x] ShowMainMenu sarà suddiviso in base alle funzioni che richiama con i rispettivi nomi di menu (ShowProductMenu, ShowCategoryMenu, ShowEndMenu)
- [x] creare un modello specifico per Prodotti e Categorie
- [x] modificare il Model del database togliendo il while del reader e ritornandolo nei vari metodi
- [x] modificare il Controller e il Model in modo che il reader venga letto nel Controller all'interno dei vari metodi
- [x] far si che i metodi del controller non passino una stringa alla view ma un modello (es Prodotto, Categoria)

## Nuove funzionalità

- [x] Modello Clienti
- [x] Funzione Menu : 14- visualizza clienti
- [x] Model: Richiesta al database e return reader CRUD
- [x] Controller: nuova opzione switch, reader assegna a un'istanza del modello Cliente, passa la lista clienti a view
- [x] ClientiView: metodo che ha come parametro una lista Cliente, fa visualizzare i clienti
- [x] ClientiView: aggiungere visualizzazione opzione menu (tutte operazini CRUD relative al cliente)
- [x] Controller: aggiunta a ShowMainMenu delle due visualizzazioni menu ClientiView
- [x] Funzione Menu : 15- cerca cliente
- [x] Funzione Menu : 16-Uscire (prima era numero 14 da spostare)

## Aggiustamenti definitivi

- [x] Divisione del controller in ProductController, CategoryController, CustomerController
- [x] Divisione dei menu:
- [x] In ProductView menu relativo ai prodotti
- [x] In ProductController metodi relativi ai prodotti
- [x] In CategoryView menu relativo alle categorie
- [x] In CategoryController metodi relativi alle categorie
- [x] In CustomerView menu relativo ai clienti
- [x] In CustomerController metodi relativi ai clienti
- [x] In BaseView menu principale
- [x] In BaseController richiamati gli altri controller
- [x] Aggiunta nuova tabella ordini
- [x] Creare OrdersView
- [x] Creare OrdersController
- [x] Metodo VisualizzaOrdini
- [x] Metodo InserisciOrdine
- [x] Metodo ModificaOrdine
- [x] Metodo EliminaOrdine
- [x] Aggiunta commenti
- [x] Conversione lingua
- [x] Revisione

</details>

## VERSIONE DEFINITIVA CON SQL

<details>
<summary>Funzionalità</summary>

- Menu base che rimanda a menu categorie, ordini, prodotti, clienti
- Funzionalità CRUD per i sottomenu
- Collegamento ad un database tramite SQL per la permanenza dei dati

</details>

<details>
<summary>File Database con SQL</summary>

    ```C#
    using System.Data.Entity;
    using System.Data.SQLite;
    public class Model
    {
        string path = @"database.db"; // il file deve essere nella stessa cartella del programma db

        public Model()
        {
            CreateDatabase();
        }

        private void CreateDatabase()
        {
            if (!File.Exists(path))
        {
                SQLiteConnection.CreateFile(path); // crea il file del database se non esiste
                SQLiteConnection connection = new SQLiteConnection($"Data Source={path};Version=3;"); // crea la connessione al database se non esiste utilizzando il file appena creato versiion identificata dal numero 3
                connection.Open(); // apre la connessione al database se non è già aperta
                // Definisce il comando SQL per creare le tabelle e inserire alcuni dati di esempio
                string sql = @"
                                CREATE TABLE categorie (
                                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                                    nome TEXT UNIQUE
                                );

                                CREATE TABLE prodotti (
                                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                                    nome TEXT UNIQUE,
                                    prezzo REAL,
                                    giacenza INTEGER CHECK (giacenza >= 0),
                                    id_categoria INTEGER,
                                    FOREIGN KEY (id_categoria) REFERENCES categorie(id)
                                );

                                CREATE TABLE clienti (
                                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                                    nome TEXT NOT NULL
                                );

                                CREATE TABLE ordini (
                                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                                    cliente_id INTEGER,
                                    prodotto_id INTEGER,
                                    quantita INTEGER CHECK (quantita >= 0),
                                    dataAcquisto DATETIME DEFAULT CURRENT_TIMESTAMP,
                                    FOREIGN KEY (cliente_id) REFERENCES clienti(id),
                                    FOREIGN KEY (prodotto_id) REFERENCES prodotti(id)
                                );

                                INSERT INTO categorie (nome) VALUES ('c1');
                                INSERT INTO categorie (nome) VALUES ('c2');
                                INSERT INTO categorie (nome) VALUES ('c3');

                                INSERT INTO prodotti (nome, prezzo, giacenza, id_categoria) VALUES ('p1', 1, 10, 1);
                                INSERT INTO prodotti (nome, prezzo, giacenza, id_categoria) VALUES ('p2', 2, 20, 2);";

                SQLiteCommand command = new SQLiteCommand(sql, connection); // crea il comando sql da eseguire sulla connessione al database se non esiste
                command.ExecuteNonQuery(); // esegue il comando sql sulla connessione al database se non esiste
                connection.Close(); // chiude la connessione al database se non è già chiusa
            }
        }

        public List<Prodotto> CaricaProdotti()  // Menu opzione 1
        {
            List<Prodotto> prodotti = new List<Prodotto>();

            using (SQLiteConnection connection = new SQLiteConnection($"Data Source=database.db;Version=3;"))
            {
                connection.Open();
                string sql = "SELECT * FROM prodotti";
                using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var prodotto = new Prodotto
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                Nome = reader["nome"].ToString(),
                                Prezzo = Convert.ToDecimal(reader["prezzo"]),
                                Giacenza = Convert.ToInt32(reader["giacenza"]),
                                Id_categoria = Convert.ToInt32(reader["id_categoria"])
                            };
                            prodotti.Add(prodotto);
                        }
                    }
                }
            }

            return prodotti;
        }

        public SQLiteDataReader CaricaProdottiOrdinatiPerPrezzo() // Menu opzione 2
        {
            SQLiteConnection connection = new SQLiteConnection($"Data Source=database.db;Version=3;");
            connection.Open();
            string sql = "SELECT * FROM prodotti ORDER BY prezzo"; // crea il comando sql che seleziona tutti i dati dalla tabella prodotti ordinati per prezzo
            SQLiteCommand command = new SQLiteCommand(sql, connection);
            SQLiteDataReader reader = command.ExecuteReader();
            return reader;
        }

        public SQLiteDataReader CaricaProdottiOrdinatiPerQuantita()   // Menu opzione 3
        {
            SQLiteConnection connection = new SQLiteConnection($"Data Source=database.db;Version=3;");
            connection.Open();
            string sql = "SELECT * FROM prodotti ORDER BY giacenza"; // crea il comando sql che seleziona tutti i dati dalla tabella prodotti ordinati per quantita
            SQLiteCommand command = new SQLiteCommand(sql, connection);
            SQLiteDataReader reader = command.ExecuteReader();
            return reader;
        }

        public void ModificaPrezzoProdotto(string nome, decimal prezzo) // Menu opzione 4
        {
            SQLiteConnection connection = new SQLiteConnection($"Data Source=database.db;Version=3;");
            connection.Open();
            string sql = "UPDATE prodotti SET prezzo = @prezzo WHERE nome = @nome"; // crea il comando sql che modifica il prezzo del prodotto con nome uguale a quello inserito
            SQLiteCommand command = new SQLiteCommand(sql, connection);
            command.Parameters.AddWithValue("@prezzo", prezzo);
            command.Parameters.AddWithValue("@nome", nome);
            command.ExecuteNonQuery(); // esegue il comando sql sulla connessione al database ExecuteNonQuery() viene utilizzato per eseguire comandi che non restituiscono dati, ad esempio i comandi INSERT, UPDATE, DELETE
            connection.Close();
        }

        public void EliminaProdotto(string nome)    // Menu opzione 5
        {
            SQLiteConnection connection = new SQLiteConnection($"Data Source=database.db;Version=3;");
            connection.Open();
            string sql = "DELETE FROM prodotti WHERE nome = @nome"; // crea il comando sql che elimina il prodotto con nome uguale a quello inserito
            SQLiteCommand command = new SQLiteCommand(sql, connection);
            command.Parameters.AddWithValue("@nome", nome);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public SQLiteDataReader CaricaProdottoPiuCostoso()    // Menu opzione 6
        {
            SQLiteConnection connection = new SQLiteConnection($"Data Source=database.db;Version=3;");
            connection.Open();
            string sql = "SELECT * FROM prodotti ORDER BY prezzo DESC LIMIT 1"; // crea il comando sql che seleziona tutti i dati dalla tabella prodotti ordinati per prezzo in modo decrescente e ne prende solo il primo
            SQLiteCommand command = new SQLiteCommand(sql, connection);
            SQLiteDataReader reader = command.ExecuteReader();
            return reader;
        }

        public SQLiteDataReader CaricaProdottoMenoCostoso()   // Menu opzione 7
        {
            SQLiteConnection connection = new SQLiteConnection($"Data Source=database.db;Version=3;");
            connection.Open();
            string sql = "SELECT * FROM prodotti ORDER BY prezzo ASC LIMIT 1"; // crea il comando sql che seleziona tutti i dati dalla tabella prodotti ordinati per prezzo in modo crescente e ne prende solo il primo
            SQLiteCommand command = new SQLiteCommand(sql, connection);
            SQLiteDataReader reader = command.ExecuteReader();
        return reader;
        }

        // Aggiunto metodo per visualizzare le categorie disponibili
        public SQLiteDataReader VisualizzaCategorie()
        {
            SQLiteConnection connection = new SQLiteConnection($"Data Source=database.db;Version=3;");
            connection.Open();
            string sql = "SELECT * FROM categorie";
            SQLiteCommand command = new SQLiteCommand(sql, connection);
            SQLiteDataReader reader = command.ExecuteReader();
            return reader;
            // using (SQLiteConnection connection = new SQLiteConnection($"Data Source=database.db;Version=3;"))
            {
                connection.Open();
                string sql = "SELECT * FROM categorie";
                using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())   // Visualizza ogni categoria con ID e nome
                        {
                        Console.WriteLine($"ID: {reader["id"]}, Nome: {reader["nome"]}");
                        }
                    }
                }
            }
        }

        public void InserisciProdotto(string nome, decimal prezzo, int giacenza, int id_categoria) // Menu opzione 8
        {
            using (SQLiteConnection connection = new SQLiteConnection($"Data Source=database.db;Version=3;"))
            {
                connection.Open();
                string sql = "INSERT INTO prodotti (nome, prezzo, giacenza, id_categoria) VALUES (@nome, @prezzo, @giacenza, @id_categoria)";
                using (SQLiteCommand command = new SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@nome", nome);
                    command.Parameters.AddWithValue("@prezzo", Convert.ToDecimal(prezzo));
                    command.Parameters.AddWithValue("@giacenza", Convert.ToInt32(giacenza));
                    command.Parameters.AddWithValue("@id_categoria", Convert.ToInt32(id_categoria));
                    command.ExecuteNonQuery();
                }
            }
        }

        public SQLiteDataReader CaricaProdotto(string nome)   // Menu opzione 9
        {
            SQLiteConnection connection = new SQLiteConnection($"Data Source=database.db;Version=3;");
            connection.Open();
            string sql = "SELECT * FROM prodotti WHERE nome = @nome"; // crea il comando sql che seleziona tutti i dati dalla tabella prodotti con nome uguale a quello inserito
            SQLiteCommand command = new SQLiteCommand(sql, connection);
            command.Parameters.AddWithValue("@nome", nome);
            SQLiteDataReader reader = command.ExecuteReader();
            return reader;
        }

        public SQLiteDataReader VisualizzaProdottiCategoria(int id_categoria) // Menu opzione 10
        {
            SQLiteConnection connection = new SQLiteConnection($"Data Source=database.db;Version=3;");
            connection.Open();
            string sql = "SELECT * FROM prodotti WHERE id_categoria = @id_categoria"; // crea il comando sql che seleziona tutti i dati dalla tabella prodotti con id_categoria uguale a quello inserito
            SQLiteCommand command = new SQLiteCommand(sql, connection);
            command.Parameters.AddWithValue("@id_categoria", Convert.ToInt32(id_categoria));
            SQLiteDataReader reader = command.ExecuteReader();
            return reader;
        }

        public void InserisciCategoria(string nome)    // Menu opzione 11
        {
            SQLiteConnection connection = new SQLiteConnection($"Data Source=database.db;Version=3;");
            connection.Open();
            string sql = "INSERT INTO categorie (nome) VALUES (@nome)"; // crea il comando sql che inserisce una categoria
            SQLiteCommand command = new SQLiteCommand(sql, connection);
            command.Parameters.AddWithValue("@nome", nome);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void EliminaCategoria(string nome)  // Menu opzione 12
        {
            SQLiteConnection connection = new SQLiteConnection($"Data Source=database.db;Version=3;");
            connection.Open();
            string sql = "DELETE FROM categorie WHERE nome = @nome"; // crea il comando sql che elimina la categoria con nome uguale a quello inserito
            SQLiteCommand command = new SQLiteCommand(sql, connection);
            command.Parameters.AddWithValue("@nome", nome);
            command.ExecuteNonQuery();
            connection.Close();
        }

        // inserimento di prodotto chiamando prima la categoria e poi il prodotto in modo da avere in inserimento il nome della categoria invece dell id
        public void InserisciProdottoCategoria(int id_categoria, string nome, decimal prezzo, int giacenza)    // Menu opzione 13
        {
            SQLiteConnection connection = new SQLiteConnection($"Data Source=database.db;Version=3;");
            connection.Open();
            string sql = "INSERT INTO prodotti (nome, prezzo, giacenza, id_categoria) VALUES (@nome, @prezzo, @giacenza, @id_categoria)";
            SQLiteCommand command = new SQLiteCommand(sql, connection);
            command.Parameters.AddWithValue("@nome", nome);
            command.Parameters.AddWithValue("@prezzo", Convert.ToDecimal(prezzo));
            command.Parameters.AddWithValue("@giacenza", Convert.ToInt32(giacenza));
            command.Parameters.AddWithValue("@id_categoria", Convert.ToInt32(id_categoria));
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void InserisciCliente(Cliente cliente) // Menu opsione 15
        {
            using (SQLiteConnection connection = new SQLiteConnection($"Data Source={path};Version=3;"))
            {
                connection.Open();
                string sql = "INSERT INTO clienti (nome) VALUES (@nome)";
                using SQLiteCommand command = new SQLiteCommand(sql, connection);
                command.Parameters.AddWithValue("@nome", cliente.Nome);
                command.ExecuteNonQuery();
            }
        }

        // TODO: I return non devono restituire reader
        public SQLiteDataReader VisualizzaClienti() // Menu opzione 14
        {
            SQLiteConnection connection = new SQLiteConnection($"Data Source=database.db;Version=3;");
            connection.Open();
            string sql = "SELECT * FROM clienti"; // crea il comando sql che seleziona tutti i dati dalla tabella prodotti con id_categoria uguale a quello inserito
            SQLiteCommand command = new SQLiteCommand(sql, connection);
            SQLiteDataReader reader = command.ExecuteReader();
            return reader;
        }
        public void ModificaCliente(Cliente cliente, string nuovoNome)
        {
            SQLiteConnection connection = new SQLiteConnection($"Data Source=database.db;Version=3;");
            connection.Open();
            string sql = "UPDATE clienti SET nome = @nuovoNome WHERE id = @id";
            SQLiteCommand command = new SQLiteCommand(sql, connection);
            command.Parameters.AddWithValue("@nuovoNome", nuovoNome);
            command.Parameters.AddWithValue("@id", cliente.Id);
            command.ExecuteNonQuery();
            connection.Close();
        }
        public void EliminaCliente(Cliente cliente)
        {
            SQLiteConnection connection = new SQLiteConnection($"Data Source=database.db;Version=3;");
            connection.Open();
            string sql = $"DELETE FROM clienti WHERE id = {cliente.Id}"; // crea il comando sql che elimina la categoria con nome uguale a quello inserito
            SQLiteCommand command = new SQLiteCommand(sql, connection);
            command.Parameters.AddWithValue("@id", cliente.Id);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void InserisciOrdine(int clienteId, int prodottoId, int quantita)
        {
            using (SQLiteConnection connection = new SQLiteConnection($"Data Source={path};Version=3;"))
            {
                connection.Open();
                using (SQLiteTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Verifica se c'è abbastanza giacenza
                        string checkStockSql = "SELECT giacenza FROM prodotti WHERE id = @prodottoId";
                        using (SQLiteCommand checkCommand = new SQLiteCommand(checkStockSql, connection, transaction))
                        {
                            checkCommand.Parameters.AddWithValue("@prodottoId", prodottoId);
                            int giacenza = Convert.ToInt32(checkCommand.ExecuteScalar());

                            if (giacenza < quantita)
                            {
                                Console.WriteLine("Quantità insufficiente in magazzino.");
                                return;
                            }
                        }

                        // Inserisce l'ordine
                        string sql = "INSERT INTO ordini (cliente_id, prodotto_id, quantita, dataAcquisto) VALUES (@clienteId, @prodottoId, @quantita, CURRENT_TIMESTAMP)";
                        using (SQLiteCommand command = new SQLiteCommand(sql, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@clienteId", clienteId);
                            command.Parameters.AddWithValue("@prodottoId", prodottoId);
                            command.Parameters.AddWithValue("@quantita", quantita);
                            command.ExecuteNonQuery();
                        }

                        // Aggiorna giacenza del prodotto
                        string updateStockSql = "UPDATE prodotti SET giacenza = giacenza - @quantita WHERE id = @prodottoId";
                        using (SQLiteCommand updateCommand = new SQLiteCommand(updateStockSql, connection, transaction))
                        {
                            updateCommand.Parameters.AddWithValue("@quantita", quantita);
                            updateCommand.Parameters.AddWithValue("@prodottoId", prodottoId);
                            updateCommand.ExecuteNonQuery();
                        }

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        Console.WriteLine($"Errore durante l'inserimento dell'ordine: {ex.Message}");
                    }
                }
            }
        }

        // metodo per visualizzare gli ordini
        public SQLiteDataReader VisualizzaOrdini()
        {
            SQLiteConnection connection = new SQLiteConnection($"Data Source={path};Version=3;");
            connection.Open();

            string sql = @"
                SELECT ordini.id, clienti.nome AS Cliente, prodotti.nome AS Prodotto, ordini.quantita, ordini.dataAcquisto
                FROM ordini
                JOIN clienti ON ordini.cliente_id = clienti.id
                JOIN prodotti ON ordini.prodotto_id = prodotti.id
                ORDER BY ordini.dataAcquisto DESC";

            SQLiteCommand command = new SQLiteCommand(sql, connection);
            SQLiteDataReader reader = command.ExecuteReader();

            return reader;
        }
    }
    ```

</details>

<details>
<summary>Schema(da aggiungere)</summary>

    ```mermaid

    ```

</details>

## VERSIONE DEFINITIVA CON ENTITY

<details>
<summary>Passaggio ad entity</summary>

- [x] Aggiungere i pacchetti per entity framework

- dotnet add package Microsoft.EntityFrameworkCore;
- oppure dotnet add package Microsoft.EntityFrameworkCore.Sqlite;
- dotnet run
- dotnet add package Microsoft.EntityFrameworkCore.Design
- dotnet add package Microsoft.EntityFrameworkCore.Tools
- Se non lo si ha mai installato in locale ----> dotnet tool install --global dotnet-ef

- [x] Visto che Model.cs dovrebbe essere Database.cs lo si commenta e si crea il nuovo file Database.cs
- [x] Creare proprietà DB Context per far si che i modelli corrispondano ai campi delle tabelle
- [x] Interagire con le proprietà DB Context per estrapolare/aggiungere/modificare dati nelle varie tabelle invece di passare per stringhe SQL

- [x] Per creare la migrazione delle DB Context properties dotnet ef migrations add InitialCreate
- [x] Per salvare dotnet ef database update
- [x] Cancellazione del reader dai controller e passaggio dei parametri giusti ad entity

</details>

<details>
<summary>TUTTI I FILE</summary>

<details>
<summary>Models</summary>

<details>
<summary>General</summary>

    ```C#
    public abstract class General
    {
        public virtual int Id { get; set; }
        public virtual string Nome { get; set; } = "";
    }
    ```

</details>

<details>
<summary>Categoria</summary>

    ```C#
    public class Categoria : General
    {

    }
    ```

</details>

<details>
<summary>Cliente</summary>

    ```C#
    public class Cliente : General
    {

    }
    ```

</details>

<details>
<summary>Prodotto</summary>

    ```C#

    ```

</details>

<details>
<summary>Ordine</summary>

    ```C#
    public class Ordine : General
    {
        private DateTime dataAcquisto;
        // private override string Nome = "";

        public override string Nome { get {return $"BRT-{Id}_{Cliente!.Id}"; } }

        // Data in cui è stato effettuato l'acquisto
        public DateTime DataAcquisto { get => dataAcquisto; set => dataAcquisto = value; }

        // Quantità del prodotto acquistato
        //public string ?Quantita { get; set; }
        public int Quantita{get;set;}

        // Cliente associato all'ordine
        public Cliente? Cliente { get; set; }

        // Prodotto associato all'ordine
        public Prodotto? Prodotto { get; set; }
    }
    ```

</details>

<details>
<summary>Database</summary>

    ```C#
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
    ```

</details>

</details>

<details>
<summary>Controllers</summary>

<details>
<summary>Base</summary>

    ```C#
    using System.Data.Common;

    public class BaseController
    {
        // Riferimento al modello dell'applicazione, che gestisce l'accesso e le operazioni sui dati
        // private Model _model;

        private Database _database;
        // Riferimento alla vista principale dell'applicazione utilizzata per visualizzare il menu principale e i messaggi generali
        private BaseView _baseView;

        // Controller specifici per gestire le diverse sezioni dell'applicazione
        private CategoryController _categoryController;
        private ProductController _productController;
        private CustomerController _customerController;

        private OrderController _orderController;

        // Costruttore del controller principale, che riceve come parametri il modello, la vista di base e i controller per categorie prodotti clienti e ordini
        public BaseController(Database database, BaseView baseView, CategoryController categoryController, ProductController productController, CustomerController customerController, OrderController orderController)
        {
            _database = database;  // Inizializza il riferimento al modello, che verrà utilizzato per le operazioni generali sui dati
            _baseView = baseView;         // Inizializza il riferimento alla vista principale
            _categoryController = categoryController;  // Inizializza il controller delle categorie, che sarà chiamato per gestire tutte le operazioni relative alle categorie
            _productController = productController; // Inizializza il controller dei prodotti che sarà chiamato per gestire tutte le operazioni relative ai prodotti
            _customerController = customerController ; // Inizializza il controller dei clienti che sarà chiamato per gestire tutte le operazioni relative ai clienti
            _orderController = orderController ;   // Inizializza il controller degli ordini che sarà chiamato per gestire tutte le operazioni relative agli ordini
        }

    // Metodo principale per gestire il menu dell'applicazione
        public void MainMenu()
        {
            while (true)
            {
                Console.Clear();
                _baseView.ShowMainMenu(); // Mostra il menu principale all'utente
                var input = _baseView.GetInput();  // Ottiene l'input dell'utente
                switch (input)
                {
                    case "1":
                        _productController.ProductsMenu(); // Gestisce il menu dei prodotti
                        break;
                    case "2":
                        _categoryController.CategoryMenu();  // Gestisce il menu delle categorie
                        break;
                    case "3":
                        _customerController.CustomerMenu();  // Gestisce il menu dei clienti
                        break;
                    case "4":
                        _orderController.OrderMenu();   // Gestisce il menu degli ordini
                        break;
                    case "5":
                        _baseView.Stampa("Esci dal programma");
                        return;
                    default:
                        _baseView.Errore();
                        _baseView.Proseguimento();
                        break;
                }
            }
        }
    }
    ```

</details>

<details>
<summary>Category</summary>

    ```C#

    ```

</details>

<details>
<summary>Customer</summary>

    ```C#

    ```

</details>

<details>
<summary>Order</summary>

    ```C#
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;

    public class OrderController
    {
        private Database _database;  // Riferimento al modello per l'accesso ai dati degli ordini
        private OrderView _orderView;  // Riferimento alla vista per visualizzare l'interfaccia degli ordini
        private ProductController _productController; // Riferimento al controller prodotti per richiamarne i metodi
        private CustomerController _customerController; // Riferimento al controller clienti per richiamarne i metodi

        // Costruttore che inizializza il controller degli ordini con il modello, la vista e i controller di prodotti e clienti
        public OrderController(Database database, OrderView orderView, ProductController productController, CustomerController customerController)
        {
            _database = database;
            _orderView = orderView;
            _productController = productController;
            _customerController = customerController;
        }

        // Metodo per mostrare il menu degli ordini
        public void OrderMenu()
        {
            Console.Clear();
            while (true)
            {
                _orderView.ShowOrderMenu();
                var input = _orderView.GetInput();

                switch (input)
                {
                    case "1":
                        AggiungiOrdine();
                        break;
                    case "2":
                        VisualizzaOrdini();
                        break;
                    case "3":
                        ModificaOrdine();
                        break;
                    case "4":
                        EliminaOrdine();
                        break;
                    case "5":
                        return; // Torna al menu principale
                    default:
                        _orderView.Errore();
                        break;
                }
            }
        }

    /*
        // Metodo per aggiungere un nuovo ordine
        private void AggiungiOrdine()
        {
            // Usa il metodo InserisciNuovoOrdine dalla vista per ottenere i dati dell'ordine
            Ordine nuovoOrdine = _orderView.InserisciNuovoOrdine();

            // Chiama il modello per inserire il nuovo ordine
            _model.InserisciOrdine(nuovoOrdine.cliente.Id, nuovoOrdine.prodotto.Id, int.Parse(nuovoOrdine.Quantita));

            _orderView.Stampa("Ordine aggiunto con successo.");
        }
    */

    /*   // Metodo per aggiungere un nuovo ordine (Menu opzione 1)
        private void AggiungiOrdine()
        {
            Ordine nuovoOrdine = _orderView.InserisciNuovoOrdine(); // Estrapola tutti i dati da inserire nel nuovoOrdine
            _database.Ordini.Add(nuovoOrdine);  // Aggiunge il nuovo ordine tramite entity
            _database.SaveChanges();    // Salva le modifiche tramite entity
        }
    */

    // Metodo per aggiungere un ordine (Menu opzione 1)
        private void AggiungiOrdine()
        {
            _productController.VisualizzaProdotti();
            _customerController.VisualizzaClienti();
            // Richiede i dettagli dell'ordine dall'utente tramite la vista e crea un nuovo ordine
            Ordine nuovoOrdine = _orderView.AggiungiOrdine();

            // Recupera il cliente esistente dal database utilizzando l'ID specificato nell'ordine
            // In questo modo si assicura che l'entità cliente sia tracciata dal contesto di Entity Framework
            nuovoOrdine.Cliente = _database.Clienti.Find(nuovoOrdine.Cliente!.Id);

            // Recupera il prodotto esistente dal database utilizzando l'ID specificato nell'ordine
            // Anche qui si garantisce che l'entità prodotto sia tracciata dal contesto di Entity Framework
            nuovoOrdine.Prodotto = _database.Prodotti.Find(nuovoOrdine.Prodotto!.Id);

            // Verifica che il cliente e il prodotto siano validi non null prima di aggiungere l'ordine
            if (nuovoOrdine.Cliente != null && nuovoOrdine.Prodotto != null && nuovoOrdine.Quantita <= nuovoOrdine.Prodotto.Giacenza)
            {
                // Aggiunge il nuovo ordine
                _database.Ordini.Add(nuovoOrdine);
                nuovoOrdine.Prodotto.Giacenza -= nuovoOrdine.Quantita;

                // Salva le modifiche nel database, inclusa la creazione del nuovo ordine
                _database.SaveChanges();

                // Conferma l'aggiunta dell'ordine tramite la vista
                _orderView.Stampa("Ordine aggiunto con successo.");
            }
            else if (nuovoOrdine.Quantita > nuovoOrdine.Prodotto!.Giacenza)
            {
                _orderView.Stampa("Giacenza prodotto non sufficiente");
            }
            else
            {
                // Mostra un messaggio di errore se il cliente o il prodotto non sono stati trovati nel database
                _orderView.Stampa("Errore: Cliente o prodotto non trovato.");
            }
        }



    /*
        // Metodo per visualizzare tutti gli ordini
        private void VisualizzaOrdini()
        {
            // Chiama il metodo `VisualizzaOrdini` nel modello per ottenere un `DbDataReader` che contiene tutti gli ordini
            // Utilizza `using` per assicurarsi che il reader venga chiuso automaticamente una volta completato
            using var reader = _model.VisualizzaOrdini();
            var ordini = new List<Ordine>();

            // Popola la lista degli ordini leggendo dal reader
            while (reader.Read())
            {
                // Crea un nuovo oggetto Ordine con i dati estratti dal reader
                var ordine = new Ordine
                {
                    Id = Convert.ToInt32(reader["id"]),          // Converte l'ID dell'ordine in intero
                    DataAcquisto = Convert.ToDateTime(reader["dataAcquisto"]),        // Converte la data di acquisto in DateTime
                    Quantita = Convert.ToInt32(reader["quantita"]),             // Ottiene la quantità dell'ordine prendendola dlala colonna del db
                    cliente = new Cliente { Nome = reader["Cliente"].ToString() },       // Ottiene il nome del cliente e lo assegna
                    prodotto = new Prodotto { Nome = reader["Prodotto"].ToString() }       // Ottiene il nome del prodotto e lo assegna
                };
                ordini.Add(ordine);  // Aggiunge l'ordine alla lista di ordini
            }

            // Passa la lista di ordini completa alla vista per la visualizzazione all'utente
            _orderView.VisualizzaOrdini(ordini);
        }
    */
    /*  // Metodo per visualizzare tutti gli ordini (Menu opzione 2)
        private void VisualizzaOrdini()
        {
            var ordini = _database.Ordini.ToList(); // Estrapola con entity una lista di ordini
            _orderView.VisualizzaOrdini(ordini);    // Passa la lista alla view
        }*/

        private void VisualizzaOrdini()
        {
            // Carica gli ordini con i dettagli del cliente e del prodotto collegati
            var ordini = _database.Ordini.Include(o => o.Cliente).Include(o => o.Prodotto).ToList();

            // Passa la lista degli ordini alla vista per la visualizzazione
            _orderView.VisualizzaOrdini(ordini);
        }

        // Metodo per modificare un ordine esistente (Menu opzione 3)
        private void ModificaOrdine()
        {
            VisualizzaOrdini();
            _productController.VisualizzaProdotti();
            Ordine ordineDaModificare = _orderView.ModificaOrdine();

            var ordine = _database.Ordini.FirstOrDefault(o => o.Id == ordineDaModificare.Id);   // Cerca un ordine tramite Id cliente
            var prodotto = _database.Prodotti.FirstOrDefault(p => p.Id == ordineDaModificare.Prodotto!.Id);  // Cerca il nuovo prodotto tramite id nei prodotti
            if (ordine != null && prodotto != null && ordineDaModificare.Quantita <= (prodotto.Giacenza + ordine.Quantita - ordineDaModificare.Quantita))
            {
                ordine.Prodotto = prodotto; // Aggiorna il prodotto nell'ordine
                prodotto.Giacenza = prodotto.Giacenza + ordine.Quantita - ordineDaModificare.Quantita;
                ordine.Quantita = ordineDaModificare.Quantita;  // Aggiorna la quantità nell'ordine
                _database.SaveChanges();    // Salva le modifiche
                _orderView.Stampa("Ordine modificato con successo.");
            }
            else if (ordineDaModificare.Quantita > (prodotto!.Giacenza + ordine!.Quantita - ordineDaModificare.Quantita))
            {
                _orderView.Stampa("Giacenza prodotto non sufficiente");
            }
            else
            {
                _orderView.Stampa("Ordine non trovato");
            }
        }

        // Metodo per eliminare un ordine esistente (Menu opzione 4)
        private void EliminaOrdine()
        {
            VisualizzaOrdini();
            int id = _orderView.EliminaOrdine();
            var ordine = _database.Ordini.FirstOrDefault(o => o.Id == id);  // Cerca l'ordine tramite id
            if (ordine != null)
            {
                ordine.Prodotto!.Giacenza += ordine.Quantita;
                _database.Remove(ordine);   // Elimina l'ordine
                _database.SaveChanges();    // Salva le modifiche
                _orderView.Stampa("Ordine eliminato con successo.");
            }
            else
            {
                _orderView.Stampa("Ordine non trovato");
            }
        }
    }
    ```

</details>

<details>
<summary>Product</summary>

    ```C#

    ```

</details>

</details>

<details>
<summary>Views</summary>

<details>
<summary>Base</summary>

    ```C#
    using System;

    public class BaseView
    {
        // Metodo per visualizzare il menu principale con le opzioni disponibili
        public void ShowMainMenu()
        {
            Stampa("MAIN MENU");
            Stampa("1 - Vai al menu prodotti");
            Stampa("2 - Vai al menu categorie");
            Stampa("3 - Vai al menu customers");
            Stampa("4 - Vai al menu ordini");
            Stampa("5 - Esci");
        }

        // Metodo per stampare un messaggio sulla console
        public void Stampa(string testo)
        {
            Console.WriteLine(testo);
        }

        // Metodo per ottenere un input dall'utente, gestendo le eccezioni
        public string GetInput()
        {
            while (true)
            {
                try
                {
                    // Legge l'input dell'utente dalla console
                    string? input = Console.ReadLine();

                    // Verifica che l'input non sia vuoto, nullo o composto solo da spazi
                    if (string.IsNullOrWhiteSpace(input))
                    {
                        Stampa("Errore: l'input non può essere vuoto. Riprova:");
                        continue; // Richiede nuovamente l'input
                    }

                    // Ritorna l'input valido
                    return input;
                }
                catch (Exception ex)
                {
                    // Gestisce eccezioni che potrebbero verificarsi durante la lettura dell'input
                    Stampa($"Errore durante la lettura dell'input: {ex.Message}. Riprova:");
                }
            }
        }

        // Metodo per ottenere un input numerico intero, gestendo le eccezioni
        public int GetIntInput(string prompt)
        {
            while (true)
            {
                // Stampa il messaggio di richiesta di input
                Stampa(prompt);
                try
                {
                    // Ottiene l'input dall'utente e lo converte in un intero
                    string input = GetInput();
                    return int.Parse(input);
                }
                catch (FormatException)
                {
                    // Gestisce l'eccezione se l'input non è un numero intero valido
                    Stampa("Errore: l'input deve essere un numero intero. Riprova:");
                }
                catch (Exception ex)
                {
                    // Gestisce altre eccezioni che potrebbero verificarsi
                    Stampa($"Errore durante la lettura dell'input: {ex.Message}. Riprova:");
                }
            }
        }

        // Metodo per ottenere un input numerico decimale, gestendo le eccezioni
        public double GetDoubleInput(string prompt)
        {
            while (true)
            {
                // Stampa il messaggio di richiesta di input
                Stampa(prompt);
                try
                {
                    // Ottiene l'input dall'utente e lo converte in un numero decimale
                    string input = GetInput();
                    return double.Parse(input);
                }
                catch (FormatException)
                {
                    // Gestisce l'eccezione se l'input non è un numero decimale valido
                    Stampa("Errore: l'input deve essere un numero decimale. Riprova:");
                }
                catch (Exception ex)
                {
                    // Gestisce altre eccezioni che potrebbero verificarsi
                    Stampa($"Errore durante la lettura dell'input: {ex.Message}. Riprova:");
                }
            }
        }

        // Metodo per gestire il proseguimento dopo un'operazione
        public void Proseguimento()
        {
            // Chiede all'utente di premere un tasto per continuare
            Stampa("Premere un tasto per proseguire...");
            Console.ReadKey();
            // Pulisce la console per prepararsi alla prossima operazione
            Console.Clear();
        }

        // Metodo per gestire un errore generico
        public void Errore()
        {
            // Stampa un messaggio di errore generico
            Stampa("Opzione non valida\n");
        }
    }
    ```

</details>

<details>
<summary>Category</summary>

    ```C#

    ```

</details>

<details>
<summary>Customer</summary>

    ```C#

    ```

</details>

<details>
<summary>Order</summary>

    ```C#
    public class OrderView : BaseView
    {
        // Visualizza il menu delle opzioni per la gestione degli ordini
        public void ShowOrderMenu()
        {
            Stampa("MENU ORDINE");
            Stampa("1 - Aggiungere un nuovo ordine");
            Stampa("2 - Visualizzare tutti gli ordini");
            Stampa("3 - Modifica un ordine");
            Stampa("4 - Elimina un ordine");
            Stampa("5 - Torna al menu principale");
        }

        // Crea un nuovo ordine basato sugli input forniti dall'utente (Menu opzione 1)
        public Ordine AggiungiOrdine()
        {
            Ordine ordine = new Ordine();

            // Chiede l'ID del cliente e lo assegna all'ordine
            // Stampa("Inserisci l'ID del cliente:");
            int idCliente = GetIntInput("Inserisci l'ID del cliente:");
            ordine.Cliente = new Cliente { Id = idCliente };

            // Imposta la data di acquisto come data e ora attuale
            ordine.DataAcquisto = DateTime.Now;

            // Chiede l'ID del prodotto e lo assegna all'ordine
            // Stampa("Inserisci l'ID del prodotto:");
            int idProdotto = GetIntInput("Inserisci l'ID del prodotto:");
            ordine.Prodotto = new Prodotto { Id = idProdotto };

            // Chiede la quantità e la assegna all'ordine
            // Stampa("Inserisci la quantità:");
            ordine.Quantita = GetIntInput("Inserisci la quantità:");

            return ordine;
        }

        // Visualizza tutti gli ordini presenti nella lista fornita (Menu opzione 2)
        public void VisualizzaOrdini(List<Ordine> ordini)
        {
            if (ordini.Count == 0)
            {
                Stampa("Nessun ordine trovato."); // Messaggio se non ci sono ordini
                return;
            }

            foreach (var ordine in ordini)
            {
                // Visualizza i dettagli di ogni ordine
                Stampa($"ID Ordine: {ordine.Id}");
                Stampa($"Nome Ordine: {ordine.Nome}");
                Stampa($"Data Acquisto: {ordine.DataAcquisto.ToString("yyyy-MM-dd HH:mm:ss")}");

                // Visualizza il nome del cliente associato, se presente
                if (ordine.Cliente != null)
                {
                    Stampa($"Cliente: {ordine.Cliente.Nome}");
                }
                else
                {
                    Stampa("Cliente: N/A"); // Messaggio se non c'è cliente associato
                }

                // Visualizza il nome del prodotto associato, se presente
                if (ordine.Prodotto != null)
                {
                    Stampa($"Prodotto: {ordine.Prodotto.Nome}");
                }
                else
                {
                    Stampa("Prodotto: N/A"); // Messaggio se non c'è prodotto associato
                }

                Stampa($"Quantità: {ordine.Quantita}");
                Stampa("---------------------------------");
            }
        }

        // Modifica un ordine esistente (Menu opzione 3)
        public Ordine ModificaOrdine()
        {
            Ordine ordine = new Ordine();   // Istanza di un oggetto ordine
            // Stampa("Inserisci l'ID dell'ordine di riferimento:");
            int id = GetIntInput("Inserisci l'ID dell'ordine di riferimento:");  // Richiede l'id dell'ordine di riferimento
            ordine.Id = id;

            // Stampa("Inserisci l'ID del nuovo prodotto :");
            int idProdotto = GetIntInput("Inserisci l'ID del nuovo prodotto :"); // Richiede l'id del nuovo prodotto
            ordine.Prodotto = new Prodotto { Id = idProdotto };

            // Stampa("Inserisci la quantità:");
            ordine.Quantita = GetIntInput("Inserisci la quantità:");    // Richiede la nuova quantità

            return ordine;
        }

        //Elimina un ordine (Menu opzione 4)
        public int EliminaOrdine()
        {
            // Stampa("Inserisci l'ID dell'ordine di riferimento:");
            int id = GetIntInput("Inserisci l'ID dell'ordine di riferimento:");
            return id;
        }
    }

    ```

</details>

<details>
<summary>Product</summary>

    ```C#

    ```

</details>

</details>

<details>
<summary>Program</summary>

    ```C#
    class Program
    {
        static void Main(string[] args)
        {
            // Creazione dell'istanza di Database (contesto di Entity Framework)
            using (var database = new Database())
            {
                // Assicura che il database sia creato se non esiste
                database.Database.EnsureCreated();

                // Creazione delle istanze delle Views
                var categoryView = new CategoryView();
                var productView = new ProductView();
                var customerView = new CustomerView();
                var orderView = new OrderView();
                var baseView = new BaseView();

                // Creazione dei Controller, passando le dipendenze al costruttore
                var categoryController = new CategoryController(database, categoryView);
                var productController = new ProductController(database, productView, categoryController);
                var customerController = new CustomerController(database, customerView);
                var orderController = new OrderController(database, orderView, productController, customerController);
                var baseController = new BaseController(database, baseView, categoryController, productController, customerController, orderController);

                // Avvio del menu principale
                baseController.MainMenu();
            }
        }
    }
    ```

</details>

</details>

# PROGETTAZIONE WEBAPP : STORE OROLOGI

## PIANIFICAZIONE

<details>
<summary>Steps</summary>

- Identificazione delle pagine necessarie alla web app
- Identificazione dei ViewModel per ogni pagina
- Identificazione delle proprietà necessarie per ogni ViewModel
- Decisione del tipo di utenti
- Stabilire le diverse visualizzazione a seconda del tipo di utente
- Identificazione del posizionamento dei link
- Creazione layout senza logiche backend
- Implementazione delle partialViews
- Decisione degli stili condivisi con css
- Listare i metodi necessari per ogni pagina
- Conservazione di fonti multimediali (loghi, fonts, video ecc)
- Inizializzare l'archetico della WebApp
- Creare git.ignore e aggiungere progetto alla sln
- Effettuare lo scaffolding delle pagine entity che si desidera personalizzare
- Controllare la presenza di CDN e pacchetti da installare
- Decisione della lingua
- Decisione dello standard del codice e dei commenti
- Divisione del lavoro su più branch

</details>

## Pagine

- HOME
<details>
<summary>Descrizione</summary>

- Visualizzazione di un carousel di cards con i prodotti più venduti
- Visualizzazione di un carousel di cards con gli ultimi arrivi

</details>

<details>
<summary>Lista link</summary>

- Pagina prodotti
- Pagina dettaglio prodotto

</details>

<details>
<summary>ViewModel</summary>

```C#
public class HomeViewModel
{
    public List<Orologio> ProdottiPiuVenduti
    public List<Orologio> UltimiArrivi
}
```

</details>

<details>
<summary>Metodi Controller</summary>

</details>

<details>
<summary>View</summary>

</details>

- PRODOTTI

<details>
<summary>Descrizione</summary>

VERSIONE NORMALE :

- Visualizzazione di tutti i prodotti inpaginati
- Filtro per prezzo
- Filtro per data di aggiunta
- Filtro per categoria
- Filtro per marca
- Filtro per materiale 
- Filtro per tipologia
- Filtro per Genere
- Su schermo grande i filtri saranno in una sidebar
- Su schermo piccolo i filtri saranno dei pulsanti in alto
- Ogni card ha un pulsante per aggiungere alla wishlist
- Ogni card ha un pulsante per aprire e aggiungere al carrello

AGGIUNTE ADMIN :

- Pulsante card per eliminare il prodotto
- Pulsante card per modificare il prodotto

</details>

<details>
<summary>Lista link</summary>

- Pagina dettaglio prodotto
- Partial view carrello


ADMIN : 

- Pagina elimina prodotto
- Pagina modifica prodotto

</details>

<details>
<summary>ViewModel</summary>

```C#
public class ProdottiViewModel
{
    public List<Orologio> Orologi {get; set;}
    public List<Categoria> Categorie {get; set;}
    public List<Marca> Marche {get; set;}
    public List<Materiale> Materiali {get; set;}
    public List<Tipologia> Tipologie {get; set;}
    public List<Genere> Generi {get; set;}
    public decimal MinPrezzo { get; set; }
    public decimal MaxPrezzo { get; set; }
    public int NumeroPagine { get; set; }
    public int PaginaCorrente { get; set; }
}
```

</details>

<details>
<summary>Metodi Controller</summary>

</details>

<details>
<summary>View</summary>

</details>

- AGGIUNGI PRODOTTO

<details>
<summary>Descrizione</summary>

SOLO ADMIN :

- Permette di visualizzare un form per aggiungere un nuovo prodotto
- Molte caratteristiche potranno essere inserite tramite menu a tendina per poter accedere ad un elenco

</details>

<details>
<summary>Lista link</summary>

- Pagina prodotti

</details>

<details>
<summary>ViewModel</summary>

```C#
public class AggiungiProdottoViewModel
{
    public Orologio Orologio {get; set;}
    public List<Categoria> Categorie {get; set;}
    public List<Marca> Marche {get; set;}
    public List<Materiale> Materiali {get; set;}
    public List<Tipologia> Tipologie {get; set;}
    public List<Genere> Generi {get; set;}
}
```

</details>

<details>
<summary>Metodi Controller</summary>

</details>

<details>
<summary>View</summary>

</details>

- ELIMINA PRODOTTO

<details>
<summary>Descrizione</summary>

SOLO ADMIN :

- Permette di visualizzare le caratteristiche principali del prodotto e di eliminarlo

</details>

<details>
<summary>Lista link</summary>

- Pagina prodotti
- Pagina dettaglio prodotto

</details>

<details>
<summary>ViewModel</summary>

```C#
public class EliminaProdottoViewModel
{
    public Orologio Orologio {get; set;}
}
```

</details>

<details>
<summary>Metodi Controller</summary>

</details>

<details>
<summary>View</summary>

</details>

- DETTAGLIO PRODOTTO

<details>
<summary>Descrizione</summary>

- Permette di visualizzare tutti i dettagli specifici di un oggetto
- Contiene una descrizione aggiuntiva dettagliata
- La pagina avrà una sezione con sfondo diverso per le specifiche tecniche
- Le specifiche saranno visualizzate tramite tab panels

</details>

<details>
<summary>Lista link</summary>

- Partial view carrello
- Pagina prodotti

</details>

<details>
<summary>ViewModel</summary>

```C#
public class DettaglioProdottoViewModel
{
    public Orologio Orologio {get; set;}
    public string Descrizione {get; set;}
}
```

</details>

<details>
<summary>Metodi Controller</summary>

</details>

<details>
<summary>View</summary>

</details>

- PROFILO UTENTE
<details>
<summary>Descrizione</summary>

</details>

<details>
<summary>Lista link</summary>

</details>

<details>
<summary>ViewModel</summary>

```C#
public class ProfiloUtenteViewModel
{
    public Cliente Cliente {get; set;}
    public List<Ordine> Ordini {get; set;}
    public List<Orologio> Wishlist {get; set;}
}
```

</details>

<details>
<summary>Metodi Controller</summary>

</details>

<details>
<summary>View</summary>

</details>

- PROFILO ADMIN
<details>
<summary>Descrizione</summary>

</details>

<details>
<summary>Lista link</summary>

</details>

<details>
<summary>ViewModel</summary>

```C#
public class ProfiloAdminViewModel
{

}
```

</details>

<details>
<summary>Metodi Controller</summary>

</details>

<details>
<summary>View</summary>

</details>

- LOGIN
- REGISTER
- CARRELLO
<details>
<summary>Descrizione</summary>

</details>

<details>
<summary>Lista link</summary>

</details>

<details>
<summary>ViewModel</summary>

```C#
public class CarrelloViewModel
{
    public List<Orologio> Carrello {get; set;}
}
```

</details>

<details>
<summary>Metodi Controller</summary>

</details>

<details>
<summary>View</summary>

</details>

- ORDINI
<details>
<summary>Descrizione</summary>

</details>

<details>
<summary>Lista link</summary>

</details>

<details>
<summary>ViewModel</summary>

```C#
public class OrdiniViewModel
{

}
```

</details>

<details>
<summary>Metodi Controller</summary>

</details>

<details>
<summary>View</summary>

</details>

<details>
<summary>MODELLI GENERICI</summary>

GENERALE

```c#
public abstract class General
{
    public virtual int Id { get; set; }
    public virtual string Nome { get; set; } = "";
}

```

PRODOTTO

```c#
public class Prodotto : General
{
    public decimal Prezzo { get; set; } // Prezzo del prodotto
    public int Giacenza { get; set; }   // Quantità disponibile in magazzino
    public string? Colore { get; set; }
    public Categoria? Categoria { get; set; }  // Relazione con la categoria
    // Identificatore della categoria a cui appartiene il prodotto
    public int? Id_categoria { get; set; } //Rimuovere id_categoria 
    public Marca? Marca{get; set;}    
    // public int? MarcaId;
}

```

OROLOGIO

```c#
public class Orologio : Prodotto
{
    public string Modello{ get; set; }
    public string Referenza{ get; set; }
    public Materiale Materiale { get; set; }
    public Tipologia Tipologia { get; set; }
    public int Diametro { get; set; }
    public Genere Genere {get; set; }
}

```

CATEGORIA

```c#
public class Categoria : General
{

} 

```

MARCA

```c#
public class Marca : General
{

} 

```

MATERIALE 

```c#
public class Materiale : General
{

} 

```

TIPOLOGIA 

```c#
public class Tipologia : General
{

} 

```

GENERE 

```c#
public class Genere : General
{

} 

```


</details>

<details>
<summary>CDN</summary>

</details>

<details>
<summary>Loghi</summary>

</details>

<details>
<summary>Fonts</summary>

</details>

<details>
<summary>CSS</summary>

```CSS

```

</details>

<details>
<summary>Partial Views</summary>

```HTML

```

</details>

<details>
<summary>Standards codice</summary>

- Metodi scritti in PascalCase
- Proprietà dei modelli scritti in PascalCase
- Variabili scritte in camelCase
- Commenti corti e non ripetitivi
- Corrispondenza delle variabili tra i vari file

</details>

<details>
<summary>Schema collegamenti pagine</summary>

```mermaid

```

</details>

<details>
<summary>Ultimi Steps</summary>

- Controllo eccezioni
- Revisione commenti e nomenclature
- Test App

</details>
