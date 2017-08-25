using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using UPC.SISGFRAN.Web.Helper;
using UPC.SISGFRAN.EL.Comunes;
using UPC.SISGFRAN.EL.Inherited;
using UPC.SISGFRAN.BL.Repositorios;

namespace UPC.SISGFRAN.Web.Controllers
{
    public class UsuarioController : Controller
    {

        #region "Variables globales"
        UsuarioBL usuarioBL = new UsuarioBL();
        #endregion

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(UsuarioEL usuario)
        {
            string mensaje = "";
            if (IsValid(usuario.CtaUsuario, usuario.Password))
            {
                int codApp;
                Int32.TryParse(Constantes.CodigoAplicacion, out codApp);
                AplicacionEL oAplicacion = new AplicacionEL() { Id = codApp };
                PerfilEL oPerfil = new PerfilEL (){ Aplicacion = oAplicacion};
                usuario.Perfil = oPerfil;
                UsuarioEL usuarioLogeado = usuarioBL.Login(usuario);
                if (usuarioLogeado.CodeMessage == 0)
                {
                    mensaje = "Exito";
                    UsuarioEL resultado = null;
                    resultado = usuarioBL.GetUsuarioById(usuarioLogeado.Id);
                    FormsAuthentication.SetAuthCookie(usuario.CtaUsuario, true);
                    SesionUsuario.Usuario = resultado;
                    SesionUsuario.Aplicacion = new AplicacionEL() { Id = codApp };
                    SesionUsuario.MenuRoot = UsuarioController.SetearMenu(false);
                }
                else
                {
                    mensaje = usuarioLogeado.MessageErr;
                }
            }
            else
            {
                mensaje = "Ingrese los campos requeridos";
            }

            return Json(mensaje, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOut()
        {
            var entrada = SesionUsuario.Usuario;

            if (entrada != null)
            {
                SesionUsuario.Usuario = null;
                SesionUsuario.MenuRoot = null;
            }

            FormsAuthentication.SignOut();

            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Login", "Usuario");
        }


        //
        // GET: /Usuario/
        public ActionResult Index()
        {
            return View();
        }

        #region Metodos

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        private bool IsValid(string usuario, string pass)
        {
            bool valido = false;
            if (usuario != null || pass != null)
            {
                valido = true;
            }
            return valido;
        }

        public static List<OpcionXPerfilEL> SetearMenu(bool padre)
        {
            List<OpcionXPerfilEL> lista = null;
            OpcionXPerfilBL menuBL = new OpcionXPerfilBL();

            OpcionXPerfilEL opcionesXPerfil = new OpcionXPerfilEL()
            {
                Aplicacion = SesionUsuario.Aplicacion,
                Perfil = SesionUsuario.Usuario.Perfil
            };

            lista = menuBL.ListMenu(opcionesXPerfil);

            List<OpcionXPerfilEL> menuArbol = new List<OpcionXPerfilEL>();
            //primero seteamos los padres
            foreach (OpcionXPerfilEL m in lista.Where(x => x.Opcion.PadreId == 0))
            {
                AsignarMenu(menuArbol, m);
            }
            //seteamos los hijos
            if (!padre)
            {
                foreach (OpcionXPerfilEL m in lista.Where(x => x.Opcion.PadreId != 0))
                {
                    AsignarMenu(menuArbol, m);
                }
            }

            return menuArbol;
        }

        // Arma el arbol de menus del usuario.
        private static bool AsignarMenu(List<OpcionXPerfilEL> menuLista, OpcionXPerfilEL opcionXPerfil)
        {
            if (menuLista == null)
                return false;

            if (opcionXPerfil.Opcion.PadreId == 0)
            {
                menuLista.Add(opcionXPerfil);
                return true;
            }

            bool agregado = false;
            foreach (var m in menuLista)
            {
                if (m.Opcion.Id == opcionXPerfil.Opcion.PadreId)
                {
                    if (m.Hijos == null)
                        m.Hijos = new List<OpcionXPerfilEL>();
                    m.Hijos.Add(opcionXPerfil);
                    agregado = true;
                    break;
                }
            }

            if (!agregado)
            {
                foreach (var m in menuLista)
                {
                    //agregado = AsignarMenu(m.Hijos, opcion);
                    if (agregado)
                        break;
                }
            }

            return agregado;
        }

        #endregion
	}
}