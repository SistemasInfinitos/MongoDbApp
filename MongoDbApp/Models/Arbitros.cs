using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDbApp.Models
{
    public class Arbitros
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public string nombre { get; set; }
        public int numero { get; set; }
        public string documento { get; set; }     
        public DateTime fecha { get; set; }


        [BsonIgnore]
        [BsonRepresentation(BsonType.DateTime)]
        public virtual string  fechaTex { get; set; }
    }
}
