using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace MongoDbApp.Models
{
    public class EncuentrosDeportivos: ReadOnlyEncuentrosDeportivos
    {
        [BsonId]
        public ObjectId id { get; set; }
        public string encuentro { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string idTemporada { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string idEquipoA { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string idEquipoB { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string idArbitro { get; set; }
        public DateTime fecha { get; set; }
        public List<Resultados> listResultados { get; set; }
        public List<ResultadosEquipo> listResultadosEquipo { get; set; }

    }
    public class Encuentros 
    {
        [BsonId]
        public ObjectId id { get; set; }
        public string encuentro { get; set; }
        public string nombreEquipo { get; set; }
        public DateTime fecha { get; set; }
        public List<Resultados> listResultados { get; set; }
        public List<ResultadosEquipo> listResultadosEquipo { get; set; }
    }
    public class ReadOnlyEncuentrosDeportivos
    {
        [BsonIgnore]
        public string idTex { get; set; }

        [BsonIgnore]
        public string fechaTex { get; set; }

        [BsonIgnore]
        public string temporada { get; set; }

        [BsonIgnore]
        public string equipoA { get; set; }

        [BsonIgnore]
        public string equipoB { get; set; }

        [BsonIgnore]
        public string arbitro { get; set; }

        [BsonIgnore]
        public Arbitros asArbitro { get; set; }


        [BsonIgnore]
        public Temporadas asTemporadas { get; set; }

        [BsonIgnore]
        public Equipos asEquiposA { get; set; }

        [BsonIgnore]
        public Equipos asEquiposB { get; set; }

        [BsonIgnore]
        public Deportistas asDeportistas { get; set; }
    }
}
