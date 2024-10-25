using System.Data.SQLite;
public class Model
{
    string path = @"database.db"; // il file deve essere nella stessa cartella del programma db

    public Model()
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
                                quantita INTEGER CHECK (quantita >= 0), 
                                id_categoria INTEGER, 
                                FOREIGN KEY (id_categoria) REFERENCES categorie(id)
                            );
                            
                            CREATE TABLE utente (
                                id INTEGER PRIMARY KEY AUTOINCREMENT, 
                                nome TEXT NOT NULL, 
                                cognome TEXT NOT NULL
                            );
                            
                            CREATE TABLE cliente (
                                id INTEGER PRIMARY KEY AUTOINCREMENT, 
                                codice_cliente TEXT UNIQUE, 
                                id_utente INTEGER,
                                FOREIGN KEY (id_utente) REFERENCES utente(id)
                            );
                            
                            INSERT INTO categorie (nome) VALUES ('c1');
                            INSERT INTO categorie (nome) VALUES ('c2');
                            INSERT INTO categorie (nome) VALUES ('c3');
                            
                            INSERT INTO prodotti (nome, prezzo, quantita, id_categoria) VALUES ('p1', 1, 10, 1);
                            INSERT INTO prodotti (nome, prezzo, quantita, id_categoria) VALUES ('p2', 2, 20, 2);
                            
                            INSERT INTO utente (nome, cognome) VALUES ('Mario', 'Rossi');
                            INSERT INTO utente (nome, cognome) VALUES ('Luigi', 'Verdi');
                            
                            INSERT INTO cliente (codice_cliente, id_utente) VALUES ('CL001', 1);
                            INSERT INTO cliente (codice_cliente, id_utente) VALUES ('CL002', 2);";

            SQLiteCommand command = new SQLiteCommand(sql, connection); // crea il comando sql da eseguire sulla connessione al database se non esiste
            command.ExecuteNonQuery(); // esegue il comando sql sulla connessione al database se non esiste
            connection.Close(); // chiude la connessione al database se non è già chiusa
        }
    }
