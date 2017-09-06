using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WCFRest.Dominio;

namespace WCFRest.Persistencia
{
    public class PropuestaxIndicadorDA
    {
        private static PropuestaxIndicadorDA propuestaxIndicador;
        private PropuestaxIndicadorDA() { }

        public static PropuestaxIndicadorDA PropuestaxIndicador {
            get {
                if (propuestaxIndicador == null)
                {
                    propuestaxIndicador = new PropuestaxIndicadorDA();
                }
                return propuestaxIndicador;
            }
        }

        public List<CalcularPropuestaxIndicadorEL> CalcularPropuestaxIndicadores(CalcularPropuestaxIndicadorEL CalcularPropuestaxIndicador)
        {
            using (SqlConnection con = new SqlConnection(ConexionUtil.Cadena))
            {
                con.Open();
                using (SqlCommand com = new SqlCommand("CalcularPropuestaxIndicadores", con))
                {
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Add("@listLocal", SqlDbType.VarChar).Value = CalcularPropuestaxIndicador.listLocal;
                    com.Parameters.Add("@codCombo", SqlDbType.Int).Value = CalcularPropuestaxIndicador.codCombo;
                    com.Parameters.Add("@indConsumo", SqlDbType.Int).Value = CalcularPropuestaxIndicador.indConsumo;
                    com.Parameters.Add("@indSabor", SqlDbType.Int).Value = CalcularPropuestaxIndicador.indSabor;
                    com.Parameters.Add("@indCosto", SqlDbType.Int).Value = CalcularPropuestaxIndicador.indCosto;
                    com.Parameters.Add("@cantPuntuacionMax", SqlDbType.Int).Value = (CalcularPropuestaxIndicador.cantPuntuacionMax == 0 ? 10 : CalcularPropuestaxIndicador.cantPuntuacionMax);
                    List<CalcularPropuestaxIndicadorEL> lst = new List<CalcularPropuestaxIndicadorEL>();
                    using (IDataReader dataReader = com.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            CalcularPropuestaxIndicadorEL obj = new CalcularPropuestaxIndicadorEL();
                            //if (dataReader["listLocal"] != DBNull.Value) { obj.listLocal = (string)dataReader["listLocal"]; }
                            if (dataReader["codLocal"] != DBNull.Value) { obj.codLocal = (int)dataReader["codLocal"]; }
                            if (dataReader["codCombo"] != DBNull.Value) { obj.codCombo = (int)dataReader["codCombo"]; }
                            if (dataReader["indConsumo"] != DBNull.Value) { obj.indConsumo = (int)dataReader["indConsumo"]; }
                            if (dataReader["indSabor"] != DBNull.Value) { obj.indSabor = (int)dataReader["indSabor"]; }
                            if (dataReader["indCosto"] != DBNull.Value) { obj.indCosto = (int)dataReader["indCosto"]; }
                            //if (dataReader["cantPuntuacionMax"] != DBNull.Value) { obj.cantPuntuacionMax = (int)dataReader["cantPuntuacionMax"]; }
                            if (dataReader["cantProyeccionVenta"] != DBNull.Value) { obj.cantProyeccionVenta = (int)dataReader["cantProyeccionVenta"]; }
                            if (dataReader["nombreCaractComboVenta"] != DBNull.Value) { obj.nombreCaractComboVenta = (string)dataReader["nombreCaractComboVenta"]; }
                            if (dataReader["impProyeccionCosto"] != DBNull.Value) { obj.impProyeccionCosto = (decimal)dataReader["impProyeccionCosto"]; }

                            lst.Add(obj);
                        }
                        return lst;
                    }
                }
            }
        }

        public int proyectarPropuestaxIndicadores(proyectarPropuestaIndicadorEL proyectarPropuestaIndicador)
        {
            using (SqlConnection con = new SqlConnection(ConexionUtil.Cadena))
            {
                con.Open();
                using (SqlCommand com = new SqlCommand("spInserttb_proyectarPropuestaIndicador", con))
                {
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.Add("@indicadorConsumo", SqlDbType.Int).Value = proyectarPropuestaIndicador.indicadorConsumo;
                    com.Parameters.Add("@indicadorSabor", SqlDbType.Int).Value = proyectarPropuestaIndicador.indicadorSabor;
                    com.Parameters.Add("@indicadorCosto", SqlDbType.Int).Value = proyectarPropuestaIndicador.indicadorCosto;
                    com.Parameters.Add("@fechaRegistroIndicador", SqlDbType.DateTime).Value =DateTime.Now;
                    com.Parameters.Add("@codCombo", SqlDbType.Int).Value = proyectarPropuestaIndicador.codLocal;
                    com.Parameters.Add("@codLocal", SqlDbType.Int).Value = proyectarPropuestaIndicador.codLocal;

                    return com.ExecuteNonQuery();
                }
            }
        }

    }
}