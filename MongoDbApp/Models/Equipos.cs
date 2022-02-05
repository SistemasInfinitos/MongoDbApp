using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDbApp.Models
{
    public class Equipos
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public string nombreEquipo { get; set; }
        public DateTime fecha { get; set; }

        [BsonIgnore]
        [BsonRepresentation(BsonType.DateTime)]
        public virtual string fechaTex { get; set; }
    }
}
