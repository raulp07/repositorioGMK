using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WCFRest.Dominio;

namespace WCFRest.Persistencia
{
    public class ComboDA
    {
        private static ComboDA combo;
        private ComboDA() { }
        public static ComboDA Combo {
            get {
                if (combo == null)
                {
                    combo = new ComboDA();
                }
                return combo;
            }
        }

        public List<ComboEL> GetAllCombo(ComboEL Combo)
        {
            using (SqlConnection con = new SqlConnection(ConexionUtil.Cadena))
            {
                con.Open();
                using (SqlCommand com = new SqlCommand("spGettb_combo", con))
                {
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Add("@codPlanMkt", SqlDbType.Int).Value = Combo.codPlanMkt;
                    List<ComboEL> lst = new List<ComboEL>();
                    using (IDataReader dataReader = com.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            ComboEL obj = new ComboEL();
                            if (dataReader["codCombo"] != DBNull.Value) { obj.codCombo = (long)dataReader["codCombo"]; }
                            if (dataReader["nombre"] != DBNull.Value) { obj.nombre = (string)dataReader["nombre"]; }
                            if (dataReader["descripcion"] != DBNull.Value) { obj.descripcion = (string)dataReader["descripcion"]; }
                            if (dataReader["precio"] != DBNull.Value) { obj.precio = (decimal)dataReader["precio"]; }
                            if (dataReader["descuento"] != DBNull.Value) { obj.descuento = (decimal)dataReader["descuento"]; }
                            if (dataReader["estado"] != DBNull.Value) { obj.estado = (bool)dataReader["estado"]; }
                            if (dataReader["codCategoria"] != DBNull.Value) { obj.codCategoria = (long)dataReader["codCategoria"]; }
                            if (dataReader["fechaCreacion"] != DBNull.Value) { obj.fechaCreacion = (DateTime)dataReader["fechaCreacion"]; }
                            if (dataReader["fechaModificacion"] != DBNull.Value) { obj.fechaModificacion = (DateTime)dataReader["fechaModificacion"]; }

                            lst.Add(obj);
                        }
                        return lst;
                    }
                }
            }
        }
    }
}