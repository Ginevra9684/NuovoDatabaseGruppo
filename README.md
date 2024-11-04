# NuovoDatabaseGruppo

## DIVISIONE BRANCH

- branch supervisioneDaReadme

funzione : supervisionare i vari brench

- branch modificaDatabase

- [x]  funzione : aggiungere tabella utente e cliente
- [x]  campi utente : nome e cognome
- [x]  campi cliente : id utente e codice cliente

<details>
<summary>Dettagli</summary>
La tabella cliente farà riferimento alla tabella utente tramite id univoco
</details>


- branch divisioneMVC

- [x]  funzione : suddividere applicazione in metodi

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
<summary>File Database con SQL (da finire di scommentare)</summary>

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

//     public void InserisciProdotto(string nome, decimal prezzo, int giacenza, int id_categoria) // Menu opzione 8
//     {
//         using (SQLiteConnection connection = new SQLiteConnection($"Data Source=database.db;Version=3;"))
//         {
//             connection.Open();
//             string sql = "INSERT INTO prodotti (nome, prezzo, giacenza, id_categoria) VALUES (@nome, @prezzo, @giacenza, @id_categoria)";
//             using (SQLiteCommand command = new SQLiteCommand(sql, connection))
//             {
//                 command.Parameters.AddWithValue("@nome", nome);
//                 command.Parameters.AddWithValue("@prezzo", Convert.ToDecimal(prezzo));
//                 command.Parameters.AddWithValue("@giacenza", Convert.ToInt32(giacenza));
//                 command.Parameters.AddWithValue("@id_categoria", Convert.ToInt32(id_categoria));
//                 command.ExecuteNonQuery();
//             }
//         }
//     }

//     public SQLiteDataReader CaricaProdotto(string nome)   // Menu opzione 9
//     {
//         SQLiteConnection connection = new SQLiteConnection($"Data Source=database.db;Version=3;");
//         connection.Open();
//         string sql = "SELECT * FROM prodotti WHERE nome = @nome"; // crea il comando sql che seleziona tutti i dati dalla tabella prodotti con nome uguale a quello inserito
//         SQLiteCommand command = new SQLiteCommand(sql, connection);
//         command.Parameters.AddWithValue("@nome", nome);
//         SQLiteDataReader reader = command.ExecuteReader();
//         return reader;
//     }

//     public SQLiteDataReader VisualizzaProdottiCategoria(int id_categoria) // Menu opzione 10
//     {
//         SQLiteConnection connection = new SQLiteConnection($"Data Source=database.db;Version=3;");
//         connection.Open();
//         string sql = "SELECT * FROM prodotti WHERE id_categoria = @id_categoria"; // crea il comando sql che seleziona tutti i dati dalla tabella prodotti con id_categoria uguale a quello inserito
//         SQLiteCommand command = new SQLiteCommand(sql, connection);
//         command.Parameters.AddWithValue("@id_categoria", Convert.ToInt32(id_categoria));
//         SQLiteDataReader reader = command.ExecuteReader();
//         return reader;
//     }

//     public void InserisciCategoria(string nome)    // Menu opzione 11
//     {
//         SQLiteConnection connection = new SQLiteConnection($"Data Source=database.db;Version=3;");
//         connection.Open();
//         string sql = "INSERT INTO categorie (nome) VALUES (@nome)"; // crea il comando sql che inserisce una categoria
//         SQLiteCommand command = new SQLiteCommand(sql, connection);
//         command.Parameters.AddWithValue("@nome", nome);
//         command.ExecuteNonQuery();
//         connection.Close();
//     }

//     public void EliminaCategoria(string nome)  // Menu opzione 12
//     {
//         SQLiteConnection connection = new SQLiteConnection($"Data Source=database.db;Version=3;");
//         connection.Open();
//         string sql = "DELETE FROM categorie WHERE nome = @nome"; // crea il comando sql che elimina la categoria con nome uguale a quello inserito
//         SQLiteCommand command = new SQLiteCommand(sql, connection);
//         command.Parameters.AddWithValue("@nome", nome);
//         command.ExecuteNonQuery();
//         connection.Close();
//     }

//     // inserimento di prodotto chiamando prima la categoria e poi il prodotto in modo da avere in inserimento il nome della categoria invece dell id
//     public void InserisciProdottoCategoria(int id_categoria, string nome, decimal prezzo, int giacenza)    // Menu opzione 13
//     {
//         SQLiteConnection connection = new SQLiteConnection($"Data Source=database.db;Version=3;");
//         connection.Open();
//         string sql = "INSERT INTO prodotti (nome, prezzo, giacenza, id_categoria) VALUES (@nome, @prezzo, @giacenza, @id_categoria)";
//         SQLiteCommand command = new SQLiteCommand(sql, connection);
//         command.Parameters.AddWithValue("@nome", nome);
//         command.Parameters.AddWithValue("@prezzo", Convert.ToDecimal(prezzo));
//         command.Parameters.AddWithValue("@giacenza", Convert.ToInt32(giacenza));
//         command.Parameters.AddWithValue("@id_categoria", Convert.ToInt32(id_categoria));
//         command.ExecuteNonQuery();
//         connection.Close();
//     }

