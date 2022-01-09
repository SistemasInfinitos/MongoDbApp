﻿using MongoDB.Bson;
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
        /// fecha registro
        /// </summary>
        public DateTime fecha { get; set; }
        public virtual string fechaTex { get; set; }
    }
}
