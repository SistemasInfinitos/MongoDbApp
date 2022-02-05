using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDbApp.Models;
using MongoDbApp.Models.Comun;
using MongoDbApp.Repositorio.TemporadasES;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDbApp.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class TemporadasController : ControllerBase
    {
        CultureInfo culture = new CultureInfo("en-US", true);
        private readonly ITemporadasContratoCollection _repositoryTemporadas;
        public TemporadasController()
        {
            _repositoryTemporadas = new TemporadasRepositorioCollection();
        }

        [Route("[action]", Name = "GetAllListTemporadas")]
        [HttpGet]
        public async Task<IActionResult> GetAllListTemporadas()
        {
            bool ok = false;
            string mensaje = "Sin Datos";
            var temporadas = await Task.Run(() => _repositoryTemporadas.GetListTemporadas());
            foreach (var item in temporadas)
            {
                item.fechaTex = item.fecha.ToString("yyyy-MM-dd", culture);
                item.fechaInicioTex = item.fechaInicio.ToString("yyyy-MM-dd", culture);
                item.fechaFinTex = item.fechaFin.ToString("yyyy-MM-dd", culture);
            }
            if (temporadas != null || temporadas.Count() > 0)
            {
                mensaje = "ok";
                ok = true;
            }
            var data = new { temporadas, ok, mensaje };
            return Ok(data);
        }

        [Route("[action]", Name = "GetUnTemporadaById")]
        [HttpGet]
        public async Task<IActionResult> GetUnaTemporadaById(string id)
        {
            ResponseApp response = new ResponseApp()
            {
                Message = "Ups!. Tu Solicitud No Pudo ser Procesada",
                Ok = false
            };
            if (!string.IsNullOrWhiteSpace(id))
            {
                var temporada = await Task.Run(() => _repositoryTemporadas.GetTemporadaById(id));
                if (temporada != null)
                {
                    temporada.fechaInicioTex = temporada.fechaInicio.ToString("yyyy-MM-dd", culture);
                    temporada.fechaFinTex = temporada.fechaFin.ToString("yyyy-MM-dd", culture);

                    response.Message = "ok";
                    response.Ok = true;
                    var data = new { temporada, response };
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
        public async Task<IActionResult> CreateTemporada([FromBody] Temporadas entidad)
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
                    await Task.Run(() => _repositoryTemporadas.InsertTemporada(entidad));
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

        [Route("[action]", Name = "UpdateTemporada")]
        [HttpPut]
        public async Task<IActionResult> UpdateTemporada([FromBody] Temporadas entidad)
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
                    await Task.Run(() => _repositoryTemporadas.UpdateTemporada(entidad));
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

        [Route("[action]", Name = "DeleteTemporada")]
        [HttpDelete]
        public async Task<IActionResult> DeleteTemporada(string idString)
        {
            if (!string.IsNullOrWhiteSpace(idString))
            {
                var id = idString.Trim();
                await Task.Run(() => _repositoryTemporadas.DeleteTemporada(id));
                return Ok(true);
            }
            return BadRequest();
        }
    }
}
