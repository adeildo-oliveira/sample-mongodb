using MongoDB.Driver;
using worker_service_mongodb.models;

namespace worker_service_mongodb.Services;

public interface IClientService
{
    Task<List<Cliente>> GetAsync();
    Task<Cliente?> GetAsync(Guid id);
    Task CreateAsync(Cliente newBook);
    Task UpdateAsync(Guid id, Cliente updatedBook);
    Task RemoveAsync(Guid id);
    Task UpdateAllAsync(Guid id, Cliente updatedBook);
    Task UpdateInsert(IEnumerable<Cliente> clientes);
}

public class ClientService : IClientService
{
    private readonly IMongoCollection<Cliente> _clienteCollection;
    private readonly IMongoClient _mongoClient;

    public ClientService(IConfiguration configuration)
    {
        _mongoClient = new MongoClient(configuration.GetValue<string>("MongoSettings:ConnectionString"));
        var mongoDatabase = _mongoClient.GetDatabase(configuration.GetValue<string>("MongoSettings:DatabaseName"));
        _clienteCollection = mongoDatabase.GetCollection<Cliente>("Cliente");
        MongoMappingEntities.RegisterMap();
    }

    public async Task<List<Cliente>> GetAsync() =>
        await _clienteCollection.Find(_ => true).ToListAsync();

    public async Task<Cliente?> GetAsync(Guid id) =>
        await _clienteCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Cliente newBook) =>
        await _clienteCollection.InsertOneAsync(newBook);

    public async Task UpdateAsync(Guid id, Cliente updatedBook) =>
        await _clienteCollection.ReplaceOneAsync(x => x.Id == id, updatedBook);

    public async Task RemoveAsync(Guid id) =>
        await _clienteCollection.DeleteOneAsync(x => x.Id == id);

    public async Task UpdateAllAsync(Guid id, Cliente updatedBook)
    {
        var options = new UpdateOptions { IsUpsert = true };
        var filtro = Builders<Cliente>.Filter.Eq(p => p.Id, id);
        var update = Builders<Cliente>.Update.Set(p => p, updatedBook);
        await _clienteCollection.UpdateOneAsync(filtro, update, options);
    }

    public async Task UpdateInsert(IEnumerable<Cliente> clientes)
    {
        var listWrites2 = new List<WriteModel<Cliente>>();

        foreach (var cliente in clientes)
        {
            var filterDefinition = Builders<Cliente>.Filter.Eq(p => p.Id, cliente.Id);
            var updateDefinition = Builders<Cliente>.Update.Set(p => p, cliente);

            var update = new UpdateOneModel<Cliente>(filterDefinition, updateDefinition)
            { 
                IsUpsert = true
            };

            listWrites2.Add(update);
        }

        await _clienteCollection.BulkWriteAsync(listWrites2, new BulkWriteOptions { IsOrdered = true });
    }
}
