using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebFrontEnd.Controllers
{
    public class EventoDeportivoController : Controller
    {
        #region Deportistas
        public IActionResult GestionEvento(string id)
        {
            ViewBag.idString = "";
            if (!string.IsNullOrWhiteSpace(id))
            {
                ViewBag.idString = id;
            }
            return View();
        }
        public IActionResult GetEventos()
        {
            return View();
        }
        #endregion
    }
}
