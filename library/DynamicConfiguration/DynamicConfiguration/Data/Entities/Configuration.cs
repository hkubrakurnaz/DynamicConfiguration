using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DynamicConfiguration.Data.Entities
{
    [BsonIgnoreExtraElements]
    public class Configuration
    {
        [BsonId, BsonElement("_id") ,BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        
        [BsonElement("name"), BsonRepresentation(BsonType.String)]
        public string Name { get; set; }
        
        [BsonElement("type"), BsonRepresentation(BsonType.String)]
        public string Type { get; set; }
        
        [BsonElement("value"), BsonRepresentation(BsonType.String)]
        public string Value { get; set; }
        
        [BsonElement("isActive"), BsonRepresentation(BsonType.Boolean)]
        public bool IsActive { get; set; }
        
        [BsonElement("applicationName"), BsonRepresentation(BsonType.String)]
        public string ApplicationName { get; set; }
    }
}