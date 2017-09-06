using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WCFRest.Persistencia
{
    public class ConexionUtil
    {
        public static string Cadena
        {
            get
            {
                return "Data Source=raul-pc;Initial Catalog=BDPARDOS;user=sa;password=123;";
            }
        }
    }
}