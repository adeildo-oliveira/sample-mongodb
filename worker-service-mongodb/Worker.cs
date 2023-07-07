using worker_service_mongodb.models;
using worker_service_mongodb.Services;

namespace worker_service_mongodb;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IClientService _clientService;

    public Worker(ILogger<Worker> logger, IClientService clientService)
    {
        _logger = logger;
        _clientService = clientService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var clientes = new List<Cliente>(100);
        while (!stoppingToken.IsCancellationRequested)
        {
            var idCliente = Guid.NewGuid();

            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

            if (clientes.Count == 99)
            {
                clientes.Add(
                    new()
                    {
                        Id = Guid.Parse("f7af5973-fceb-4190-b934-13e3863b4096"),
                        Nome = "Nome Cliente",
                        SobreNome = "Sobre Nome Cliente",
                        Enderecos = new List<Endereco>
                        {
                            new()
                            {
                                Id = Guid.Parse("154a911c-7a82-477d-adee-56c61092d871"),
                                ClienteId = Guid.Parse("f7af5973-fceb-4190-b934-13e3863b4096"),
                                Cep = "04828880",
                                Bairro = "Jardins",
                                Cidade = "São Paulo",
                                Logradouro = "Av. Paulista",
                                Numero = "1454",
                                Uf = "SP"
                            }
                        }
                    });
            }
            else
            {
                clientes.Add(
                    new()
                    {
                        Id = Guid.Parse("f7af5973-fceb-4190-b934-13e3863b4096"),
                        Nome = "Nome Cliente 2",
                        SobreNome = "Sobre Nome Cliente 2",
                        Enderecos = new List<Endereco>
                        {
                            new()
                            {
                                Id = Guid.Parse("154a911c-7a82-477d-adee-56c61092d871"),
                                ClienteId = Guid.Parse("f7af5973-fceb-4190-b934-13e3863b4096"),
                                Cep = "04828880",
                                Bairro = "Jardins",
                                Cidade = "São Paulo",
                                Logradouro = "Av. Paulista",
                                Numero = "1454",
                                Uf = "SP"
                            }
                        }
                    });
            }

            if (clientes.Count == 100)
            {
                await _clientService.UpdateInsert(clientes);
                clientes.Clear();
            }
        }
    }
}