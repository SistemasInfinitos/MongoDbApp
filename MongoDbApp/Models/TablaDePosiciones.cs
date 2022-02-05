using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace MongoDbApp.Models
{
    /// <summary>
    /// entidad para listar los resultados
    /// </summary>
    public class TablaDePosiciones
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public string temporada { get; set; }
        public int equipo { get; set; }
        public DateTime fecha { get; set; }
        public bool gano { get; set; }
        public bool empate { get; set; }
        public bool perdio { get; set; }

        public int partidosJugado { get; set; }//PJ
        public int ganados { get; set; }//G
        public int perdidos { get; set; }//P
        public int golesAFavor { get; set; }//GF
        public int golesEnContra { get; set; }//GC
        public int dg { get; set; }//DG
        public int puntos { get; set; }//Pts
    }
}
