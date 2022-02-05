using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace MongoDbApp.Models
{
    public class Resultados: ReadOnlyResultados
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string idEquipo { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string idDeportista { get; set; }
        public int goles { get; set; }
    }

    public class ReadOnlyResultados 
    {
        [BsonIgnore]
        public virtual string equipo { get; set; }

        [BsonIgnore]
        public virtual string deportista { get; set; }

        [BsonIgnore]
        public virtual bool gano { get; set; }
    }
}
