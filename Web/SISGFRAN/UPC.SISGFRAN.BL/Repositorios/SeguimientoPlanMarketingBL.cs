using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using UPC.SISGFRAN.EL.Inherited;

namespace UPC.SISGFRAN.BL.Repositorios
{


    public class SeguimientoPlanMarketingBL
    {
        private static SeguimientoPlanMarketingBL seguimientoPlanMarketing;
        private SeguimientoPlanMarketingBL() { }

        public static SeguimientoPlanMarketingBL SeguimientoPlanMarketing {
            get
            {
                if (seguimientoPlanMarketing == null)
                {
                    seguimientoPlanMarketing = new SeguimientoPlanMarketingBL();
                }
                return seguimientoPlanMarketing;
            }
        }
        REST conecRest = new REST();
        JavaScriptSerializer js = new JavaScriptSerializer();
        public List<ObjetivoEL> GetAllObjetivo(ObjetivoEL DE)
        {
            string postdata = js.Serialize(DE);
            List<ObjetivoEL> Objetivo = js.Deserialize<List<ObjetivoEL>>(conecRest.ConectREST("objetivo", "POST", postdata));
            return Objetivo;
        }

        public List<EstrategiaEL> GetAllEstrategia(EstrategiaEL DE)
        {
            string postdata = js.Serialize(DE);
            List<EstrategiaEL> List = js.Deserialize<List<EstrategiaEL>>(conecRest.ConectREST("Estrategia", "POST", postdata));
            return List;
        }

        public List<AccionEL> GetAllAccion(AccionEL DE)
        {
            string postdata = js.Serialize(DE);
            List<AccionEL> List = js.Deserialize<List<AccionEL>>(conecRest.ConectREST("Accion", "POST", postdata));
            return List;
        }
    }
}
