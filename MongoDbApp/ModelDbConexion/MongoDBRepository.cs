using MongoDB.Driver;
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
                    new MongoServerAddress("localhost", 27006),
                    new MongoServerAddress("localhost", 27007),
                    new MongoServerAddress("localhost", 27008),
                },
                //ConnectionMode = ConnectionMode.Automatic,
                DirectConnection=false,
                //ReplicaSetName = "replicaTorneoIB",
                ReplicaSetName = "replicaTorneoP1",
                //ReplicaSetName = "replicaTorneoP2",
                WriteConcern = new WriteConcern(WriteConcern.WValue.Parse("3"), wTimeout: TimeSpan.Parse("10"))
            };
             client = new MongoClient(settings);

            db = client.GetDatabase("TorneoDeportivoIBERO");
        }
    }
}
