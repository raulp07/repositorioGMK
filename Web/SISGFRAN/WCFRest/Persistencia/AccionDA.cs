using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WCFRest.Dominio;

namespace WCFRest.Persistencia
{
    public class AccionDA
    {
        private static AccionDA accion;
        private AccionDA() { }
        public static AccionDA Accion
        {
            get
            {
                if (accion == null)
                {
                    accion = new AccionDA();
                }
                return accion;
            }
        }

        public List<AccionEL> GetAllAccion(AccionEL DE)
        {
            using (SqlConnection con = new SqlConnection(ConexionUtil.Cadena))
            {
                con.Open();
                using (SqlCommand com = new SqlCommand("spGettb_accionAll", con))
                {
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Add("@codAccion", SqlDbType.Int).Value = DE.codAccion;
                    com.Parameters.Add("@codEstrategia", SqlDbType.Int).Value = DE.codEstrategia;
                    List<AccionEL> lst = new List<AccionEL>();
                    using (IDataReader dataReader = com.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            AccionEL obj = new AccionEL();
                            if (dataReader["codAccion"] != DBNull.Value) { obj.codAccion = (int)dataReader["codAccion"]; }
                            if (dataReader["nombreAccion"] != DBNull.Value) { obj.nombreAccion = (string)dataReader["nombreAccion"]; }
                            if (dataReader["descripcionAccion"] != DBNull.Value) { obj.descripcionAccion = (string)dataReader["descripcionAccion"]; }
                            if (dataReader["fechaRegistroAccion"] != DBNull.Value) { obj.fechaRegistroAccion = (DateTime)dataReader["fechaRegistroAccion"]; }
                            if (dataReader["costoAccion"] != DBNull.Value) { obj.costoAccion = (decimal)dataReader["costoAccion"]; }
                            if (dataReader["fechaInicioAccion"] != DBNull.Value) { obj.fechaInicioAccion = (DateTime)dataReader["fechaInicioAccion"]; }
                            if (dataReader["codEstrategia"] != DBNull.Value) { obj.codEstrategia = (int)dataReader["codEstrategia"]; }
                            if (dataReader["fechaFinAccion"] != DBNull.Value) { obj.fechaFinAccion = (DateTime)dataReader["fechaFinAccion"]; }
                            if (dataReader["fechaInicioRealAccion"] != DBNull.Value) { obj.fechaInicioRealAccion = (DateTime)dataReader["fechaInicioRealAccion"]; }
                            if (dataReader["fechaFinRealAccion"] != DBNull.Value) { obj.fechaFinRealAccion = (DateTime)dataReader["fechaFinRealAccion"]; }
                            if (dataReader["estadoAccion"] != DBNull.Value) { obj.estadoAccion = (string)dataReader["estadoAccion"]; }

                            lst.Add(obj);
                        }
                        return lst;
                    }
                }
            }
        }
    }
}