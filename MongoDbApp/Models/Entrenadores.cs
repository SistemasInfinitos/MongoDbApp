using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDbApp.Models
{
    public class Entrenadores
    {
        [BsonId]
        public ObjectId id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string idEquipo { get; set; }
        public string nombre { get; set; }
        public string documento { get; set; }
        public DateTime fecha { get; set; }


        [BsonIgnore]
        public virtual string idTex { get; set; }
        [BsonIgnore]
        public virtual string fechaTex { get; set; }
    }
}
