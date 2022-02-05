using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDbApp.Models;
using MongoDbApp.Models.Comun;
using MongoDbApp.Repositorio.EventosDeportivosES;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDbApp.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventosDeportivosController : ControllerBase
    {
        CultureInfo culture = new CultureInfo("en-US", true);
        private readonly IEventosDeportivosContradoCollection _repositoryEventosDeportivos;
        public EventosDeportivosController()
        {
            _repositoryEventosDeportivos = new EventosDeportivosRepositorioCollection();
        }
        #region EncuentrosDeportivos

        [Route("[action]", Name = "GetAllListEncuentrosDeportivos")]
        [HttpGet]
        public async Task<IActionResult> GetAllListEncuentrosDeportivos()
        {
            bool ok = false;
            string mensaje = "Sin Datos";
            var eventos = await Task.Run(() => _repositoryEventosDeportivos.GetListEncuentrosDeportivos());
            if (eventos != null || eventos.Count() > 0)
            {
                mensaje = "ok";
                ok = true;
            }
            var data = new { eventos, ok, mensaje };
            return Ok(data);
        }

        [Route("[action]", Name = "GetEncuentrosDeportivoById")]
        [HttpGet]
        public async Task<IActionResult> GetEncuentrosDeportivoById(string id)
        {
            ResponseApp response = new ResponseApp()
            {
                Message = "Ups!. Tu Solicitud No Pudo ser Procesada",
                Ok = false
            };
            if (!string.IsNullOrWhiteSpace(id))
            {
                var evento = await Task.Run(() => _repositoryEventosDeportivos.GetEncuentrosDeportivoById(id));
                if (evento != null)
                {
                    response.Message = "ok";
                    response.Ok = true;
                    var data = new { evento, response };
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
        public async Task<IActionResult> CreateEncuentrosDeportivo([FromBody] EncuentrosDeportivos entidad)
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
                  var evento=  await Task.Run(() => _repositoryEventosDeportivos.InsertEncuentrosDeportivo(entidad));
                  return Ok(evento);
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
            }
            catch (Exception x)
            {
                data.Message = "Ups!. Algo salio mal!. Error interno. " + x.HResult;
                return BadRequest(data);
            }
        }

        [Route("[action]", Name = "UpdateEncuentrosDeportivo")]
        [HttpPut]
        public async Task<IActionResult> UpdateEncuentrosDeportivo([FromBody] EncuentrosDeportivos entidad)
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
                    var evento = await Task.Run(() => _repositoryEventosDeportivos.UpdateEncuentrosDeportivo(entidad));
                    return Ok(evento);
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
                
            }
            catch (Exception x)
            {
                data.Message = "Ups!. Algo salio mal!. Error interno. " + x.HResult;
                return BadRequest(data);
            }
        }

        [Route("[action]", Name = "DeleteEncuentrosDeportivo")]
        [HttpDelete]
        public async Task<IActionResult> DeleteEncuentrosDeportivo(string idString)
        {
            if (!string.IsNullOrWhiteSpace(idString))
            {
                var id = idString.Trim();
                await Task.Run(() => _repositoryEventosDeportivos.DeleteEncuentrosDeportivo(id));
                return Ok(true);
            }
            return BadRequest();
        }
        #endregion
    }
}
