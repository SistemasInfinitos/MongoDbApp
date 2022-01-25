using MongoDB.Bson;
using MongoDB.Driver;
using MongoDbApp.Models;
using MongoDbApp.Models.ModelDbConexion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDbApp.Repositorio.EquiposES
{
    public class EquiposRepositorioCollection : IEquiposContratoCollection
    {
        internal MongoDBRepository _repository = new MongoDBRepository();
        private IMongoCollection<Equipos> collectin;
        public EquiposRepositorioCollection()
        {
            // si no encuentra la collection crea una nueva
            collectin = _repository.db.GetCollection<Equipos>("Equipos");
        }
        public async Task DeleteEquipo(string id)
        {
            var filtro = Builders<Equipos>.Filter.Eq(x => x.id, new MongoDB.Bson.ObjectId(id));
            await collectin.DeleteOneAsync(filtro);
        }

        public async Task<Equipos> GetEquiposById(string id)
        {
            Equipos equipos = new Equipos();
            try
            {
                equipos = await collectin.FindAsync(new BsonDocument { { "_id", new ObjectId(id) } }).Result.FirstAsync();
            }
            catch (Exception e)
            {
                return equipos;
            }
            return equipos;
        }

        public async Task<List<Equipos>> GetListEquipos()
        {
            return await collectin.FindAsync(new BsonDocument()).Result.ToListAsync();
        }

        public async Task InsertEquipo(Equipos entidad)
        {
            entidad.fecha = DateTime.Now;
            try
            {
                await collectin.InsertOneAsync(entidad);
            }
            catch (Exception X)
            {
                throw;
            }
        }

        public async Task UpdateEquipo(Equipos entidad)
        {
            var filtro = Builders<Equipos>.Filter.Eq(x => x.id, entidad.id);
            entidad.fecha = DateTime.Now;
            await collectin.ReplaceOneAsync(filtro, entidad);
        }
    }
}
