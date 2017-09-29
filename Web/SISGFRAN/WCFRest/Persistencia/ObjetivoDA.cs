using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WCFRest.Dominio;

namespace WCFRest.Persistencia
{
    public class ObjetivoDA
    {
        private static ObjetivoDA objetivo;
        private ObjetivoDA() { }
        public static ObjetivoDA Objetivo
        {
            get
            {
                if (objetivo == null)
                {
                    objetivo = new ObjetivoDA();
                }
                return objetivo;
            }
        }

        public List<ObjetivoEL> GetAllObjetivo(ObjetivoEL DE)
        {
            using (SqlConnection con = new SqlConnection(ConexionUtil.Cadena))
            {
                con.Open();
                using (SqlCommand com = new SqlCommand("spGettb_ObjetivoAll", con))
                {
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Add("@idPlanMkt", SqlDbType.Int).Value = DE.idPlanMkt;
                    List<ObjetivoEL> lst = new List<ObjetivoEL>();
                    using (IDataReader dataReader = com.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            ObjetivoEL obj = new ObjetivoEL();
                            if (dataReader["codObjetivo"] != DBNull.Value) { obj.codObjetivo = (int)dataReader["codObjetivo"]; }
                            if (dataReader["nombreObjetivo"] != DBNull.Value) { obj.nombreObjetivo = (string)dataReader["nombreObjetivo"]; }
                            if (dataReader["estadoObjetivo"] != DBNull.Value) { obj.estadoObjetivo = (string)dataReader["estadoObjetivo"]; }
                            if (dataReader["idPlanMkt"] != DBNull.Value) { obj.idPlanMkt = (int)dataReader["idPlanMkt"]; }

                            lst.Add(obj);
                        }
                        return lst;
                    }
                }
            }
        }
    }
}