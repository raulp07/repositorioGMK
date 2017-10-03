﻿using System;
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

        [HttpPost]
        public JsonResult ListaEstrategia(EstrategiaEL DE)
        {
            List<EstrategiaEL> ListEstrategia = new List<EstrategiaEL>();
            ListEstrategia = SeguimientoPlanMarketingBL.SeguimientoPlanMarketing.GetAllEstrategia(DE);
            return Json(new { ListEstrategia = ListEstrategia }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ListaAccion(AccionEL DE)
        {
            List<AccionEL> ListAccion = new List<AccionEL>();
            ListAccion = SeguimientoPlanMarketingBL.SeguimientoPlanMarketing.GetAllAccion(DE);
            return Json(new { ListAccion = ListAccion }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ActualizarAccion(AccionEL DE)
        {
            List<AccionEL> ListAccion = new List<AccionEL>();
            if (SeguimientoPlanMarketingBL.SeguimientoPlanMarketing.ActualizacionAccion(DE) > 0)
            {
                DE.codAccion = 0;
                ListAccion = SeguimientoPlanMarketingBL.SeguimientoPlanMarketing.GetAllAccion(DE);
            }
            return Json(new { ListAccion = ListAccion }, JsonRequestBehavior.AllowGet);
        }

    }
}