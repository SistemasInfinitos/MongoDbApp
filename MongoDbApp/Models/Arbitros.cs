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
        public ObjectId id { get; set; }

        [BsonIgnore]
        public virtual string idTex { get; set; }
        public string nombre { get; set; }
        public int numero { get; set; }
        public string documento { get; set; }

        /// <summary>
        /// fecha registro
        /// </summary>
        public DateTime fecha { get; set; }

        [BsonIgnore]
        public virtual string  fechaTex { get; set; }
    }
}
