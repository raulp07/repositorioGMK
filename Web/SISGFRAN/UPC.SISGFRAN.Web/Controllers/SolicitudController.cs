using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;
using System.Web.Security;
using UPC.SISGFRAN.Web.Helper;
using UPC.SISGFRAN.EL.Comunes;
using UPC.SISGFRAN.EL.Inherited;
using UPC.SISGFRAN.BL.Repositorios;
using UPC.SISGFRAN.WS.Repositorios;
using UPC.SISGFRAN.EL.NonInherited;
using UPC.SISGFRAN.Web.Helper.PdfReportGenerator;
using System.Web.Mvc;
using System.Configuration;
using System.Text;
using UPC.SISGFRAN.Web.Helper.Mail;

namespace UPC.SISGFRAN.Web.Controllers
{
    public class SolicitudController : PdfViewController
    {
        #region "Variables globales"
        SolicitudBL solicitudBL = new SolicitudBL();
        EvaluadorWS evaluadorClient = new EvaluadorWS();
        #endregion

        //
        // GET: /Solicitud/
        public ActionResult Index(int page = 1, int pageSize = 10, string sort = "Id", string sortdir = "DESC")
        {
            SolicitudEL records = new SolicitudEL();
            ListaPaginada<SolicitudEL> listaContentSolicitud = new ListaPaginada<SolicitudEL>();

            string desc = string.Empty;
            List<SolicitudEL> listSolicitudesPendientes = solicitudBL.GetSolicitudesPendientes(desc);

            listaContentSolicitud.Content = listSolicitudesPendientes
                        .OrderBy(sort + " " + sortdir)
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();

            // Count
            listaContentSolicitud.TotalRecords = listSolicitudesPendientes.Count();
            listaContentSolicitud.CurrentPage = page;
            listaContentSolicitud.PageSize = pageSize;

            records.ListaSolicitudes = listaContentSolicitud;
            return View(records);
        }

        [HttpGet]
        public ActionResult BuscarSolicitud(string desc = null, int page = 1, int pageSize = 10, string sort = "Id", string sortdir = "DESC")
        {
            try
            {
                SolicitudEL records = new SolicitudEL();
                desc = (desc == null ? "" : desc);
                ViewBag.desc = desc;

                ListaPaginada<SolicitudEL> listaContentSolicitud = new ListaPaginada<SolicitudEL>();

                List<SolicitudEL> listSolicitudesPendientes = solicitudBL.GetSolicitudesPendientes(desc);

                listaContentSolicitud.Content = listSolicitudesPendientes
                            .OrderBy(sort + " " + sortdir)
                            .Skip((page - 1) * pageSize)
                            .Take(pageSize)
                            .ToList();

                // Count
                listaContentSolicitud.TotalRecords = listSolicitudesPendientes.Count();
                listaContentSolicitud.CurrentPage = page;
                listaContentSolicitud.PageSize = pageSize;

                records.ListaSolicitudes = listaContentSolicitud;
                return PartialView("_Solicitud", records);
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Exportar(int id)
        {
            string titulo = string.Empty;
            int solId = id;
            SolicitudEL solicitudEval = solicitudBL.GetResultadoEvaluacion(solId);

            if (solicitudEval != null)
            {
                titulo = "Evaluación de la solicitud N° " + solicitudEval.NumSolicitud;
                FillImageUrl(solicitudEval.ReporteEvaluacion, "logo_pc.jpeg");
                return this.ViewPdf(titulo, "_ReporteEval", solicitudEval);
            }
            else
            {
                TempData["msg"] = "No existe evaluación";
                return RedirectToAction("Index");
            }
            
        }

        [HttpGet]
        public ActionResult EnviarCorreo(string id, string fecha, string hora, string correo)
        {
            try
            {
                int idSol = Convert.ToInt32(id);
                SolicitudEL oSolicitud = solicitudBL.GetSolicitudById(idSol);

                // Configurar envio de correo
                string subject = string.Format("{0}: {1} - {2}", ConfigurationManager.AppSettings.Get("AsuntoMail"), "Pardos Chicken", DateTime.Now.ToString("dd / MMM / yyy hh:mm:ss"));
                string mailFrom = ConfigurationManager.AppSettings.Get("MailEmisor");
                string passwordMailEmisor = ConfigurationManager.AppSettings.Get("PasswordMailEmisor");
                StringBuilder buffer = new StringBuilder();
                buffer.Append("Estimado <b>{0} {1}, {2} </b> ");
                buffer.Append(" Es grato saludarlo e informarle que se le convoca a una entrevista en nuestra oficina principal<br />");
                buffer.Append(" Fecha:" + fecha + " <br/><br/>");
                buffer.Append(" Hora:" + hora + "<br/><br/>");
                buffer.Append(" Saludos cordiales. <br/><br/>");
                buffer.Append("<i> Nota: Por favor no responda este correo. <i>");


                MailHelper.SendMail(mailFrom, passwordMailEmisor, correo, subject, string.Format(buffer.ToString(), oSolicitud.Solicitante.ApellidoPaterno, oSolicitud.Solicitante.ApellidoMaterno, oSolicitud.Solicitante.Nombres), true, System.Net.Mail.MailPriority.Normal);


                return Json(new { status = true, message = "Se envió correctamente el correo." }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
            }
        }

        #region Metodos

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        private void FillImageUrl(ReporteEvaluacionEL reporte, string imageName)
        {
            string url = string.Format("{0}://{1}{2}", Request.Url.Scheme, Request.Url.Authority, Url.Content("~"));
            reporte.ImageUrl = url + "Content/Images/" + imageName;
        }

        public JsonResult Evaluar(string solicitud)
        {
            try
            {
                SolicitudEL solictudEvaluada = null;
                ReporteEvaluacionEL resultado;
                int oEstado = -1;
                int idSolicitud = Convert.ToInt32(solicitud);

                //Obtener datos de la solicitud a Evaluar
                solictudEvaluada = solicitudBL.GetSolicitudById(idSolicitud);

                SolicitudEL solicitudActualizada;
                // Enviar al servicio web a evaluar
                resultado = evaluadorClient.EvaluarSolicitud(solictudEvaluada);
                resultado.UsuarioCreacion = SesionUsuario.Usuario.Id;

                if (resultado.Resultado)
	            {
		            oEstado = (int)Constantes.EstadoSolicitud.Aprobada;
	            }
                else
	            {
                    oEstado = (int)Constantes.EstadoSolicitud.Rechazada;
	            }

                // Actualizar el estado de la solicitud
                ParametroEL estado = new ParametroEL(){ Codigo = oEstado };

                SolicitudEL oSolicitud = new SolicitudEL()
                {
                    Id = idSolicitud,
                    Estado = estado,
                    UsuarioModifica = SesionUsuario.Usuario.Id,
                    ReporteEvaluacion = resultado
                };

                solicitudActualizada = solicitudBL.Actualizar(oSolicitud);

                // Actualizar resultado del reporte para descargar el reporte.
                solicitudBL.RegistrarReporteEvaluacion(oSolicitud);

                if (resultado.Resultado) // true
                {
                    //Enviar Nombre, Correo, 
                    return Json(new { status = true, message = "La solicitud de franquicia seleccionada ha sido aprobada.", Solicitante = solicitudActualizada.Solicitante.NombreCompleto, Email = solicitudActualizada.Solicitante.Email }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { status = false, message = "La solicitud de franquicia seleccionada ha sido rechazada" }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = ex.Message.ToString() }, JsonRequestBehavior.AllowGet);
            }

        }


        #endregion

	}
}