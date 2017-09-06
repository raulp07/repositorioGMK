using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace WCFRest.Persistencia
{
    public class ConexionUtil
    {
        private static ConexionUtil _conexionUtil;
        private ConexionUtil() { }
        public static ConexionUtil _ConexionUtil
        {
            get
            {
                if (_conexionUtil == null)
                {
                    _conexionUtil = new ConexionUtil();
                }
                return _conexionUtil;
            }
        }

        private static string Server = ConfigurationManager.AppSettings["Server"];
        private static string BaseDatos = ConfigurationManager.AppSettings["BaseDatos"];
        private static string User = ConfigurationManager.AppSettings["User"];
        private static string PWD = ConfigurationManager.AppSettings["PWD"];

        public static string Cadena
        {
            get
            {
                return "Data Source=" + Server + ";Initial Catalog=" + BaseDatos + ";user=" + User + ";password=" + PWD + ";";
            }
        }
    }
}