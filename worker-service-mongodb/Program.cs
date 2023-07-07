using worker_service_mongodb;
using worker_service_mongodb.Services;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddSingleton<IClientService, ClientService>();
        services.AddHostedService<Worker>();
    })
    .Build();

host.Run();
