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
                    _orderView.Stampa("Opzione non valida.");
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
        nuovoOrdine.cliente = _database.Clienti.Find(nuovoOrdine.cliente.Id);

        // Recupera il prodotto esistente dal database utilizzando l'ID specificato nell'ordine
        // Anche qui si garantisce che l'entità prodotto sia tracciata dal contesto di Entity Framework
        nuovoOrdine.prodotto = _database.Prodotti.Find(nuovoOrdine.prodotto.Id);

        // Verifica che il cliente e il prodotto siano validi non null prima di aggiungere l'ordine
        if (nuovoOrdine.cliente != null && nuovoOrdine.prodotto != null)
        {
            // Aggiunge il nuovo ordine 
            _database.Ordini.Add(nuovoOrdine);

            // Salva le modifiche nel database, inclusa la creazione del nuovo ordine
            _database.SaveChanges();

            // Conferma l'aggiunta dell'ordine tramite la vista
            _orderView.Stampa("Ordine aggiunto con successo.");
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
    // Metodo per visualizzare tutti gli ordini (Menu opzione 2)
    private void VisualizzaOrdini()
    {
        var ordini = _database.Ordini.ToList(); // Estrapola con entity una lista di ordini
        _orderView.VisualizzaOrdini(ordini);    // Passa la lista alla view
    }

    // Metodo per modificare un ordine esistente (Menu opzione 3)
    private void ModificaOrdine()
    {
        VisualizzaOrdini();
        _productController.VisualizzaProdotti();
        Ordine ordineDaModificare = _orderView.ModificaOrdine();

        var ordine = _database.Ordini.FirstOrDefault(o => o.Id == ordineDaModificare.Id);   // Cerca un ordine tramite Id cliente
        var prodotto = _database.Prodotti.FirstOrDefault(p => p.Id == ordineDaModificare.prodotto.Id);  // Cerca il nuovo prodotto tramite id nei prodotti
        if (ordine != null && prodotto != null && ordineDaModificare.Quantita != null)
        {
            ordine.prodotto = prodotto; // Aggiorna il prodotto nell'ordine
            ordine.Quantita = ordineDaModificare.Quantita;  // Aggiorna la quantità nell'ordine
            _database.SaveChanges();    // Salva le modifiche
            _orderView.Stampa("Ordine modificato con successo.");
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
