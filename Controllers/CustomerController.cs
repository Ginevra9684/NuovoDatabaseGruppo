using System;
using System.Collections.Generic;
using System.Data.Common;

public class CustomerController
{
    private Model _model;
    private ClienteView _clienteView;

    public CustomerController(Model model, ClienteView clienteView)
    {
        _model = model;
        _clienteView = clienteView;
    }

    public void InserisciCliente()    // Menu opzione 14
    {
        Cliente cliente = new Cliente();
        cliente.Nome = _clienteView.InserisciCliente();
        _model.InserisciCliente(cliente);
    }

    public void VisualizzaClienti() // Menu opzione 15
    {
        var clienti = new List<Cliente>();

        using (var reader = _model.VisualizzaClienti())
        {
            while (reader.Read())
            {
                var cliente = new Cliente
                {
                    Id = Convert.ToInt32(reader["id"]),
                    Nome = reader["nome"].ToString(),
                };
                clienti.Add(cliente);
            }
        }

        _clienteView.VisualizzaClienti(clienti);
    }

    public void ModificaCliente()    // Menu opzione 16
    {
        VisualizzaClienti();
        var (id, nuovoNome) = _clienteView.ModificaCliente();
        Cliente cliente = new Cliente { Id = id };
        _model.ModificaCliente(cliente, nuovoNome);
    }

    public void EliminaCliente()   // Menu opzione 17
    {
        Cliente cliente = new Cliente();
        VisualizzaClienti();
        cliente.Id = _clienteView.EliminaCliente();
        _model.EliminaCliente(cliente);
    }
}
