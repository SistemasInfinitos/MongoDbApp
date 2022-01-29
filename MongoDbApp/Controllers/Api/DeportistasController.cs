using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDbApp.Models;
using MongoDbApp.Models.Comun;
using MongoDbApp.Repositorio.DeportistasES;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace MongoDbApp.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeportistasController : ControllerBase
    {
        CultureInfo culture = new CultureInfo("en-US", true);
        private readonly IDeportistasContratoCollection _repositoryDeportistas;
        public DeportistasController()
        {
            _repositoryDeportistas = new DeportistasRepositorioCollection();
        }


        [Route("[action]", Name = "GetAllListDeportistas")]
        [HttpGet]
        public async Task<IActionResult> GetAllListDeportistas()
        {
            bool ok = false;
            string mensaje = "Sin Datos";
            var Deportistas = await Task.Run(() => _repositoryDeportistas.GetListDeportistas());
            foreach (var item in Deportistas)
            {
                item.idTex = item.id.ToString();
                item.fechaTex = item.fecha.ToString("yyyy-MM-dd", culture);
            }
            if (Deportistas != null || Deportistas.Count()>0)
            {
                mensaje = "ok";
                ok = true;
            }
            var data = new { Deportistas, ok, mensaje };
            return Ok(data);
        }

        [Route("[action]", Name = "GetUnDeportistaById")]
        [HttpGet]
        public async Task<IActionResult> GetUnDeportistaById(string id)
        {
            ResponseApp response = new ResponseApp()
            {
                Message = "Ups!. Tu Solicitud No Pudo ser Procesada",
                Ok = false
            };
            if (!string.IsNullOrWhiteSpace(id))
            {
                var deportista = await Task.Run(() => _repositoryDeportistas.GetDeportistasById(id));
                if (deportista != null && deportista.id.Increment>0)
                {
                    deportista.idTex = deportista.id.ToString();
                    response.Message = "ok";
                    response.Ok = true;
                    var data = new { deportista, response };
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
        public async Task<IActionResult> CreateDeportista([FromBody] Deportistas entidad)
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
                    try
                    {
                        await Task.Run(() => _repositoryDeportistas.InsertDeportistas(entidad));
                        return Created("Created", true);
                    }
                    catch (Exception e)
                    {
                        data.Message = "Ups!. Algo salio mal!. Error interno. " + x.HResult;
                    }
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
                return BadRequest(data);
            }
            catch (Exception x)
            {
                data.Message = "Ups!. Algo salio mal!. Error interno. " + x.HResult;
                return BadRequest(data);
            }
        }

        [Route("[action]", Name = "UpdateDeportista")]
        [HttpPut]
        public async Task<IActionResult> UpdateDeportista([FromBody] Deportistas entidad)
        {
            ResponseApp data = new ResponseApp()
            {
                Message = "Ups!. Tu Solicitud No Pudo ser Procesada",
                Ok = false
            };
            try
            {
                if (ModelState.IsValid && !string.IsNullOrWhiteSpace(entidad.idTex))
                {
                    entidad.id =new MongoDB.Bson.ObjectId(entidad.idTex);
                    await Task.Run(() => _repositoryDeportistas.UpdateDeportistas(entidad));
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

        [Route("[action]", Name = "DeleteDeportista")]
        [HttpDelete]
        public async Task<IActionResult> DeleteDeportista(string idString)
        {
            if (!string.IsNullOrWhiteSpace(idString))
            {
                var id = idString.Trim();
                await Task.Run(() => _repositoryDeportistas.DeleteDeportistas(id));
                return Ok(true);
            }
            return BadRequest();
        }
    }
}
