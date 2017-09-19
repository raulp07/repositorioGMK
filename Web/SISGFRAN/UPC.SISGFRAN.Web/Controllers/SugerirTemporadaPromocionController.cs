using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UPC.SISGFRAN.BL.Repositorios;
using UPC.SISGFRAN.EL.Inherited;

namespace UPC.SISGFRAN.Web.Controllers
{
    public class SugerirTemporadaPromocionController : Controller
    {
        PropuestaIndicadorBL PropuestaIndicador = PropuestaIndicadorBL.PropuestaIndicador;
        //
        // GET: /SugerirTemporadaPromocion/
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public JsonResult ListaCombo(ComboEL Combo)
        {
            List<ComboEL> _Combo = new List<ComboEL>();
            _Combo = PropuestaIndicador.GetAllCombo(Combo);
            return Json(new { Combo = _Combo }, JsonRequestBehavior.AllowGet);
        }
	}
}