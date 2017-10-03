using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WCFRest.Dominio;

namespace WCFRest.Persistencia
{
    public class PlanMarketingDA
    {
        private static PlanMarketingDA planMarketing;
        private PlanMarketingDA() { }
        public static PlanMarketingDA PlanMarketing {
            get {
                if (planMarketing == null)
                {
                    planMarketing = new PlanMarketingDA();
                }
                return planMarketing;
            }
        }
        public List<PlanMarketingEL> GetAllPlanMarketing()
        {
            using (SqlConnection con = new SqlConnection(ConexionUtil.Cadena))
            {
                con.Open();
                using (SqlCommand com = new SqlCommand("spGettb_planMarketingAll", con))
                {
                    com.CommandType = CommandType.StoredProcedure;
                    List<PlanMarketingEL> lstPlanMarketing = new List<PlanMarketingEL>();
                    using (IDataReader dataReader = com.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            PlanMarketingEL obj = new PlanMarketingEL();
                            if (dataReader["codPlanMkt"] != DBNull.Value) { obj.codPlanMkt = (int)dataReader["codPlanMkt"]; }
                            if (dataReader["nombrePlanMkt"] != DBNull.Value) { obj.nombrePlanMkt = (string)dataReader["nombrePlanMkt"]; }
                            if (dataReader["presupuestoPlanMkt"] != DBNull.Value) { obj.presupuestoPlanMkt = (decimal)dataReader["presupuestoPlanMkt"]; }
                            if (dataReader["fechaRegistro"] != DBNull.Value) { obj.fechaRegistro = (DateTime)dataReader["fechaRegistro"]; }
                            if (dataReader["fechaInicio"] != DBNull.Value) { obj.fechaInicio = (DateTime)dataReader["fechaInicio"]; }
                            if (dataReader["fechaFin"] != DBNull.Value) { obj.fechaFin = (DateTime)dataReader["fechaFin"]; }
                            if (dataReader["fechaInicioReal"] != DBNull.Value) { obj.fechaInicioReal = (DateTime)dataReader["fechaInicioReal"]; }
                            if (dataReader["fechaFinReal"] != DBNull.Value) { obj.fechaFinReal = (DateTime)dataReader["fechaFinReal"]; }
                            if (dataReader["estadoPlanMkt"] != DBNull.Value) { obj.estadoPlanMkt = (string)dataReader["estadoPlanMkt"]; }
                            if (dataReader["porcentajeavance"] != DBNull.Value) { obj.porcentajeavance = (int)dataReader["porcentajeavance"]; }
                            
                            lstPlanMarketing.Add(obj);
                        }
                        return lstPlanMarketing;
                    }
                }
            }
        }

    }
}