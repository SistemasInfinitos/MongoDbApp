using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDbApp.Models
{
    public class Temporadas
    {
        [BsonId]
        public ObjectId id { get; set; }

        [BsonIgnore]
        public virtual string idTex { get; set; }
        public string temporada { get; set; }
        public DateTime fecha { get; set; }
        public DateTime fechaInicio { get; set; }
        public string fechaInicioTex { get; set; }
        public DateTime fechaFin { get; set; }
        public string fechaFinTex { get; set; }

        [BsonIgnore]
        public virtual string fechaTex { get; set; }

    }
}
