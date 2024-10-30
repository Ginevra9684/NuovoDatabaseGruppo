using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

public class OrderController
{
    private Database _database;  // Riferimento al modello per l'accesso ai dati degli ordini
    private OrderView _orderView;  // Riferimento alla vista per visualizzare l'interfaccia degli ordini

     // Costruttore che inizializza il controller degli ordini con il modello e la vista
    public OrderController(Database database, OrderView orderView)
    {
        _database = database;
        _orderView = orderView;
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

    // Metodo per aggiungere un nuovo ordine (Menu opzione 1)
    private void AggiungiOrdine()
    {
        Ordine nuovoOrdine = _orderView.InserisciNuovoOrdine(); // Estrapola tutti i dati da inserire nel nuovoOrdine
        _database.Ordini.Add(nuovoOrdine);  // Aggiunge il nuovo ordine tramite entity
        _database.SaveChanges();    // Salva le modifiche tramite entity
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
}
