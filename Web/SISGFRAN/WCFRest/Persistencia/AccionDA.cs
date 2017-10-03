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

        public int ActualizacionAccion(AccionEL DE)
        {
            using (SqlConnection con = new SqlConnection(ConexionUtil.Cadena))
            {
                con.Open();
                using (SqlCommand com = new SqlCommand("spUpdatetb_accion", con))
                {
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Add("@codAccion", SqlDbType.Int).Value = DE.codAccion;
                    com.Parameters.Add("@nombreAccion", SqlDbType.VarChar).Value = DE.nombreAccion;
                    com.Parameters.Add("@descripcionAccion", SqlDbType.VarChar).Value = DE.descripcionAccion;
                    //com.Parameters.Add("@fechaRegistroAccion", SqlDbType.DateTime).Value = (DE.fechaRegistroAccion == DateTime.MinValue ? DateTime.Now : DE.fechaRegistroAccion);
                    com.Parameters.Add("@costoAccion", SqlDbType.Decimal).Value = DE.costoAccion;
                    //com.Parameters.Add("@fechaInicioAccion", SqlDbType.DateTime).Value = (DE.fechaInicioAccion== DateTime.MinValue ? DateTime.Now : DE.fechaInicioAccion);
                    com.Parameters.Add("@codEstrategia", SqlDbType.Int).Value = DE.codEstrategia;
                    //com.Parameters.Add("@fechaFinAccion", SqlDbType.DateTime).Value = (DE.fechaFinAccion == DateTime.MinValue ? DateTime.Now : DE.fechaFinAccion);
                    //com.Parameters.Add("@fechaInicioRealAccion", SqlDbType.DateTime).Value = (DE.fechaInicioRealAccion == DateTime.MinValue ? DateTime.Now : DE.fechaInicioRealAccion);
                    //com.Parameters.Add("@fechaFinRealAccion", SqlDbType.DateTime).Value = (DE.fechaFinRealAccion == DateTime.MinValue ? DateTime.Now : DE.fechaFinRealAccion);
                    com.Parameters.Add("@estadoAccion", SqlDbType.Int).Value = DE.estadoAccion;

                    return com.ExecuteNonQuery();

                }
            }
        }
    }
}