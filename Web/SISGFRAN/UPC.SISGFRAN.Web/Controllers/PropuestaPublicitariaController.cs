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
        private ResultadoEncuestaBL ResultadoEncuesta = ResultadoEncuestaBL.ResultadoEncuesta;

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ListaMedios_X_Local() {
            try
            {
                List<LocalEL> listLocal = new List<LocalEL>();
                listLocal = ResultadoEncuesta.GetAllDatoMedio_X_Local();
                return Json(new { listLocal = listLocal }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Herramienta.Herramientas.Log(e.Message);
                return Json(new { listLocal = "" }, JsonRequestBehavior.AllowGet);
            }
            
        }

        [HttpPost]
        public JsonResult ListaMediosComunicacion() {
            try
            {
                List<ResultadoEncuestaEL> listResultadoEncuesta = new List<ResultadoEncuestaEL>();
                listResultadoEncuesta = ResultadoEncuesta.GetAllDatoMedioPublicitario();
                return Json(new { listResultadoEncuesta = listResultadoEncuesta }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Herramienta.Herramientas.Log(e.Message);
                return Json(new { listResultadoEncuesta = "" }, JsonRequestBehavior.AllowGet);
            }
            
        }

        [HttpPost]
        public JsonResult RegistroPropuesta(PropuestaPublicidadEL PropuestaPublicidad, List<DetallePropuestaPublicidadEL> DetallePropuestaPublicidad)
        {
            try
            {
                int codPropuestapublicidad = ResultadoEncuesta.RegistroPropuesta(PropuestaPublicidad);
                foreach (DetallePropuestaPublicidadEL item in DetallePropuestaPublicidad)
                {
                    item.codPropuestapublicidad = codPropuestapublicidad;
                    int resultado = ResultadoEncuesta.RegistroDetallePropuesta(item);
                }
                Herramienta.Herramientas.LogTransaccion("Se completo el registro de la propuesta publicitaria");
                return Json(new { listLocal = "" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Herramienta.Herramientas.Log(e.Message);
                return Json(new { listLocal = "" }, JsonRequestBehavior.AllowGet);
            }
        }

	}
}