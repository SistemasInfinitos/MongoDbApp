using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using MongoDbApp.Models;
using MongoDbApp.Models.ModelDbConexion;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Threading.Tasks;
using System.Collections;
using System.Linq;

namespace MongoDbApp.Repositorio.EventosDeportivosES
{
    public class EventosDeportivosRepositorioCollection : IEventosDeportivosContradoCollection
    {
        CultureInfo culture = new CultureInfo("en-US", true);
        internal MongoDBRepository _repository = new MongoDBRepository();
        private IMongoCollection<EncuentrosDeportivos> collectinEncuentrosDeportivos;
        private IMongoCollection<Resultados> collectinResultados;
        private IMongoCollection<Temporadas> collectinTemporadas;
        private IMongoCollection<Equipos> collectinEquipos;
        private IMongoCollection<Arbitros> collectinArbitros;
        private IMongoCollection<Deportistas> collectinDeportistas;
        public EventosDeportivosRepositorioCollection()
        {
            // si no encuentra la collection crea una nueva
            collectinEncuentrosDeportivos = _repository.db.GetCollection<EncuentrosDeportivos>("EncuentrosDeportivos");
            collectinResultados = _repository.db.GetCollection<Resultados>("Resultados");
            collectinTemporadas = _repository.db.GetCollection<Temporadas>("Temporadas");
            collectinEquipos = _repository.db.GetCollection<Equipos>("Equipos");
            collectinArbitros = _repository.db.GetCollection<Arbitros>("Arbitros");
            collectinDeportistas = _repository.db.GetCollection<Deportistas>("Deportistas");
        }

        #region EncuentrosDeportivos
        public async Task<EncuentrosDeportivos> GetEncuentrosDeportivoById(string id)
        {
            EncuentrosDeportivos encuentros = new EncuentrosDeportivos();
            try
            {
                encuentros = await collectinEncuentrosDeportivos.FindAsync(new BsonDocument { { "_id", new ObjectId(id) } }).Result.FirstAsync();
            }
            catch (Exception e)
            {
                return encuentros;
            }
            return encuentros;
        }

        public async Task<List<EncuentrosDeportivos>> GetListEncuentrosDeportivos()
        {
            List<EncuentrosDeportivos> listEncuentros = new List<EncuentrosDeportivos>();

            listEncuentros = await collectinEncuentrosDeportivos.FindAsync(new BsonDocument()).Result.ToListAsync();
            foreach (var item in listEncuentros)
            {
                item.fechaTex = item.fecha.ToString("yyyy-MM-dd", culture);

                item.asTemporadas = await collectinTemporadas.FindAsync(x => x.id == item.idTemporada).Result.FirstAsync();
                item.asEquiposA = await collectinEquipos.FindAsync(x => x.id == item.idEquipoA).Result.FirstAsync();
                item.asEquiposB = await collectinEquipos.FindAsync(x => x.id == item.idEquipoB).Result.FirstAsync();
                item.asArbitro = await collectinArbitros.FindAsync(x => x.id == item.idArbitro).Result.FirstAsync();

                if (item.listResultados.Count() > 0)
                {
                    foreach (var resultado in item.listResultados)
                    {
                        var equipo = await collectinEquipos.FindAsync(x => x.id == resultado.idEquipo).Result.FirstAsync();
                        resultado.equipo = equipo.nombreEquipo;

                        try
                        {
                            if (resultado.idDeportista!=null)
                            {
                                //var jugador = await collectinDeportistas.WHE(new BsonDocument { { "_id", new ObjectId(resultado.idDeportista) } }).Result.FirstAsync();

                                var jugador = await collectinDeportistas.FindAsync(x => x.id == resultado.idDeportista).Result.FirstAsync();
                                resultado.deportista = jugador.nombre;
                            }

                        }
                        catch (Exception X)
                        {

                            throw;
                        }

                        var counA = +item.listResultados.Where(x => x.idEquipo == item.asEquiposA.id.ToString()).Select(x => x.goles).ToList().Sum();
                        var counB = +item.listResultados.Where(x => x.idEquipo == item.asEquiposB.id.ToString()).Select(x => x.goles).ToList().Sum();
                    }
                }
            }


            #region

            var query = (from eco in collectinEncuentrosDeportivos.AsQueryable()
                        join tem in collectinTemporadas.AsQueryable() on eco.idTemporada equals tem.id
                        join equipoA in collectinEquipos.AsQueryable() on eco.idEquipoA equals equipoA.id
                        join equipoB in collectinEquipos.AsQueryable() on eco.idEquipoB equals equipoB.id
                        join arbitro in collectinArbitros.AsQueryable() on eco.idArbitro equals arbitro.id
                        select new { eco.id,tem.temporada, equipoa=equipoA.nombreEquipo, equipob=equipoB.nombreEquipo ,arbitros=arbitro.nombre }).ToList();


            var docs2 = collectinEncuentrosDeportivos.Aggregate().Lookup("Equipos", "idEquipoA", "_id", "asTemporadas").ToList();

            var docs3 = collectinEncuentrosDeportivos.Aggregate().Lookup("Temporadas", "idTemporada", "_id", "asTemporadas").As<BsonDocument>().ToList();
            var res = collectinEncuentrosDeportivos.Aggregate().Lookup("Temporadas", "idTemporada", "_id", "asTemporadas").Project(Builders<BsonDocument>.Projection.Exclude("_id")).ToList();
            //var dd = collectinEncuentrosDeportivos.Aggregate().Lookup("Nombre de la colección extranjera", "Nombre del campo local", "Nombre del campo extranjero", "resultado");


            return listEncuentros;
            #endregion
        }

