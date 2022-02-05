using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDbApp.Models;
using MongoDbApp.Models.Comun;
using MongoDbApp.Repositorio.ArbitrosES;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDbApp.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArbitrosController : ControllerBase
    {
        CultureInfo culture = new CultureInfo("en-US", true);
        private readonly IArbitrosContratoCollection _repositoryArbitros;
        public ArbitrosController()
        {
            _repositoryArbitros = new ArbitrosRepositorioCollection();
        }

        [Route("[action]", Name = "GetAllListArbitros")]
        [HttpGet]
        public async Task<IActionResult> GetAllListArbitros()
        {
            bool ok = false;
            string mensaje = "Sin Datos";
            var arbitros = await Task.Run(() => _repositoryArbitros.GetListArbitros());
            foreach (var item in arbitros)
            {
                item.fechaTex = item.fecha.ToString("yyyy-MM-dd", culture);
            }
            if (arbitros != null || arbitros.Count() > 0)
            {
                mensaje = "ok";
                ok = true;
            }
            var data = new { arbitros, ok, mensaje };
            return Ok(data);
        }

        [Route("[action]", Name = "GetUnArbitroById")]
        [HttpGet]
        public async Task<IActionResult> GetUnArbitroById(string id)
        {
            ResponseApp response = new ResponseApp()
            {
                Message = "Ups!. Tu Solicitud No Pudo ser Procesada",
                Ok = false
            };
            if (!string.IsNullOrWhiteSpace(id))
            {
                var arbitro = await Task.Run(() => _repositoryArbitros.GetArbitroById(id));
                if (arbitro != null /*&& arbitro.id.Increment > 0*/)
                {
                    response.Message = "ok";
                    response.Ok = true;
                    var data = new { arbitro, response };
                    return Ok(data);
                }
            }
            else
            {
                response.Message = "Debes enviar un id";
            }
            return BadRequest(response);
        }

        [Route("[action]")]
        [HttpPost]
        public async Task<IActionResult> CreateArbitro([FromBody] Arbitros entidad)
        {
            ResponseApp data = new ResponseApp()
            {
                Message = "Ups!. Tu Solicitud No Pudo ser Procesada",
                Ok = false
            };
            try
            {
                if (ModelState.IsValid)
                {
                    await Task.Run(() => _repositoryArbitros.InsertArbitro(entidad));
                }
                else
                {
                    foreach (var item in ModelState.Values)
                    {
                        if (item.Errors[0].ErrorMessage == "")
                        {
                            data.Message += item.Errors[0].Exception.Message + " ";
                        }
                        else
                        {
                            data.Message += item.Errors[0].ErrorMessage + " ";
                        }
                    }
                    return BadRequest(data);
                }
                return Created("Created", true);
            }
            catch (Exception x)
            {
                data.Message = "Ups!. Algo salio mal!. Error interno. " + x.HResult;
                return BadRequest(data);
            }
        }

        [Route("[action]", Name = "UpdateArbitro")]
        [HttpPut]
        public async Task<IActionResult> UpdateArbitro([FromBody] Arbitros entidad)
        {
            ResponseApp data = new ResponseApp()
            {
                Message = "Ups!. Tu Solicitud No Pudo ser Procesada",
                Ok = false
            };
            try
            {
                if (ModelState.IsValid && !string.IsNullOrWhiteSpace(entidad.id))
                {
                    //entidad.id = new MongoDB.Bson.ObjectId(entidad.idTex);
                    await Task.Run(() => _repositoryArbitros.UpdateArbitro(entidad));
                }
                else
                {
                    data.Message = "id no puede ser null";
                    foreach (var item in ModelState.Values)
                    {
                        if (item.Errors[0].ErrorMessage == "")
                        {
                            data.Message += item.Errors[0].Exception.Message + " ";
                        }
                        else
                        {
                            data.Message += item.Errors[0].ErrorMessage + " ";
                        }
                    }
                    return BadRequest(data);
                }
                return Ok(data);
            }
            catch (Exception x)
            {
                data.Message = "Ups!. Algo salio mal!. Error interno. " + x.HResult;
                return BadRequest(data);
            }
        }

        [Route("[action]", Name = "DeleteArbitro")]
        [HttpDelete]
        public async Task<IActionResult> DeleteArbitro(string idString)
        {
            if (!string.IsNullOrWhiteSpace(idString))
            {
                var id = idString.Trim();
                await Task.Run(() => _repositoryArbitros.DeleteArbitro(id));
                return Ok(true);
            }
            return BadRequest();
        }
    }
}
