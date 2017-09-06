using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WCFRest.Dominio;

namespace WCFRest.Persistencia
{
    public class LocalDA
    {
        private static LocalDA local;
        private LocalDA() { }
        public static LocalDA Local {
            get {
                if (local == null)
                {
                    local = new LocalDA();
                }
                return local;
            }
        }
        public List<LocalEL> GetAllLocal()
        {
            using (SqlConnection con = new SqlConnection(ConexionUtil.Cadena))
            {
                con.Open();
                using (SqlCommand com = new SqlCommand("spGettb_localAll", con))
                {
                    com.CommandType = CommandType.StoredProcedure;
                    List<LocalEL> Local = new List<LocalEL>();
                    using (IDataReader dataReader = com.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            LocalEL obj = new LocalEL();
                            if (dataReader["id"] != DBNull.Value) { obj.id = (int)dataReader["id"]; }
                            if (dataReader["franquiciaId"] != DBNull.Value) { obj.franquiciaId = (int)dataReader["franquiciaId"]; }
                            if (dataReader["nombre"] != DBNull.Value) { obj.nombre = (string)dataReader["nombre"]; }
                            if (dataReader["fechaApertura"] != DBNull.Value) { obj.fechaApertura = (DateTime)dataReader["fechaApertura"]; }
                            if (dataReader["responsable"] != DBNull.Value) { obj.responsable = (string)dataReader["responsable"]; }
                            if (dataReader["distrito"] != DBNull.Value) { obj.distrito = (string)dataReader["distrito"]; }
                            if (dataReader["direccion"] != DBNull.Value) { obj.direccion = (string)dataReader["direccion"]; }
                            if (dataReader["latitud"] != DBNull.Value) { obj.latitud = (string)dataReader["latitud"]; }
                            if (dataReader["longitud"] != DBNull.Value) { obj.longitud = (string)dataReader["longitud"]; }
                            if (dataReader["auditoriaUC"] != DBNull.Value) { obj.auditoriaUC = (int)dataReader["auditoriaUC"]; }
                            if (dataReader["auditoriaUM"] != DBNull.Value) { obj.auditoriaUM = (int)dataReader["auditoriaUM"]; }
                            if (dataReader["auditoriaFC"] != DBNull.Value) { obj.auditoriaFC = (DateTime)dataReader["auditoriaFC"]; }
                            if (dataReader["auditoriaFM"] != DBNull.Value) { obj.auditoriaFC = (DateTime)dataReader["auditoriaFM"]; }

                            Local.Add(obj);
                        }
                        return Local;
                    }
                }
            }
        }




    }
}