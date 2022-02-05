using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDbApp.Models;
using MongoDbApp.Models.Comun;
using MongoDbApp.Repositorio.EntrenadoresES;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDbApp.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntrenadoresController : ControllerBase
    {
        CultureInfo culture = new CultureInfo("en-US", true);
        private readonly IEntrenadoresContratoCollection _repositoryEntrenadores;
        public EntrenadoresController()
        {
            _repositoryEntrenadores = new EntrenadoresRepositorioCollection();
        }

        [Route("[action]", Name = "GetAllListEntrenadores")]
        [HttpGet]
        public async Task<IActionResult> GetAllListEntrenadores()
        {
            bool ok = false;
            string mensaje = "Sin Datos";
            var entrenadores = await Task.Run(() => _repositoryEntrenadores.GetListEntrenadores());
            foreach (var item in entrenadores)
            {
                item.fechaTex = item.fecha.ToString("yyyy-MM-dd", culture);
            }
            if (entrenadores != null || entrenadores.Count() > 0)
            {
                mensaje = "ok";
                ok = true;
            }
            var data = new { entrenadores, ok, mensaje };
            return Ok(data);
        }

        [Route("[action]", Name = "GetUnEntrenadorById")]
        [HttpGet]
        public async Task<IActionResult> GetUnEntrenadorById(string id)
        {
            ResponseApp response = new ResponseApp()
            {
                Message = "Ups!. Tu Solicitud No Pudo ser Procesada",
                Ok = false
            };
            if (!string.IsNullOrWhiteSpace(id))
            {
                var entrenador = await Task.Run(() => _repositoryEntrenadores.GetEntrenadorById(id));
                if (entrenador != null)
                {
                    response.Message = "ok";
                    response.Ok = true;
                    var data = new { entrenador, response };
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
        public async Task<IActionResult> CreateEntrenador([FromBody] Entrenadores entidad)
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
                    await Task.Run(() => _repositoryEntrenadores.InsertEntrenador(entidad));
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

        [Route("[action]", Name = "UpdateEntrenador")]
        [HttpPut]
        public async Task<IActionResult> UpdateEntrenador([FromBody] Entrenadores entidad)
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
                    await Task.Run(() => _repositoryEntrenadores.UpdateEntrenador(entidad));
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

        [Route("[action]", Name = "DeleteEntrenador")]
        [HttpDelete]
        public async Task<IActionResult> DeleteEntrenador(string idString)
        {
            if (!string.IsNullOrWhiteSpace(idString))
            {
                var id = idString.Trim();
                await Task.Run(() => _repositoryEntrenadores.DeleteEntrenador(id));
                return Ok(true);
            }
            return BadRequest();
        }
    }
}
