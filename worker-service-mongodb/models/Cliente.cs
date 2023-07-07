using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace worker_service_mongodb.models;

public class Cliente
{
    [BsonId]
    public Guid Id { get; set; }
    public string Nome { get; set; } = null!;
    public string SobreNome { get; set; } = null!;
    public IEnumerable<Endereco>? Enderecos { get; set; }
}

public class Endereco
{
    public Guid Id { get; set; }
    public Guid ClienteId { get; set; }
    public string? Cep { get; set; }
    public string? Logradouro { get; set; }
    public string? Numero { get; set; }
    public string? Bairro { get; set; }
    public string? Cidade { get; set; }
    public string? Uf { get; set; }
}

public class MongoDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
}