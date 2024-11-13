using Configuration.Domain.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;

namespace Configuration.Infrastructure.Db.Mappings
{
    public static class ConfigurationMapping
    {
        public static void RegisterMapping()
        {
            BsonClassMap.RegisterClassMap<DynamicConfiguration>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
                cm.MapIdMember(c => c.Id)
                    .SetElementName("_id")
                    .SetIdGenerator(StringObjectIdGenerator.Instance)
                    .SetSerializer(new StringSerializer(BsonType.ObjectId));
            
                cm.MapMember(c => c.Name).SetElementName("name").SetSerializer(new StringSerializer(BsonType.String));
                cm.MapMember(c => c.Type).SetElementName("type").SetSerializer(new StringSerializer(BsonType.String));
                cm.MapMember(c => c.Value).SetElementName("value").SetSerializer(new StringSerializer(BsonType.String));
                cm.MapMember(c => c.IsActive).SetElementName("isActive").SetSerializer(new BooleanSerializer(BsonType.Boolean));
                cm.MapMember(c => c.ApplicationName).SetElementName("applicationName").SetSerializer(new StringSerializer(BsonType.String));
            });
        }
    }
}