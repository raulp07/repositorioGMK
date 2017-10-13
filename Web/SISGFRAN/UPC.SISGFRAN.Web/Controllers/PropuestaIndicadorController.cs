using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UPC.SISGFRAN.BL.Repositorios;
using UPC.SISGFRAN.EL.Inherited;

namespace UPC.SISGFRAN.Web.Controllers
{
    public class PropuestaIndicadorController : Controller
    {

        PropuestaIndicadorBL PropuestaIndicador = PropuestaIndicadorBL.PropuestaIndicador;
        //
        // GET: /PropuestaIndicador/
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ListaPlanMarketing()
        {
            try
            {
                List<PlanMarketingEL> PlanMarketing = new List<PlanMarketingEL>();
                PlanMarketing = PropuestaIndicador.GetAllPlanMarketing();
                return Json(new { PlanMarketing = PlanMarketing }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Herramienta.Herramientas.Log(e.Message);
                return Json(new { PlanMarketing = "" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult ListaComboProducto()
        {
            try
            {
                List<ComboProductoEL> ComboProducto = new List<ComboProductoEL>();
                ComboProducto = PropuestaIndicador.GetAllComboProducto();
                return Json(new { ComboProducto = ComboProducto }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Herramienta.Herramientas.Log(e.Message);
                return Json(new { ComboProducto = "" }, JsonRequestBehavior.AllowGet);
            }
            
        }

        [HttpPost]
        public JsonResult ListaCombo(ComboEL Combo)
        {
            try
            {
                List<ComboEL> _Combo = new List<ComboEL>();
                _Combo = PropuestaIndicador.GetAllCombo(Combo);
                return Json(new { Combo = _Combo }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Herramienta.Herramientas.Log(e.Message);
                return Json(new { Combo = "" }, JsonRequestBehavior.AllowGet);
            }
            
        }

        [HttpPost]
        public JsonResult ListaLocales()
        {
            try
            {
                List<LocalEL> Local = new List<LocalEL>();
                Local = PropuestaIndicador.ListaLocales();
                return Json(new { Local = Local }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Herramienta.Herramientas.Log(e.Message);
                return Json(new { Local = "" }, JsonRequestBehavior.AllowGet);
            }
            
        }

        [HttpPost]
        public JsonResult CalcularPropuestaxIndicadores(CalcularPropuestaxIndicadorEL CalcularPropuestaxIndicador)
        {
            try
            {
                List<CalcularPropuestaxIndicadorEL> listCalcularPropuestaxIndicador = new List<CalcularPropuestaxIndicadorEL>();
                listCalcularPropuestaxIndicador = PropuestaIndicador.CalcularPropuestaxIndicadores(CalcularPropuestaxIndicador);
                return Json(new { listCalcularPropuestaxIndicador = listCalcularPropuestaxIndicador }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Herramienta.Herramientas.Log(e.Message);
                return Json(new { Local = "" }, JsonRequestBehavior.AllowGet);
            }
            
        }

        [HttpPost]
        public JsonResult proyectarPropuestaxIndicadores(List<proyectarPropuestaIndicadorEL> proyectarPropuestaIndicador)
        {
            try
            {
                int resultado = 0;
                foreach (proyectarPropuestaIndicadorEL item in proyectarPropuestaIndicador)
                {
                    resultado = PropuestaIndicador.proyectarPropuestaxIndicadores(item);
                }
                Herramienta.Herramientas.LogTransaccion("Se completo la transaccion de la propuesta por indicador");
                return Json(new { resultado = resultado }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                Herramienta.Herramientas.Log(e.Message);
                return Json(new { Local = "" }, JsonRequestBehavior.AllowGet);
            }
            
        }
        

	}
}