using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WCFRest.Dominio;

namespace WCFRest.Persistencia
{
    public class EstrategiaDA
    {
        private static EstrategiaDA estrategia;
        private EstrategiaDA() { }
        public static EstrategiaDA Estrategia
        {
            get
            {
                if (estrategia == null)
                {
                    estrategia = new EstrategiaDA();
                }
                return estrategia;
            }
        }

        public List<EstrategiaEL> GetAllEstrategia(EstrategiaEL DE)
        {
            using (SqlConnection con = new SqlConnection(ConexionUtil.Cadena))
            {
                con.Open();
                using (SqlCommand com = new SqlCommand("spGettb_estrategiaAll", con))
                {
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Add("@codEstrategia", SqlDbType.Int).Value = DE.codEstrategia;
                    com.Parameters.Add("@codObjetivo", SqlDbType.Int).Value = DE.codObjetivo;
                    List<EstrategiaEL> lst = new List<EstrategiaEL>();
                    using (IDataReader dataReader = com.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            EstrategiaEL obj = new EstrategiaEL();
                            if (dataReader["codEstrategia"] != DBNull.Value) { obj.codEstrategia = (int)dataReader["codEstrategia"]; }
                            if (dataReader["descripcionEstrategia"] != DBNull.Value) { obj.descripcionEstrategia = (string)dataReader["descripcionEstrategia"]; }
                            if (dataReader["fechaCumplimiento"] != DBNull.Value) { obj.fechaCumplimiento = (DateTime)dataReader["fechaCumplimiento"]; }
                            if (dataReader["estadoEstrategia"] != DBNull.Value) { obj.estadoEstrategia = (string)dataReader["estadoEstrategia"]; }
                            if (dataReader["codObjetivo"] != DBNull.Value) { obj.codObjetivo = (int)dataReader["codObjetivo"]; }

                            lst.Add(obj);
                        }
                        return lst;
                    }
                }
            }
        }

    }
}