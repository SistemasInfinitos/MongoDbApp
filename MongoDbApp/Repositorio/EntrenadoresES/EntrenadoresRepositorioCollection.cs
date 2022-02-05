using MongoDB.Bson;
using MongoDB.Driver;
using MongoDbApp.Models;
using MongoDbApp.Models.ModelDbConexion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDbApp.Repositorio.EntrenadoresES
{
    public class EntrenadoresRepositorioCollection : IEntrenadoresContratoCollection
    {
        internal MongoDBRepository _repository = new MongoDBRepository();
        private IMongoCollection<Entrenadores> collectin;
        public EntrenadoresRepositorioCollection()
        {
            // si no encuentra la collection crea una nueva
            collectin = _repository.db.GetCollection<Entrenadores>("Entrenadores");
        }
        public async Task DeleteEntrenador(string id)
        {
            var filtro = Builders<Entrenadores>.Filter.Eq(x => x.id, id);
            await collectin.DeleteOneAsync(filtro);
        }

        public async Task<Entrenadores> GetEntrenadorById(string id)
        {
            Entrenadores entrenadores = new Entrenadores();
            try
            {
                entrenadores = await collectin.FindAsync(new BsonDocument { { "_id", new ObjectId(id) } }).Result.FirstAsync();
            }
            catch (Exception e)
            {
                return entrenadores;
            }
            return entrenadores;
        }

        public async Task<List<Entrenadores>> GetListEntrenadores()
        {
            return await collectin.FindAsync(new BsonDocument()).Result.ToListAsync();
        }

        public async Task InsertEntrenador(Entrenadores entidad)
        {
            entidad.fecha = DateTime.Now;
            await collectin.InsertOneAsync(entidad);
        }

        public async Task UpdateEntrenador(Entrenadores entidad)
        {
            var filtro = Builders<Entrenadores>.Filter.Eq(x => x.id, entidad.id);
            entidad.fecha = DateTime.Now;
            await collectin.ReplaceOneAsync(filtro, entidad);
        }
    }
}
