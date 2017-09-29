using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UPC.SISGFRAN.BL.Repositorios;
using UPC.SISGFRAN.EL.Inherited;

namespace UPC.SISGFRAN.Web.Controllers
{
    public class SeguimientoPlanMarketingController : Controller
    {
        //
        // GET: /SeguimientoPlanMarketing/
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ListaObjetivo(ObjetivoEL DE)
        {
            List<ObjetivoEL> ListObjetivo = new List<ObjetivoEL>();
            ListObjetivo = SeguimientoPlanMarketingBL.SeguimientoPlanMarketing.GetAllObjetivo(DE);
            return Json(new { ListObjetivo = ListObjetivo }, JsonRequestBehavior.AllowGet);
        }

    }
}