using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WCFRest.Dominio;

namespace WCFRest.Persistencia
{
    public class SugerirTemporadaPromocionDA
    {
        private static SugerirTemporadaPromocionDA sugerirTemporadaPromocion;
        private SugerirTemporadaPromocionDA() { }

        public static SugerirTemporadaPromocionDA SugerirTemporadaPromocion {
            get {
                if (sugerirTemporadaPromocion == null)
                {
                    sugerirTemporadaPromocion = new SugerirTemporadaPromocionDA();
                }
                return sugerirTemporadaPromocion;
            }
        }


        public List<SugerirTemporadaPromocionEL> GetCalcularPorcentajexPeriodo(SugerirTemporadaPromocionEL DE)
        {
            using (SqlConnection con = new SqlConnection(ConexionUtil.Cadena))
            {
                con.Open();
                using (SqlCommand com = new SqlCommand("CalcularPorcentajexPeriodo", con))
                {
                    com.Parameters.Add("@codCombo", SqlDbType.Int).Value = DE.codCombo;
                    com.Parameters.Add("@periodoInicial", SqlDbType.Int).Value = DE.periodoini;
                    com.Parameters.Add("@anioInicial", SqlDbType.Int).Value = DE.anioini;
                    com.Parameters.Add("@periodoFinal", SqlDbType.Int).Value = DE.periodofin;
                    com.Parameters.Add("@anioFinal", SqlDbType.Int).Value = DE.aniofin;

                    com.CommandType = CommandType.StoredProcedure;
                    List<SugerirTemporadaPromocionEL> lstLocal = new List<SugerirTemporadaPromocionEL>();
                    using (IDataReader dataReader = com.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            SugerirTemporadaPromocionEL obj = new SugerirTemporadaPromocionEL();
                            if (dataReader["codLocal"] != DBNull.Value) { obj.codLocal = (int)dataReader["codLocal"]; }
                            if (dataReader["NombreLocal"] != DBNull.Value) { obj.NombreLocal = (string)dataReader["NombreLocal"]; }
                            if (dataReader["codCombo"] != DBNull.Value) { obj.codCombo = Convert.ToInt32(dataReader["codCombo"]); }
                            if (dataReader["NombreCombo"] != DBNull.Value) { obj.NombreCombo = (string)dataReader["NombreCombo"]; }
                            if (dataReader["periodo"] != DBNull.Value) { obj.periodo = (int)dataReader["periodo"]; }
                            if (dataReader["anioVenta"] != DBNull.Value) { obj.anioVenta = (int)dataReader["anioVenta"]; }
                            if (dataReader["porVentaxPeridoxAnio"] != DBNull.Value) { obj.porVentaxPeridoxAnio = (int)dataReader["porVentaxPeridoxAnio"]; }
                            lstLocal.Add(obj);
                        }
                        return lstLocal;
                    }
                }
            }
        }

        public int InsertSeguerirTemporadaPromocion(SugerirTemporadaPromocionEL DE)
        {
            using (SqlConnection con = new SqlConnection(ConexionUtil.Cadena))
            {
                con.Open();
                using (SqlCommand com = new SqlCommand("spInsertseguerir_presupuesto", con))
                {
                    com.Parameters.Add("@observacion", SqlDbType.VarChar).Value = DE.NombreLocal;
                    com.Parameters.Add("@codCombo", SqlDbType.Int).Value = DE.codCombo;
                    com.Parameters.Add("@periodo", SqlDbType.Int).Value = DE.periodo;
                    com.Parameters.Add("@anio", SqlDbType.Int).Value = DE.anioini;

                    com.CommandType = CommandType.StoredProcedure;
                    return com.ExecuteNonQuery();
                   
                }
            }
        }

    }
}