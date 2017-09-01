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

        PropuestaIndicadorBL PropuestaIndicador = new PropuestaIndicadorBL();
        //
        // GET: /PropuestaIndicador/
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ListaPlanMarketing()
        {
            List<PlanMarketingEL> PlanMarketing = new List<PlanMarketingEL>();
            PlanMarketing = PropuestaIndicador.GetAllPlanMarketing();
            return Json(new { PlanMarketing = PlanMarketing }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListaComboProducto()
        {
            List<ComboProductoEL> ComboProducto = new List<ComboProductoEL>();
            ComboProducto = PropuestaIndicador.GetAllComboProducto();
            return Json(new { ComboProducto = ComboProducto }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListaCombo(ComboEL Combo)
        {
            List<ComboEL> _Combo = new List<ComboEL>();
            _Combo = PropuestaIndicador.GetAllCombo(Combo);
            return Json(new { Combo = _Combo }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListaLocales()
        {
            List<LocalEL> Local = new List<LocalEL>();
            Local = PropuestaIndicador.ListaLocales();
            return Json(new { Local = Local }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CalcularPropuestaxIndicadores(CalcularPropuestaxIndicadorEL CalcularPropuestaxIndicador)
        {
            List<CalcularPropuestaxIndicadorEL> listCalcularPropuestaxIndicador = new List<CalcularPropuestaxIndicadorEL>();
            listCalcularPropuestaxIndicador = PropuestaIndicador.CalcularPropuestaxIndicadores(CalcularPropuestaxIndicador);
            return Json(new { listCalcularPropuestaxIndicador = listCalcularPropuestaxIndicador }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult proyectarPropuestaxIndicadores(List<proyectarPropuestaIndicadorEL> proyectarPropuestaIndicador)
        {
            int resultado = 0;
            foreach (proyectarPropuestaIndicadorEL item in proyectarPropuestaIndicador)
            {
                resultado = PropuestaIndicador.proyectarPropuestaxIndicadores(item);
            }
            return Json(new { resultado = resultado }, JsonRequestBehavior.AllowGet);
        }
        

	}
}