        public async Task<EncuentrosDeportivos> InsertEncuentrosDeportivo(EncuentrosDeportivos entidad)
        {
            entidad.fecha = DateTime.Now;
            await collectinEncuentrosDeportivos.InsertOneAsync(entidad);

            entidad.fechaTex = entidad.fecha.ToString("yyyy-MM-dd", culture);
            return entidad;
        }

        public async Task<EncuentrosDeportivos> UpdateEncuentrosDeportivo(EncuentrosDeportivos entidad)
        {
            var builder = Builders<EncuentrosDeportivos>.Update.Set(x => x.id, entidad.id);

            foreach (PropertyInfo prop in entidad.GetType().GetProperties())
            {
                var value = entidad.GetType().GetProperty(prop.Name).GetValue(entidad, null);

                if (prop.Name != "id"&& prop.Name != "fecha")
                {
                    if (value != null)
                    {
                        builder = builder.Set(prop.Name, value);
                    }
                    else
                    {
                        builder = builder.Unset(prop.Name);
                    }
                }
            }

            var filter = Builders<EncuentrosDeportivos>.Filter;
            var filter_def = filter.Eq(x => x.id, entidad.id);

            var result=await collectinEncuentrosDeportivos.UpdateOneAsync(filter_def, builder);
            return entidad;
        }

        public async Task DeleteEncuentrosDeportivo(string id)
        {
            var filtro = Builders<EncuentrosDeportivos>.Filter.Eq(x => x.id,id);
            await collectinEncuentrosDeportivos.DeleteOneAsync(filtro);
        }
        #endregion


        #region Resultados
        public async Task DeleteResultado(string id)
        {
            throw new System.NotImplementedException();
        }

        public async Task<List<Resultados>> GetListResultados()
        {
            throw new System.NotImplementedException();
        }

        public async Task<Resultados> GetResultadoById(string id)
        {
            throw new System.NotImplementedException();
        }

        public async Task InsertResultado(Resultados entidad)
        {
            throw new System.NotImplementedException();
        }

        public async Task UpdateResultado(Resultados entidad)
        {
            throw new System.NotImplementedException();
        }
        #endregion


        #region TablaDePosiciones
        public async Task<List<TablaDePosiciones>> GetListTablaDePosicionesSmart(string busqueda)
        {
            throw new System.NotImplementedException();
        }
        #endregion
    }
}
