using System;
using System.Collections.Generic;

public class OrderController
{
    private Model _model;  // Riferimento al modello per l'accesso ai dati degli ordini
    private OrderView _orderView;  // Riferimento alla vista per visualizzare l'interfaccia degli ordini

    // Costruttore del controller degli ordini
    public OrderController(Model model, OrderView orderView)
    {
        _model = model;
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

    // Metodo per aggiungere un nuovo ordine
    private void AggiungiOrdine()
    {
        // Usa il metodo InserisciNuovoOrdine dalla vista per ottenere i dati dell'ordine
        Ordine nuovoOrdine = _orderView.InserisciNuovoOrdine();

        // Chiama il modello per inserire il nuovo ordine
        _model.InserisciOrdine(nuovoOrdine.cliente.Id, nuovoOrdine.prodotto.Id, int.Parse(nuovoOrdine.Quantita));
        
        _orderView.Stampa("Ordine aggiunto con successo.");
    }

    // Metodo per visualizzare tutti gli ordini
    private void VisualizzaOrdini()
    {
        using var reader = _model.VisualizzaOrdini();
        var ordini = new List<Ordine>();

        // Popola la lista degli ordini leggendo dal reader
        while (reader.Read())
        {
            var ordine = new Ordine
            {
                Id = Convert.ToInt32(reader["id"]),
                DataAcquisto = Convert.ToDateTime(reader["dataAcquisto"]),
                Quantita = reader["quantita"].ToString(),
                cliente = new Cliente { Nome = reader["Cliente"].ToString() },
                prodotto = new Prodotto { Nome = reader["Prodotto"].ToString() }
            };
            ordini.Add(ordine);
        }

        // Mostra tutti gli ordini utilizzando la vista
        _orderView.VisualizzaOrdini(ordini);
    }
}
