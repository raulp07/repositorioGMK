using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WCFRest.Dominio;

namespace WCFRest.Persistencia
{
    public class ComboProductoDA
    {

        private static ComboProductoDA comboProducto;
        private ComboProductoDA() { }

        public static ComboProductoDA ComboProducto {
            get {
                if (comboProducto ==null)
                {
                    comboProducto = new ComboProductoDA();
                }
                return comboProducto;
            }
        }

        public List<ComboProductoEL> GetAllComboProducto()
        {
            using (SqlConnection con = new SqlConnection(ConexionUtil.Cadena))
            {
                con.Open();
                using (SqlCommand com = new SqlCommand("spGettb_comboProductoAll", con))
                {
                    com.CommandType = CommandType.StoredProcedure;
                    List<ComboProductoEL> lstComboProducto = new List<ComboProductoEL>();
                    using (IDataReader dataReader = com.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            ComboProductoEL obj = new ComboProductoEL();
                            if (dataReader["codCombo"] != DBNull.Value) { obj.codCombo = (long)dataReader["codCombo"]; }
                            if (dataReader["codProducto"] != DBNull.Value) { obj.codProducto = (long)dataReader["codProducto"]; }
                            if (dataReader["cantidad"] != DBNull.Value) { obj.cantidad = (int)dataReader["cantidad"]; }
                            if (dataReader["nombreComboProducto"] != DBNull.Value) { obj.nombreComboProducto = (string)dataReader["nombreComboProducto"]; }

                            lstComboProducto.Add(obj);
                        }
                        return lstComboProducto;
                    }
                }
            }
        }
    }
}