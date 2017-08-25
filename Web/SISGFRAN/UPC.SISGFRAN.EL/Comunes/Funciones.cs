using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace UPC.SISGFRAN.EL.Comunes
{
    public class Funciones
    {
        public Funciones()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        static public bool isNumeric(object value)
        {
            bool resultado;
            double numero;

            resultado = Double.TryParse(Convert.ToString(value), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out numero);
            return resultado;

        }
        //funcion adicionada para validar
        static public string ConvertSoles(object value)
        {
            string salida = "0.00";
            if (value == null || value == System.DBNull.Value)
            {
                salida = "0.00";
            }
            else
            {
                if (Convert.ToString(value) == string.Empty)
                    salida = "0.00";
                else
                    salida = string.Format("{0:n}", Convert.ToDouble(value) / 100);
            }
            return salida;
        }

        static public bool IsNumeric(string input)
        {
            bool flag = true;
            //Valid user input 
            string pattern = @"^[0-9]*$";
            Regex validate = new Regex(pattern);
            //Check the user input format 
            if (!validate.IsMatch(input))
            {
                flag = false;
            }
            return flag;
        }

        bool IsNumeric2(string inputString)
        {
            return Regex.IsMatch(inputString, "^[0-9]+$");
        }

        static public string CheckStr(object value)
        {
            string salida = string.Empty;
            if (value == null || value == System.DBNull.Value)
                salida = string.Empty;
            else
                salida = value.ToString();
            return salida.Trim();
        }

        static public Int64 CheckInt64(object value)
        {
            Int64 salida = 0;
            if (value == null || value == System.DBNull.Value)
            {
                salida = 0;
            }
            else
            {
                if (Convert.ToString(value) == string.Empty)
                    salida = 0;
                else
                    salida = Convert.ToInt64(value);
            }
            return salida;
        }

        static public float CheckFloat(object value)
        {
            int salida = 0;
            if (value == null || value == System.DBNull.Value)
            {
                salida = 0;
            }
            else
            {
                if (Convert.ToString(value) == string.Empty)
                    salida = 0;
                else
                    salida = Convert.ToInt32(value);
            }
            return salida;
        }

        static public int CheckInt(object value)
        {
            int salida = 0;
            if (value == null || value == System.DBNull.Value)
            {
                salida = 0;
            }
            else
            {
                if (Convert.ToString(value) == string.Empty)
                    salida = 0;
                else
                    salida = Convert.ToInt32(value);
            }
            return salida;
        }

        static public double CheckDbl(object value)
        {
            double salida = 0;
            if (value == null || value == System.DBNull.Value)
            {
                salida = 0;
            }
            else
            {
                if (Convert.ToString(value) == string.Empty)
                    salida = 0;
                else
                    salida = Convert.ToDouble(value);
            }
            return salida;
        }

        static public decimal CheckDecimal(object value)
        {
            decimal salida = 0;
            if (value == null || value == System.DBNull.Value)
            {
                salida = 0;
            }
            else
            {
                salida = Convert.ToDecimal(value);
            }
            return salida;
        }

        static public object CheckDblDB(object value)
        {
            double salida = 0;
            if (value == null || value == System.DBNull.Value)
            {
                return System.DBNull.Value;
            }
            salida = Convert.ToDouble(value);
            return salida;
        }

        static public double CheckDbl(object value, int nroDecimales)
        {
            double salida = 0;
            if (value == null || value == System.DBNull.Value)
            {
                salida = 0;
            }
            else
            {
                salida = Convert.ToDouble(value);
            }
            return redondearMontos(salida, nroDecimales);
        }

        static public double redondearMontos(double value, int nroDecimales)
        {
            return Math.Round(value, nroDecimales);
        }

        static public DateTime CheckDate(object value)
        {
            DateTime salida;
            if (value == null || value == System.DBNull.Value)
            {
                salida = DateTime.Now;
            }
            else
            {
                salida = Convert.ToDateTime(value);
            }

            return salida;
        }

        public static System.Data.DataTable dtParams()
        {
            System.Data.DbType tipo = new System.Data.DbType();
            System.Data.ParameterDirection direccion = new System.Data.ParameterDirection();
            System.Data.DataTable dt = new System.Data.DataTable();
            dt.Columns.Add("Nombre", System.Type.GetType("System.String"));
            dt.Columns.Add("Tipo", tipo.GetType());
            dt.Columns.Add("Size", System.Type.GetType("System.Int32"));
            dt.Columns.Add("Direccion", direccion.GetType());
            dt.Columns.Add("Valor", System.Type.GetType("System.Object"));

            return dt;
        }

        public static bool InsertarParam(System.Data.DataTable vdtParams,
            string vName,
            System.Data.DbType vType,
            int vSize,
            System.Data.ParameterDirection vDirection,
            object vValue)
        {

            System.Data.DataRow dr = vdtParams.NewRow();
            dr["Nombre"] = vName;
            dr["Tipo"] = vType;
            if (vSize == 0)
                dr["Size"] = 0;
            else
                dr["Size"] = vSize;

            dr["Direccion"] = vDirection;

            if (vValue == null)
                dr["Valor"] = DBNull.Value;
            else
                dr["Valor"] = vValue;

            vdtParams.Rows.Add(dr);
            return true;
        }

        public static double ConvertSolesToCentimos(double vMonto)
        {
            return (vMonto * 100);
        }

        public static string FormatoInternacional(string telefono)
        {
            string salida = string.Empty;

            salida = "51" + telefono;
            return salida;
        }

        static public string FormatFecha(string vFecha)
        {
            string fecha = string.Empty;
            fecha = vFecha.Replace(@"-", string.Empty);
            if (vFecha != string.Empty)
            {
                fecha = fecha.Substring(8, 2) + "/" + fecha.Substring(4, 2) + "/" + fecha.Substring(0, 4);
            }
            return fecha;
        }

        // Pasa un DateTime de C# a DD/MM/YYYY
        // no valen DateTime nulos (simplemente porque no hay DateTime nulos)
        public static string GetDateTimeAsDDMMYYYY(DateTime pidtValue)
        {
            string sDDMMYYYY, sDay, sMonth, sYear;

            sDay = Convert.ToString(pidtValue.Day);
            sDay = sDay.PadLeft(2, Convert.ToChar("0"));

            sMonth = Convert.ToString(pidtValue.Month);
            sMonth = sMonth.PadLeft(2, Convert.ToChar("0"));

            sYear = Convert.ToString(pidtValue.Year);

            sDDMMYYYY = sDay + "/" +
                sMonth + "/" +
                sYear;

            return (sDDMMYYYY);
        }

        // Pasa un DateTime de C# a DD/MM/YYYY HH:MM:SS
        // no valen DateTime nulos (simplemente porque no hay DateTime nulos)
        public static string GetDateTimeAsDDMMYYYY_HHMMSS(DateTime pidtValue)
        {
            string sDDMMYYYY_HHMMSS, sDay, sMonth, sYear, sHour, sMinute, sSecond;

            sDay = Convert.ToString(pidtValue.Day);
            sDay = sDay.PadLeft(2, Convert.ToChar("0"));

            sMonth = Convert.ToString(pidtValue.Month);
            sMonth = sMonth.PadLeft(2, Convert.ToChar("0"));

            sYear = Convert.ToString(pidtValue.Year);

            sHour = Convert.ToString(pidtValue.Hour);
            sHour = sHour.PadLeft(2, Convert.ToChar("0"));

            sMinute = Convert.ToString(pidtValue.Minute);
            sMinute = sMinute.PadLeft(2, Convert.ToChar("0"));

            sSecond = Convert.ToString(pidtValue.Second);
            sSecond = sSecond.PadLeft(2, Convert.ToChar("0"));

            sDDMMYYYY_HHMMSS = sDay + "/" +
                sMonth + "/" +
                sYear + " " +
                sHour + ":" +
                sMinute + ":" +
                sSecond;

            return (sDDMMYYYY_HHMMSS);
        }

        // Pasa un DateTime de C# a MM/DD/YYYY HH:MM:SS
        // no valen DateTime nulos (simplemente porque no hay DateTime nulos)
        public static string GetDateTimeAsMMDDYYYY_HHMMSS(DateTime pidtValue)
        {
            string sMMDDYYYY_HHMMSS, sDay, sMonth, sYear, sHour, sMinute, sSecond;

            sDay = Convert.ToString(pidtValue.Day);
            sDay = sDay.PadLeft(2, Convert.ToChar("0"));

            sMonth = Convert.ToString(pidtValue.Month);
            sMonth = sMonth.PadLeft(2, Convert.ToChar("0"));

            sYear = Convert.ToString(pidtValue.Year);

            sHour = Convert.ToString(pidtValue.Hour);
            sHour = sHour.PadLeft(2, Convert.ToChar("0"));

            sMinute = Convert.ToString(pidtValue.Minute);
            sMinute = sMinute.PadLeft(2, Convert.ToChar("0"));

            sSecond = Convert.ToString(pidtValue.Second);
            sSecond = sSecond.PadLeft(2, Convert.ToChar("0"));

            sMMDDYYYY_HHMMSS = sMonth + "/" +
                sDay + "/" +
                sYear + " " +
                sHour + ":" +
                sMinute + ":" +
                sSecond;

            return (sMMDDYYYY_HHMMSS);
        }

        // Pasa un DateTime de C# a MM/DD/YYYY
        // no valen DateTime nulos (simplemente porque no hay DateTime nulos)
        public static string GetDateTimeAsMMDDYYYY(DateTime pidtValue)
        {
            string sMMDDYYYY, sDay, sMonth, sYear;

            sDay = Convert.ToString(pidtValue.Day);
            sDay = sDay.PadLeft(2, Convert.ToChar("0"));

            sMonth = Convert.ToString(pidtValue.Month);
            sMonth = sMonth.PadLeft(2, Convert.ToChar("0"));

            sYear = Convert.ToString(pidtValue.Year);

            sMMDDYYYY = sMonth + "/" +
                sDay + "/" +
                sYear;

            return (sMMDDYYYY);
        }

        // Pasa un DateTime de C# a MM/YYYY
        // no valen DateTime nulos (simplemente porque no hay DateTime nulos)
        public static string GetDateTimeAsMMYYYY(DateTime pidtValue)
        {
            string sMMYYYY, sMonth, sYear;

            sMonth = Convert.ToString(pidtValue.Month);
            sMonth = sMonth.PadLeft(2, Convert.ToChar("0"));

            sYear = Convert.ToString(pidtValue.Year);

            sMMYYYY = sMonth + "/" +
                sYear;

            return (sMMYYYY);
        }

        // Pasa un DateTime de C# a YYYYMM
        // no valen DateTime nulos (simplemente porque no hay DateTime nulos)
        public static string GetDateTimeAsYYYYMM(DateTime pidtValue)
        {
            string sYYYYMM, sMonth, sYear;

            sMonth = Convert.ToString(pidtValue.Month);
            sMonth = sMonth.PadLeft(2, Convert.ToChar("0"));

            sYear = Convert.ToString(pidtValue.Year);

            sYYYYMM = sYear + sMonth;

            return (sYYYYMM);
        }

        // Pasa un string DD/MM/YYYY a DateTime de C#
        // sólo vale con strings con el formato DD/MM/YYYY, no se mandan horas ni minutos ni segundos
        static public DateTime GetDDMMYYYYAsDateTime(string pisValue)
        {
            string sDay, sMonth, sYear;

            if (pisValue.Length != 10)
            {
                throw new Exception("La cadena ingresada [" + pisValue + "] no tiene el formato correcto DD/MM/YYYY");
            }

            sDay = pisValue.Substring(0, 2);
            sMonth = pisValue.Substring(3, 2);
            sYear = pisValue.Substring(6, 4);

            DateTime dtValue = new DateTime(int.Parse(sYear), int.Parse(sMonth), int.Parse(sDay));

            return (dtValue);
        }

        // Recibe un DateTime con valores en hora, minutos y segundos y lo trunca a 00:00:00
        public static DateTime TruncDateTime(DateTime pidtValue)
        {
            int iDay, iMonth, iYear;

            iDay = pidtValue.Day;
            iMonth = pidtValue.Month;
            iYear = pidtValue.Year;

            DateTime dtValue = new DateTime(iYear, iMonth, iDay);

            return (dtValue);

        }

        public static string FormatDateTimeAsYYYYMMDD(DateTime pidtValue)
        {
            string sYYYYMMDD, sDay, sMonth, sYear;

            sDay = Convert.ToString(pidtValue.Day);
            sDay = sDay.PadLeft(2, Convert.ToChar("0"));

            sMonth = Convert.ToString(pidtValue.Month);
            sMonth = sMonth.PadLeft(2, Convert.ToChar("0"));

            sYear = Convert.ToString(pidtValue.Year);

            sYYYYMMDD = sYear + sMonth + sDay;

            return (sYYYYMMDD);
        }

        public static string GetFormatMMSS(Int64 cantidad, string tipo)
        {
            string result;
            if (tipo.ToUpper().IndexOf("LLAMADA") != -1)
            {
                if (cantidad > 60)
                {
                    Int64 intMinutos = Convert.ToInt64(CheckDbl(cantidad / 60, 2));
                    Int64 intSegundos = Convert.ToInt64(cantidad % 60);
                    result = String.Format("{0}:{1}", intMinutos.ToString("00"), intSegundos.ToString("00"));
                }
                else
                {
                    result = String.Format("00:{0}", cantidad.ToString("00"));
                }
            }
            else
            {
                result = cantidad.ToString();
            }
            return result;
        }

        public static string GetFormatMMSS(double cantidad, string tipo, bool isPrepago)
        {
            string result;
            if (tipo.ToUpper().IndexOf("LLAMADA") != -1 || tipo.ToUpper().IndexOf("MOC") != -1)
            {
                //Para DBPre, como vienen en minutos lo pasamos a segundos
                cantidad = CheckDbl(cantidad, 2) * 60;
                //Para ODSPre, dejamos como viene pues llega en segundos
                //cantidad = cantidad;
                if (cantidad > 60)
                {
                    Int64 intMinutos = Convert.ToInt64(CheckDbl(cantidad / 60, 2));
                    Int64 intSegundos = Convert.ToInt64(cantidad % 60);
                    result = String.Format("{0}:{1}", intMinutos.ToString("00"), intSegundos.ToString("00"));
                }
                else
                {
                    result = String.Format("00:{0}", cantidad.ToString("00"));
                }
            }
            else
            {
                result = cantidad.ToString();
            }
            return result;
        }

        public static string GetFormatMMSS(Int64 cantidad)
        {
            string result;
            if (cantidad > 60)
            {
                Int64 intMinutos = Convert.ToInt64(CheckDbl(cantidad / 60, 2));
                Int64 intSegundos = Convert.ToInt64(cantidad % 60);
                result = String.Format("{0}:{1}", intMinutos.ToString("00"), intSegundos.ToString("00"));
            }
            else
            {
                result = String.Format("00:{0}", cantidad.ToString("00"));
            }
            return result;
        }

        public static string GetFormatHHMMSS(double cantidad, string tipo, bool isPrepago)
        {
            string result;
            if (isPrepago == true)
            {
                if (tipo.ToUpper().IndexOf("LLAMADA") != -1 || tipo.ToUpper().IndexOf("MOC") != -1)
                {
                    //Para DBPre, como vienen en minutos lo pasamos a segundos
                    cantidad = CheckDbl(cantidad, 2) * 60;
                    //Para ODSPre, dejamos como viene pues llega en segundos
                    //cantidad = cantidad;
                    result = GetFormatHHMMSS(CheckInt64(cantidad));
                }
                else
                {
                    result = cantidad.ToString();
                }
            }
            else
            {
                if (tipo.ToUpper().IndexOf("LLAMADA") != -1)
                {
                    result = GetFormatHHMMSS(CheckInt64(CheckDbl(cantidad, 2)));
                }
                else
                {
                    result = cantidad.ToString();
                }
            }
            return result;
        }

        public static string GetFormatHHMMSS(Int64 cantidad)
        {
            string result;
            Int64 intHoras = 0;
            Int64 intSegundos = 0;
            if (cantidad >= 3600)
            {
                intHoras = Convert.ToInt64(CheckDbl(cantidad / 3600, 2));
                intSegundos = Convert.ToInt64(cantidad % 3600);
                result = GetFormatMMSS(CheckInt64(intSegundos));
            }
            else
            {
                result = GetFormatMMSS(CheckInt64(cantidad));
            }
            result = intHoras.ToString("00") + ":" + result;
            return result;
        }

        public static string GetFormatHHMMSS24AsHHMMSSAMPM(string isHora)
        {
            string sRetorno = string.Empty;
            string[] arrDatos = isHora.Split(Convert.ToChar(":"));
            if (arrDatos.Length > 1)
            {
                if (arrDatos.Length > 2)
                {
                    Int64 iHoras = CheckInt64(CheckDbl(arrDatos[0], 2));
                    Int64 iMinutos = CheckInt64(CheckDbl(arrDatos[1], 2));
                    Int64 iSegundos = CheckInt64(CheckDbl(arrDatos[2], 2));
                    if (iHoras > 12)
                    {
                        iHoras = iHoras - 12;
                        sRetorno = String.Format("{0}:{1}:{2} p.m.", iHoras.ToString("00"), iMinutos.ToString("00"), iSegundos.ToString("00"));
                    }
                    else
                    {
                        sRetorno = String.Format("{0}:{1}:{2} a.m.", iHoras.ToString("00"), iMinutos.ToString("00"), iSegundos.ToString("00"));
                    }
                }
                else
                {
                    sRetorno = GetFormatMMSSAsHHMMSS(isHora);
                }
            }
            else
            {
                sRetorno = isHora;
            }
            return sRetorno;
        }

        public static string GetFormatMMSSAsHHMMSS(string isHora)
        {
            string sRetorno = string.Empty;
            string[] arrDatos = isHora.Split(Convert.ToChar(":"));
            if (arrDatos.Length > 1)
            {
                if (arrDatos.Length > 2)
                {
                    sRetorno = isHora;
                }
                else
                {
                    Int64 iMinutos = CheckInt64(CheckDbl(arrDatos[0], 2));
                    Int64 iSegundos = CheckInt64(CheckDbl(arrDatos[1], 2));
                    if (iMinutos >= 60)
                    {
                        Int64 iHoras = Convert.ToInt64(CheckDbl(iMinutos / 60, 2));
                        iMinutos = Convert.ToInt64(iMinutos % 60);
                        sRetorno = String.Format("{0}:{1}:{2}", iHoras.ToString("00"), iMinutos.ToString("00"), iSegundos.ToString("00"));
                    }
                    else
                    {
                        sRetorno = "00:" + String.Format("{0}:{1}", iMinutos.ToString("00"), iSegundos.ToString("00"));
                    }
                }
            }
            else
            {
                sRetorno = isHora;
            }
            return sRetorno;
        }

        static public void RegistrarSuceso(string istrTextoSuceso, System.Diagnostics.EventLogEntryType iobjTipoSuceso)
        {
            System.Diagnostics.EventLog objMiLog = new System.Diagnostics.EventLog();
            objMiLog.Source = ".NET Runtime"; //Por defecto grabará con este origen de suceso
            objMiLog.WriteEntry(istrTextoSuceso, iobjTipoSuceso);
        }

        static public void RegistrarError(System.Exception ex, string iNombreAssembly, string iNombreMetodo)
        {
            string strTextoError = string.Empty;
            strTextoError = "\n\rError en el assembly:" + iNombreAssembly + "\n\rMétodo:" + iNombreMetodo + "\n\rTexto del error:" + ex.Message + "\n\rTraza del error: " + ex.StackTrace;
            System.Diagnostics.EventLog objMiLog = new System.Diagnostics.EventLog();
            objMiLog.Source = ".NET Runtime"; //Por defecto grabará con este origen de suceso
            objMiLog.WriteEntry(ex.StackTrace, System.Diagnostics.EventLogEntryType.Error);
        }

        public static string SeguridadFormatTelf(string pTelefono)
        {
            string sTelefono;
            string sNumeroFormat = System.Configuration.ConfigurationSettings.AppSettings["gConstNroDigSeguridadTelefono"];
            int iNumero = System.Int32.Parse(sNumeroFormat);

            sTelefono = pTelefono.Substring(0, pTelefono.Length - iNumero);
            sTelefono = sTelefono + "XXXXXXXXXXX".Substring(0, iNumero);
            return (sTelefono);
        }

        public static string extraerConfiguracion(string key)
        {
            string result = String.Empty;
            if (String.IsNullOrEmpty(key))
                return result;

            result = System.Configuration.ConfigurationSettings.AppSettings[key];
            return result;
        }

    }
}
