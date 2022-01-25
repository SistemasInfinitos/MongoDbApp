﻿using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDbApp.Models.ModelDbConexion
{
    public class MongoDBRepository
    {
        public MongoClient client;
        public IMongoDatabase db;

        public MongoDBRepository() 
        {
            //client = new MongoClient("mongodb://127.0.0.1:27017");
            //client = new MongoClient("mongodb://localhost:27001,localhost:27002,localhost:27003/?safe=true&connect=replicaset");
            //client = new MongoClient("mongodb://127.0.0.1:27001,127.0.0.1:27002,127.0.0.1:27003/?safe=true&connect=replicaset");
            //client = new MongoClient("mongodb://127.0.0.1:27001,127.0.0.1:27002,127.0.0.1:27003/?replicaSet=replicaIBERO");

            var settings = new MongoClientSettings
            {
                Servers = new[]
                {
                    new MongoServerAddress("localhost", 27001),
                    new MongoServerAddress("localhost", 27002),
                    new MongoServerAddress("localhost", 27003)
                },
                ConnectionMode = ConnectionMode.Automatic,
                ReplicaSetName = "replicaIBERO",
                WriteConcern = new WriteConcern(WriteConcern.WValue.Parse("3"), wTimeout: TimeSpan.Parse("10"))
            };
             client = new MongoClient(settings);

            db = client.GetDatabase("TorneoDeportivoIBERO");
        }
    }
}
