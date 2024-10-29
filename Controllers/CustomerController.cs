public class CustomerController
{
    private Model _model;
    private CustomerView _customerView;

    public CustomerController(Model model, CustomerView customerView)
    {
        _model = model;
        _customerView = customerView;
    }

    public void CustomerMenu()
    {
        while(true)
        {
            _customerView.ShowCustomerMenu();
            var input = _customerView.GetInput();
            switch (input)
            {
                case "1":
                    InserisciCliente();
                    break;
                case "2":
                    VisualizzaClienti();
                    break;
                case "3":
                    ModificaCliente();
                    break;
                case "4":
                    EliminaCliente();
                    break;
                case "5":
                    return;
                default:
                    _customerView.Stampa("opzione non valida");
                    break;
            }
        }
    }

    public void InserisciCliente()    // Menu opzione 1
    {
        Cliente cliente = new Cliente();
        cliente.Nome = _customerView.InserisciCliente();
        _model.InserisciCliente(cliente);
    }

    public void VisualizzaClienti() // Menu opzione 2
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

    public void ModificaCliente()    // Menu opzione 3
    {
        VisualizzaClienti();
        var (id, nuovoNome) = _customerView.ModificaCliente();
        Cliente cliente = new Cliente { Id = id };
        _model.ModificaCliente(cliente, nuovoNome);
    }

    public void EliminaCliente()   // Menu opzione 4
    {
        Cliente cliente = new Cliente();
        VisualizzaClienti();
        cliente.Id = _customerView.EliminaCliente();
        _model.EliminaCliente(cliente);
    }
}