//     public void InserisciCliente(Cliente cliente) // Menu opsione 15
//     {
//         using (SQLiteConnection connection = new SQLiteConnection($"Data Source={path};Version=3;"))
//         {
//             connection.Open();
//             string sql = "INSERT INTO clienti (nome) VALUES (@nome)";
//             using SQLiteCommand command = new SQLiteCommand(sql, connection);
//             command.Parameters.AddWithValue("@nome", cliente.Nome);
//             command.ExecuteNonQuery();
//         }
//     }

//     // TODO: I return non devono restituire reader
//     public SQLiteDataReader VisualizzaClienti() // Menu opzione 14
//     {
//         SQLiteConnection connection = new SQLiteConnection($"Data Source=database.db;Version=3;");
//         connection.Open();
//         string sql = "SELECT * FROM clienti"; // crea il comando sql che seleziona tutti i dati dalla tabella prodotti con id_categoria uguale a quello inserito
//         SQLiteCommand command = new SQLiteCommand(sql, connection);
//         SQLiteDataReader reader = command.ExecuteReader();
//         return reader;
//     }
//     public void ModificaCliente(Cliente cliente, string nuovoNome)
//     {
//         SQLiteConnection connection = new SQLiteConnection($"Data Source=database.db;Version=3;");
//         connection.Open();
//         string sql = "UPDATE clienti SET nome = @nuovoNome WHERE id = @id";
//         SQLiteCommand command = new SQLiteCommand(sql, connection);
//         command.Parameters.AddWithValue("@nuovoNome", nuovoNome);
//         command.Parameters.AddWithValue("@id", cliente.Id);
//         command.ExecuteNonQuery();
//         connection.Close();
//     }
//     public void EliminaCliente(Cliente cliente)
//     {
//         SQLiteConnection connection = new SQLiteConnection($"Data Source=database.db;Version=3;");
//         connection.Open();
//         string sql = $"DELETE FROM clienti WHERE id = {cliente.Id}"; // crea il comando sql che elimina la categoria con nome uguale a quello inserito
//         SQLiteCommand command = new SQLiteCommand(sql, connection);
//         command.Parameters.AddWithValue("@id", cliente.Id);
//         command.ExecuteNonQuery();
//         connection.Close();
//     }

//     public void InserisciOrdine(int clienteId, int prodottoId, int quantita)
//     {
//         using (SQLiteConnection connection = new SQLiteConnection($"Data Source={path};Version=3;"))
//         {
//             connection.Open();
//             using (SQLiteTransaction transaction = connection.BeginTransaction())
//             {
//                 try
//                 {
//                     // Verifica se c'è abbastanza giacenza
//                     string checkStockSql = "SELECT giacenza FROM prodotti WHERE id = @prodottoId";
//                     using (SQLiteCommand checkCommand = new SQLiteCommand(checkStockSql, connection, transaction))
//                     {
//                         checkCommand.Parameters.AddWithValue("@prodottoId", prodottoId);
//                         int giacenza = Convert.ToInt32(checkCommand.ExecuteScalar());

//                         if (giacenza < quantita)
//                         {
//                             Console.WriteLine("Quantità insufficiente in magazzino.");
//                             return;
//                         }
//                     }

//                     // Inserisce l'ordine
//                     string sql = "INSERT INTO ordini (cliente_id, prodotto_id, quantita, dataAcquisto) VALUES (@clienteId, @prodottoId, @quantita, CURRENT_TIMESTAMP)";
//                     using (SQLiteCommand command = new SQLiteCommand(sql, connection, transaction))
//                     {
//                         command.Parameters.AddWithValue("@clienteId", clienteId);
//                         command.Parameters.AddWithValue("@prodottoId", prodottoId);
//                         command.Parameters.AddWithValue("@quantita", quantita);
//                         command.ExecuteNonQuery();
//                     }

//                     // Aggiorna giacenza del prodotto
//                     string updateStockSql = "UPDATE prodotti SET giacenza = giacenza - @quantita WHERE id = @prodottoId";
//                     using (SQLiteCommand updateCommand = new SQLiteCommand(updateStockSql, connection, transaction))
//                     {
//                         updateCommand.Parameters.AddWithValue("@quantita", quantita);
//                         updateCommand.Parameters.AddWithValue("@prodottoId", prodottoId);
//                         updateCommand.ExecuteNonQuery();
//                     }

//                     transaction.Commit();
//                 }
//                 catch (Exception ex)
//                 {
//                     transaction.Rollback();
//                     Console.WriteLine($"Errore durante l'inserimento dell'ordine: {ex.Message}");
//                 }
//             }
//         }
//     }

//     // metodo per visualizzare gli ordini
//     public SQLiteDataReader VisualizzaOrdini()
//     {
//         SQLiteConnection connection = new SQLiteConnection($"Data Source={path};Version=3;");
//         connection.Open();

//         string sql = @"
//             SELECT ordini.id, clienti.nome AS Cliente, prodotti.nome AS Prodotto, ordini.quantita, ordini.dataAcquisto
//             FROM ordini
//             JOIN clienti ON ordini.cliente_id = clienti.id
//             JOIN prodotti ON ordini.prodotto_id = prodotti.id
//             ORDER BY ordini.dataAcquisto DESC";

//         SQLiteCommand command = new SQLiteCommand(sql, connection);
//         SQLiteDataReader reader = command.ExecuteReader();

//         return reader;
//     }
// }
    ```

</details>

<details>
<summary>Schema(da aggiungere)</summary>

    ```mermaid

    ```

</details>




## PASSAGGIO AD ENTITY FRAMEWORK

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

