using System;
using System.Collections.Generic;
using System.Data.Common;

public class CustomerController
{
    private Model _model;
    private CustomerView _customerView;

    public CustomerController(Model model, CustomerView clienteView)
    {
        _model = model;
        _customerView = clienteView;
    }

    public void InserisciCliente()    // Menu opzione 14
    {
        Cliente cliente = new Cliente();
        cliente.Nome = _customerView.InserisciCliente();
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

        _customerView.VisualizzaClienti(clienti);
    }

    public void ModificaCliente()    // Menu opzione 16
    {
        VisualizzaClienti();
        var (id, nuovoNome) = _customerView.ModificaCliente();
        Cliente cliente = new Cliente { Id = id };
        _model.ModificaCliente(cliente, nuovoNome);
    }

    public void EliminaCliente()   // Menu opzione 17
    {
        Cliente cliente = new Cliente();
        VisualizzaClienti();
        cliente.Id = _customerView.EliminaCliente();
        _model.EliminaCliente(cliente);
    }
}
