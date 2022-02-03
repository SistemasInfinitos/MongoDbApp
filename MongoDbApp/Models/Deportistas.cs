using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDbApp.Models
{
    public class Deportistas
    {
        [BsonId]
        public ObjectId id { get; set; }

        [BsonIgnore]
        public string idTex { get; set; }

        /// <summary>
        /// Nombre juados
        /// </summary>
        public string nombre { get; set; }

        /// <summary>
        /// numero camiseta
        /// </summary>
        public int numero { get; set; }

        /// <summary>
        /// posicion en el area de juego ejemplo: DC defensa central
        /// </summary>
        public string posicion { get; set; }

        /// <summary>
        /// identificacion del jugador
        /// </summary>
        public string documento { get; set; }

        /// <summary>
        /// fecha registro
        /// </summary>
        public DateTime fecha { get; set; }

        [BsonIgnore]
        public string fechaTex { get; set; }

        /// <summary>
        /// id automatico de la tabla de equipos
        /// </summary>
        public string idEquipo { get; set; }
    }
}
