using MongoDB.Bson;
using MongoDB.Driver;
using MongoDbApp.Models;
using MongoDbApp.Models.ModelDbConexion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDbApp.Repositorio.TemporadasES
{
    public class TemporadasRepositorioCollection : ITemporadasContratoCollection
    {
        internal MongoDBRepository _repository = new MongoDBRepository();
        private IMongoCollection<Temporadas> collectin;
        public TemporadasRepositorioCollection()
        {
            // si no encuentra la collection crea una nueva
            collectin = _repository.db.GetCollection<Temporadas>("Temporadas");
        }
        public async Task DeleteTemporada(string id)
        {
            var filtro = Builders<Temporadas>.Filter.Eq(x => x.id, id);
            await collectin.DeleteOneAsync(filtro);
        }

        public async Task<List<Temporadas>> GetListTemporadas()
        {
            return await collectin.FindAsync(new BsonDocument()).Result.ToListAsync();
        }

        public async Task<Temporadas> GetTemporadaById(string id)
        {
            Temporadas temporadas = new Temporadas();
            try
            {
                temporadas = await collectin.FindAsync(new BsonDocument { { "_id", new ObjectId(id) } }).Result.FirstAsync();
            }
            catch (Exception e)
            {
                return temporadas;
            }
            return temporadas;
        }

        public async Task InsertTemporada(Temporadas entidad)
        {
            entidad.fecha = DateTime.Now;
            await collectin.InsertOneAsync(entidad);
        }

        public async Task UpdateTemporada(Temporadas entidad)
        {
            var filtro = Builders<Temporadas>.Filter.Eq(x => x.id, entidad.id);
            entidad.fecha = DateTime.Now;
            await collectin.ReplaceOneAsync(filtro, entidad);
        }
    }
}
