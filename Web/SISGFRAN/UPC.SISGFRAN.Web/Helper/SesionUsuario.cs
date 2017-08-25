using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using UPC.SISGFRAN.EL.Inherited;

namespace UPC.SISGFRAN.Web.Helper
{
    public class SesionUsuario
    {

        #region "Propiedades"

        public static UsuarioEL Usuario
        {
            get { return Obtener<UsuarioEL>("entrada"); }
            set { Asignar("entrada", value); }
        }

        public static List<OpcionXPerfilEL> MenuRoot
        {
            get { return Obtener<List<OpcionXPerfilEL>>("MenuRoot"); }
            set { Asignar("MenuRoot", value); }
        }

        public static String UrlSite
        {
            get { return ConfigurationManager.AppSettings["urlSite"].ToString(); }
        }

        public static AplicacionEL Aplicacion
        {
            get { return Obtener<AplicacionEL>("aplicationWeb"); }
            set { Asignar("aplicationWeb", value); }
        }

        #endregion

        #region "Metodos"

        private static void Asignar(string key, object value)
        {
            if (HttpContext.Current.Session[key] == null)
            {
                HttpContext.Current.Session.Add(key, value);
            }
            else
            {
                HttpContext.Current.Session[key] = value;
            }
        }

        private static T Obtener<T>(string key)
        {
            var x = new HttpContextWrapper(HttpContext.Current);
            var y = x.Session[key];

            return (T)HttpContext.Current.Session[key];
        }

        #endregion
    }
}