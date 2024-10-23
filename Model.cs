string path = @"database.db"; // il file deve essere nella stessa cartella del programma
        if (!File.Exists(path))
        {
            SQLiteConnection.CreateFile(path); // crea il file del database se non esiste
            SQLiteConnection connection = new SQLiteConnection($"Data Source={path};Version=3;"); // crea la connessione al database se non esiste utilizzando il file appena creato versiion identificata dal numero 3
            connection.Open(); // apre la connessione al database se non è già aperta
            string sql = @"
                            CREATE TABLE categorie (id INTEGER PRIMARY KEY AUTOINCREMENT, nome TEXT UNIQUE);
                            CREATE TABLE prodotti (id INTEGER PRIMARY KEY AUTOINCREMENT, nome TEXT UNIQUE, 
                            prezzo REAL, quantita INTEGER CHECK (quantita >= 0), id_categoria INTEGER, 
                            FOREIGN KEY (id_categoria) REFERENCES categorie(id));
                            INSERT INTO categorie (nome) VALUES ('c1');
                            INSERT INTO categorie (nome) VALUES ('c2');
                            INSERT INTO categorie (nome) VALUES ('c3');
                            INSERT INTO prodotti (nome, prezzo, quantita, id_categoria) VALUES ('p1', 1, 10, 1);
                            INSERT INTO prodotti (nome, prezzo, quantita, id_categoria) VALUES ('p2', 2, 20, 2);";

            SQLiteCommand command = new SQLiteCommand(sql, connection); // crea il comando sql da eseguire sulla connessione al database se non esiste
            command.ExecuteNonQuery(); // esegue il comando sql sulla connessione al database se non esiste
            connection.Close(); // chiude la connessione al database se non è già chiusa
        }