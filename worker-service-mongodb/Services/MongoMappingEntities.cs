using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using worker_service_mongodb.models;

namespace worker_service_mongodb.Services;
internal class MongoMappingEntities
{
    internal static void RegisterMap()
    {
        var conventionPack = new ConventionPack
            {
                new IgnoreExtraElementsConvention(true)
            };
        ConventionRegistry.Register("IgnoreExtraElements", conventionPack, type => true);

        if (!BsonClassMap.IsClassMapRegistered(typeof(Cliente)))
        {
            BsonClassMap.RegisterClassMap<Cliente>(map =>
            {
                map.AutoMap();
                map.MapMember(c => c.Id);
                map.MapMember(c => c.Nome);
                map.MapMember(c => c.SobreNome);
                map.MapMember(c => c.Enderecos).SetElementName("Endereco");
                map.SetDiscriminator("Cliente");
            });
        }

        if (!BsonClassMap.IsClassMapRegistered(typeof(Endereco)))
        {
            BsonClassMap.RegisterClassMap<Endereco>(map =>
            {
                map.AutoMap();
                map.MapMember(c => c.Id);
                map.MapMember(c => c.ClienteId);
                map.MapMember(c => c.Logradouro);
                map.MapMember(c => c.Cep);
                map.MapMember(c => c.Numero);
                map.MapMember(c => c.Cidade);
                map.MapMember(c => c.Bairro);
                map.MapMember(c => c.Uf);
                map.SetDiscriminator("Endereco");
            });
        }
    }
}
