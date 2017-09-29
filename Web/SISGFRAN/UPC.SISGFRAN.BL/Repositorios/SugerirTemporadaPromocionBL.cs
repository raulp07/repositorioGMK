using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using UPC.SISGFRAN.EL.Inherited;

namespace UPC.SISGFRAN.BL.Repositorios
{
    public class SugerirTemporadaPromocionBL
    {
        private static SugerirTemporadaPromocionBL sugerirTemporadaPromocion;
        private SugerirTemporadaPromocionBL() { }

        public static SugerirTemporadaPromocionBL SugerirTemporadaPromocion
        {
            get
            {
                if (sugerirTemporadaPromocion== null)
                {
                    sugerirTemporadaPromocion = new SugerirTemporadaPromocionBL();
                }
                return sugerirTemporadaPromocion;
            }
        }
        REST conecRest = new REST();
        JavaScriptSerializer js = new JavaScriptSerializer();
        public List<SugerirTemporadaPromocionEL> CalcularPorcentajexPeriodo(SugerirTemporadaPromocionEL DE)
        {
            string postdata = js.Serialize(DE);
            return js.Deserialize<List<SugerirTemporadaPromocionEL>>(conecRest.ConectREST("CalcularPorcentajexPeriodo", "POST", postdata));
        }
    }
}