/*
    public string VisualizzaProdotti()  // Menu opzione 1
    {
        SQLiteConnection connection = new SQLiteConnection($"Data Source=database.db;Version=3;"); // crea la connessione di nuovo perché è stata chiusa alla fine del while in modo da poter visualizzare i dati aggiornati
        connection.Open();
        string sql = "SELECT * FROM prodotti"; // crea il comando sql che seleziona tutti i dati dalla tabella prodotti
        SQLiteCommand command = new SQLiteCommand(sql, connection); // crea il comando sql da eseguire sulla connessione al database
        SQLiteDataReader reader = command.ExecuteReader(); // esegue il comando sql sulla connessione al database e salva i dati in reader che è un oggetto di tipo SQLiteDataReader incaricato di leggere i dati
        string stringa="";
        while (reader.Read())
        {
            stringa = stringa + $"id: {reader["id"]}, nome: {reader["nome"]}, prezzo: {reader["prezzo"]}, quantita: {reader["quantita"]}, id_categoria: {reader["id_categoria"]}\n";
        }
        connection.Close(); // chiude la connessione al database se non è già chiusa
        return stringa;
    }
*/
    // METODO APPROPRIATO
    public SQLiteDataReader CaricaProdotti()  // Menu opzione 1
    {
        SQLiteConnection connection = new SQLiteConnection($"Data Source=database.db;Version=3;"); // crea la connessione di nuovo perché è stata chiusa alla fine del while in modo da poter visualizzare i dati aggiornati
        connection.Open();
        string sql = "SELECT * FROM prodotti"; // crea il comando sql che seleziona tutti i dati dalla tabella prodotti
        SQLiteCommand command = new SQLiteCommand(sql, connection); // crea il comando sql da eseguire sulla connessione al database
        SQLiteDataReader reader = command.ExecuteReader(); // esegue il comando sql sulla connessione al database e salva i dati in reader che è un oggetto di tipo SQLiteDataReader incaricato di leggere i dati
        
        // BISOGNA SPOSTARE IL WHILE AL CONTROLLER E RITORNARE IL READER INVECE DELLA STRINGA E LA CHIUSURA DELLA CONNESSIONE VA NELL'EXIT
        return reader;
    }

    public string VisualizzaProdottiOrdinatiPerPrezzo() // Menu opzione 2
    {
        SQLiteConnection connection = new SQLiteConnection($"Data Source=database.db;Version=3;");
        connection.Open();
        string sql = "SELECT * FROM prodotti ORDER BY prezzo"; // crea il comando sql che seleziona tutti i dati dalla tabella prodotti ordinati per prezzo
        SQLiteCommand command = new SQLiteCommand(sql, connection);
        SQLiteDataReader reader = command.ExecuteReader();
        string stringa="";
        while (reader.Read())
        {
            stringa = stringa + $"id: {reader["id"]}, nome: {reader["nome"]}, prezzo: {reader["prezzo"]}, quantita: {reader["quantita"]}, id_categoria: {reader["id_categoria"]}\n";
        }
        connection.Close();
        return stringa;
    }

    public string VisualizzaProdottiOrdinatiPerQuantita()   // Menu opzione 3
    {
        SQLiteConnection connection = new SQLiteConnection($"Data Source=database.db;Version=3;");
        connection.Open();
        string sql = "SELECT * FROM prodotti ORDER BY quantita"; // crea il comando sql che seleziona tutti i dati dalla tabella prodotti ordinati per quantita
        SQLiteCommand command = new SQLiteCommand(sql, connection);
        SQLiteDataReader reader = command.ExecuteReader();
        string stringa="";
        while (reader.Read())
        {
            stringa = stringa + $"id: {reader["id"]}, nome: {reader["nome"]}, prezzo: {reader["prezzo"]}, quantita: {reader["quantita"]}, id_categoria: {reader["id_categoria"]}\n";
        }
        connection.Close();
        return stringa;
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

    public string VisualizzaProdottoPiuCostoso()    // Menu opzione 6
    {
        SQLiteConnection connection = new SQLiteConnection($"Data Source=database.db;Version=3;");
        connection.Open();
        string sql = "SELECT * FROM prodotti ORDER BY prezzo DESC LIMIT 1"; // crea il comando sql che seleziona tutti i dati dalla tabella prodotti ordinati per prezzo in modo decrescente e ne prende solo il primo
        SQLiteCommand command = new SQLiteCommand(sql, connection);
        SQLiteDataReader reader = command.ExecuteReader();
        string stringa="";
        while (reader.Read())
        {
            stringa = stringa + $"id: {reader["id"]}, nome: {reader["nome"]}, prezzo: {reader["prezzo"]}, quantita: {reader["quantita"]}, id_categoria: {reader["id_categoria"]}\n";
        }
        connection.Close();
        return stringa;
    }

    public string VisualizzaProdottoMenoCostoso()   // Menu opzione 7
    {
        SQLiteConnection connection = new SQLiteConnection($"Data Source=database.db;Version=3;");
        connection.Open();
        string sql = "SELECT * FROM prodotti ORDER BY prezzo ASC LIMIT 1"; // crea il comando sql che seleziona tutti i dati dalla tabella prodotti ordinati per prezzo in modo crescente e ne prende solo il primo
        SQLiteCommand command = new SQLiteCommand(sql, connection);
        SQLiteDataReader reader = command.ExecuteReader();
        string stringa="";
        while (reader.Read())
        {
            stringa = stringa + $"id: {reader["id"]}, nome: {reader["nome"]}, prezzo: {reader["prezzo"]}, quantita: {reader["quantita"]}, id_categoria: {reader["id_categoria"]}\n";
        }
        connection.Close();
        return stringa;
    }

// Aggiunto metodo per visualizzare le categorie disponibili
    public void VisualizzaCategorie()
    {
        using (SQLiteConnection connection = new SQLiteConnection($"Data Source=database.db;Version=3;"))
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


    public void InserisciProdotto() // Menu opzione 8
    {
        Console.WriteLine("inserisci il nome del prodotto");
        string nome = Console.ReadLine()!;
        Console.WriteLine("inserisci il prezzo del prodotto");
        string prezzo = Console.ReadLine()!;
        Console.WriteLine("inserisci la quantità del prodotto");
        string quantita = Console.ReadLine()!;
        // Visualizza le categorie disponibili
        Console.WriteLine("Categorie disponibili:");
        VisualizzaCategorie(); // Chiamata al metodo che visualizza le categorie con i loro ID
        Console.WriteLine("inserisci l'id della categoria del prodotto");
        string id_categoria = Console.ReadLine()!;
        SQLiteConnection connection = new SQLiteConnection($"Data Source=database.db;Version=3;");
        connection.Open();
        string sql = "INSERT INTO prodotti (nome, prezzo, quantita, id_categoria) VALUES (@nome, @prezzo, @quantita, @id_categoria)";
        SQLiteCommand command = new SQLiteCommand(sql, connection);
        command.Parameters.AddWithValue("@nome", nome);
        command.Parameters.AddWithValue("@prezzo", Convert.ToDecimal(prezzo));
        command.Parameters.AddWithValue("@quantita", Convert.ToInt32(quantita));
        command.Parameters.AddWithValue("@id_categoria", Convert.ToInt32(id_categoria));
        command.ExecuteNonQuery();
        connection.Close();
    }

    public string VisualizzaProdotto(string nome)   // Menu opzione 9
    {
        SQLiteConnection connection = new SQLiteConnection($"Data Source=database.db;Version=3;");
        connection.Open();
        string sql =  "SELECT * FROM prodotti WHERE nome = @nome"; // crea il comando sql che seleziona tutti i dati dalla tabella prodotti con nome uguale a quello inserito
        SQLiteCommand command = new SQLiteCommand(sql, connection);
        command.Parameters.AddWithValue("@nome", nome);
        SQLiteDataReader reader = command.ExecuteReader();
        string stringa="";
        while (reader.Read())
        {
            stringa = stringa + $"id: {reader["id"]}, nome: {reader["nome"]}, prezzo: {reader["prezzo"]}, quantita: {reader["quantita"]}, id_categoria: {reader["id_categoria"]}\n";
        }
        connection.Close();
        return stringa;
    }

    public string VisualizzaProdottiCategoria() // Menu opzione 10
    {
        VisualizzaCategorie();
        Console.WriteLine("inserisci l'id della categoria");
        string id_categoria = Console.ReadLine()!;
        SQLiteConnection connection = new SQLiteConnection($"Data Source=database.db;Version=3;");
        connection.Open();
        string sql ="SELECT * FROM prodotti WHERE id_categoria = @id_categoria"; // crea il comando sql che seleziona tutti i dati dalla tabella prodotti con id_categoria uguale a quello inserito
        SQLiteCommand command = new SQLiteCommand(sql, connection);
        command.Parameters.AddWithValue("@id_categoria", Convert.ToInt32(id_categoria));
        SQLiteDataReader reader = command.ExecuteReader();
        string stringa="";
        while (reader.Read())
        {
            stringa = stringa + $"id: {reader["id"]}, nome: {reader["nome"]}, prezzo: {reader["prezzo"]}, quantita: {reader["quantita"]}, id_categoria: {reader["id_categoria"]}\n";
        }
        connection.Close();
        return stringa;
    }

    public void InserisciCategoria()    // Menu opzione 11
    {
        VisualizzaCategorie();
        Console.WriteLine("inserisci il nome della nuova categoria");
        string nome = Console.ReadLine()!;
        SQLiteConnection connection = new SQLiteConnection($"Data Source=database.db;Version=3;");
        connection.Open();
        string sql = "INSERT INTO categorie (nome) VALUES (@nome)"; // crea il comando sql che inserisce una categoria
        SQLiteCommand command = new SQLiteCommand(sql, connection);
        command.Parameters.AddWithValue("@nome", nome);
        command.ExecuteNonQuery();
        connection.Close();
    }

    public void EliminaCategoria()  // Menu opzione 12
    {
        VisualizzaCategorie();
        Console.WriteLine("inserisci il nome della categoria da eliminare");
        string nome = Console.ReadLine()!;
        SQLiteConnection connection = new SQLiteConnection($"Data Source=database.db;Version=3;");
        connection.Open();
        string sql = "DELETE FROM categorie WHERE nome = @nome"; // crea il comando sql che elimina la categoria con nome uguale a quello inserito
        SQLiteCommand command = new SQLiteCommand(sql, connection);
        command.Parameters.AddWithValue("@nome", nome);
        command.ExecuteNonQuery();
        connection.Close();
    }

    // inserimento di prodotto chiamando prima la categoria e poi il prodotto in modo da avere in inserimento il nome della categoria invece dell id
    public void InserisciProdottoCategoria()    // Menu opzione 13
    {
       // Chiama il metodo per visualizzare le categorie
        VisualizzaCategorie();
        //seleziona categoria
        Console.WriteLine("inserisci l'id della categoria");
        string id_categoria = Console.ReadLine()!;
        //inserisci prodotto
        Console.WriteLine("inserisci il nome del prodotto");
        string nome = Console.ReadLine()!;
        Console.WriteLine("inserisci il prezzo del prodotto");
        string prezzo = Console.ReadLine()!;
        Console.WriteLine("inserisci la quantità del prodotto");
        string quantita = Console.ReadLine()!;
        SQLiteConnection connectionins = new SQLiteConnection($"Data Source=database.db;Version=3;");
        connectionins.Open();
        string sql = "INSERT INTO prodotti (nome, prezzo, quantita, id_categoria) VALUES (@nome, @prezzo, @quantita, @id_categoria)";
        SQLiteCommand command = new SQLiteCommand(sql, connectionins);
        command.Parameters.AddWithValue("@nome", nome);
        command.Parameters.AddWithValue("@prezzo", Convert.ToDecimal(prezzo));
        command.Parameters.AddWithValue("@quantita", Convert.ToInt32(quantita));
        command.Parameters.AddWithValue("@id_categoria", Convert.ToInt32(id_categoria));
        command.ExecuteNonQuery();
        connectionins.Close();
    }

    public string VisualizzaProdottiAdvanced()  // Menu opzione 14
    {
        SQLiteConnection connection = new SQLiteConnection($"Data Source=database.db;Version=3;");
        connection.Open();

        // Modifica la query SQL per includere una join con la tabella categorie
        string sql = @"
            SELECT prodotti.id, prodotti.nome, prodotti.prezzo, prodotti.quantita, categorie.nome AS nome_categoria 
            FROM prodotti
            JOIN categorie ON prodotti.id_categoria = categorie.id";

        SQLiteCommand command = new SQLiteCommand(sql, connection);
        SQLiteDataReader reader = command.ExecuteReader();

        string stringa="";
        while (reader.Read())
        {
            stringa = stringa + $"id: {reader["id"]}, nome: {reader["nome"]}, prezzo: {reader["prezzo"]}, quantita: {reader["quantita"]}, categoria: {reader["nome_categoria"]}\n";
        }
        connection.Close();
        return stringa;
    }
}