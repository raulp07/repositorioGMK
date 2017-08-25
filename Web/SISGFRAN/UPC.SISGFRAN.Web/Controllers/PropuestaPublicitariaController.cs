using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UPC.SISGFRAN.BL.Repositorios;
using UPC.SISGFRAN.EL.Inherited;
namespace UPC.SISGFRAN.Web.Controllers
{
    public class PropuestaPublicitariaController : Controller
    {
        //
        // GET: /PropuestaPublicitaria/
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ListaMedios_X_Local() {
            List<LocalEL> listLocal = new List<LocalEL>();
            listLocal = new ResultadoEncuestaBL().GetAllDatoMedio_X_Local();


            return Json(new { listLocal = listLocal }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListaMediosComunicacion() {
            List<ResultadoEncuestaEL> listResultadoEncuesta = new List<ResultadoEncuestaEL>();
            listResultadoEncuesta = new ResultadoEncuestaBL().GetAllDatoMedioPublicitario();
            return Json(new { listResultadoEncuesta = listResultadoEncuesta }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult RegistroPropuesta(PropuestaPublicidadEL PropuestaPublicidad, List<DetallePropuestaPublicidadEL> DetallePropuestaPublicidad)
        {
            ResultadoEncuestaBL obj = new ResultadoEncuestaBL();
            int codPropuestapublicidad = obj.RegistroPropuesta(PropuestaPublicidad);
            foreach (DetallePropuestaPublicidadEL item in DetallePropuestaPublicidad)
            {
                item.codPropuestapublicidad = codPropuestapublicidad;
                int resultado = obj.RegistroDetallePropuesta(item);
            }
            return Json(new { listLocal = "" }, JsonRequestBehavior.AllowGet);
        }

	}
}