using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDbApp.Models;
using MongoDbApp.Models.Comun;
using MongoDbApp.Repositorio.EquiposES;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDbApp.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquiposController : ControllerBase
    {
        CultureInfo culture = new CultureInfo("en-US", true);
        private readonly IEquiposContratoCollection _repositoryEquipos;
        public EquiposController()
        {
            _repositoryEquipos = new EquiposRepositorioCollection();
        }

        [Route("[action]", Name = "GetAllListEquipos")]
        [HttpGet]
        public async Task<IActionResult> GetAllListEquipos()
        {
            bool ok = false;
            string mensaje = "Sin Datos";
            var equipos = await Task.Run(() => _repositoryEquipos.GetListEquipos());
            foreach (var item in equipos)
            {
                item.fechaTex = item.fecha.ToString("yyyy-MM-dd", culture);
            }
            if (equipos != null || equipos.Count() > 0)
            {
                mensaje = "ok";
                ok = true;
            }
            var data = new { equipos, ok, mensaje };
            return Ok(data);
        }

        [Route("[action]", Name = "GetUnEquipoById")]
        [HttpGet]
        public async Task<IActionResult> GetUnEquipoById(string id)
        {
            ResponseApp response = new ResponseApp()
            {
                Message = "Ups!. Tu Solicitud No Pudo ser Procesada",
                Ok = false
            };
            if (!string.IsNullOrWhiteSpace(id))
            {
                var equipo = await Task.Run(() => _repositoryEquipos.GetEquiposById(id));
                if (equipo != null)
                {
                    response.Message = "ok";
                    response.Ok = true;
                    var data = new { equipo, response };
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
        public async Task<IActionResult> CreateEquipo([FromBody] Equipos entidad)
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
                    await Task.Run(() => _repositoryEquipos.InsertEquipo(entidad));
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

        [Route("[action]", Name = "UpdateEquipo")]
        [HttpPut]
        public async Task<IActionResult> UpdateEquipo([FromBody] Equipos entidad)
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
                    await Task.Run(() => _repositoryEquipos.UpdateEquipo(entidad));
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

        [Route("[action]", Name = "DeleteEquipo")]
        [HttpDelete]
        public async Task<IActionResult> DeleteEquipo(string idString)
        {
            if (!string.IsNullOrWhiteSpace(idString))
            {
                var id = idString.Trim();
                await Task.Run(() => _repositoryEquipos.DeleteEquipo(id));
                return Ok(true);
            }
            return BadRequest();
        }
    }
}
