using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace UPC.SISGFRAN.Web.Controllers
{
    public class Herramienta
    {
        private static Herramienta herramienta;
        private Herramienta() { }
        public static Herramienta Herramientas
        {
            get
            {
                if (herramienta == null)
                {
                    herramienta = new Herramienta();
                }
                return herramienta;
            }
        }

        public void Log(string sErrMsg)
        {
            string sYear = DateTime.Now.Year.ToString();
            string sMonth = DateTime.Now.Month.ToString();
            string sDay = DateTime.Now.Day.ToString();

            string sPathName = System.Web.HttpContext.Current.Server.MapPath("~") + @"Scripts\Log\LogError.txt";


            string sLogFormat = DateTime.Now.ToShortDateString().ToString() + " " + DateTime.Now.ToLongTimeString().ToString() + " ==> ";
            string sErrorTime = sYear + sMonth + sDay;

            StreamWriter sw = new StreamWriter(sPathName, true);
            sw.WriteLine(sLogFormat + sErrMsg);
            sw.Flush();
            sw.Close();
        }

        public void LogTransaccion(string sMsg)
        {
            string sYear = DateTime.Now.Year.ToString();
            string sMonth = DateTime.Now.Month.ToString();
            string sDay = DateTime.Now.Day.ToString();

            string sPathName = System.Web.HttpContext.Current.Server.MapPath("~") + @"Scripts\Log\Logtransaccion.txt";


            string sLogFormat = DateTime.Now.ToShortDateString().ToString() + " " + DateTime.Now.ToLongTimeString().ToString() + " ==> ";
            string sErrorTime = sYear + sMonth + sDay;

            StreamWriter sw = new StreamWriter(sPathName, true);
            sw.WriteLine(sLogFormat + sMsg);
            sw.Flush();
            sw.Close();
        }

    }
}