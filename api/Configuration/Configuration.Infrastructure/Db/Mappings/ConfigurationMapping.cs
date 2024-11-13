using Configuration.Domain.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;

namespace Configuration.Infrastructure.Db.Mappings
{
    public class ConfigurationMapping
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
                    .SetSerializer(new MongoDB.Bson.Serialization.Serializers.StringSerializer(BsonType.ObjectId));
            
                cm.MapMember(c => c.Name).SetElementName("name").SetSerializer(new MongoDB.Bson.Serialization.Serializers.StringSerializer(BsonType.String));
                cm.MapMember(c => c.Type).SetElementName("type").SetSerializer(new MongoDB.Bson.Serialization.Serializers.StringSerializer(BsonType.String));
                cm.MapMember(c => c.Value).SetElementName("value").SetSerializer(new MongoDB.Bson.Serialization.Serializers.StringSerializer(BsonType.String));
                cm.MapMember(c => c.IsActive).SetElementName("isActive").SetSerializer(new MongoDB.Bson.Serialization.Serializers.BooleanSerializer(BsonType.Boolean));
                cm.MapMember(c => c.ApplicationName).SetElementName("applicationName").SetSerializer(new MongoDB.Bson.Serialization.Serializers.StringSerializer(BsonType.String));
            });
        }
    }
}