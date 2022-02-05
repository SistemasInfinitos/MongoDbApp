using MongoDB.Bson;
using MongoDB.Driver;
using MongoDbApp.Models;
using MongoDbApp.Models.ModelDbConexion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDbApp.Repositorio.ArbitrosES
{
    public class ArbitrosRepositorioCollection : IArbitrosContratoCollection
    {
        internal MongoDBRepository _repository = new MongoDBRepository();
        private IMongoCollection<Arbitros> collectin;
        public ArbitrosRepositorioCollection()
        {
            // si no encuentra la collection crea una nueva
            collectin = _repository.db.GetCollection<Arbitros>("Arbitros");
        }
        public async Task DeleteArbitro(string id)
        {
            //var filtro = Builders<Arbitros>.Filter.Eq(x => x.id, new MongoDB.Bson.ObjectId(id));
            var filtro = Builders<Arbitros>.Filter.Eq(x => x.id, id);
            await collectin.DeleteOneAsync(filtro);
        }

        public async Task<Arbitros> GetArbitroById(string id)
        {
            Arbitros arbitros = new Arbitros();
            try
            {
                arbitros = await collectin.FindAsync(new BsonDocument { { "_id", new ObjectId(id) } }).Result.FirstAsync();
            }
            catch (Exception e)
            {
                return arbitros;
            }
            return arbitros;
        }

        public async Task<List<Arbitros>> GetListArbitros()
        {
            return await collectin.FindAsync(new BsonDocument()).Result.ToListAsync();
        }

        public async Task InsertArbitro(Arbitros entidad)
        {
            entidad.fecha = DateTime.Now;
            await collectin.InsertOneAsync(entidad);
        }

        public async Task UpdateArbitro(Arbitros entidad)
        {
            var filtro = Builders<Arbitros>.Filter.Eq(x => x.id, entidad.id);
            entidad.fecha = DateTime.Now;
            await collectin.ReplaceOneAsync(filtro, entidad);
        }
    }
}
