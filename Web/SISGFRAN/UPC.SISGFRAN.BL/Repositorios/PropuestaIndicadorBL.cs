using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using UPC.SISGFRAN.EL.Inherited;

namespace UPC.SISGFRAN.BL.Repositorios
{
    public class PropuestaIndicadorBL
    {
        REST conecRest = new REST();
        JavaScriptSerializer js = new JavaScriptSerializer();
        public List<PlanMarketingEL> GetAllPlanMarketing() {
            List<PlanMarketingEL> PlanMarketing = js.Deserialize<List<PlanMarketingEL>>(conecRest.ConectREST("planmarketing", "GET"));
            return PlanMarketing;
        }

        public List<ComboProductoEL> GetAllComboProducto()
        {
            List<ComboProductoEL> ComboProducto = js.Deserialize<List<ComboProductoEL>>(conecRest.ConectREST("comboproducto", "GET"));
            return ComboProducto;
        }
        public List<LocalEL> ListaLocales()
        {
            List<LocalEL> Local = js.Deserialize<List<LocalEL>>(conecRest.ConectREST("locales", "GET"));
            return Local;
        }

        public List<CalcularPropuestaxIndicadorEL> CalcularPropuestaxIndicadores(CalcularPropuestaxIndicadorEL CalcularPropuestaxIndicador)
        {
            string postdata = js.Serialize(CalcularPropuestaxIndicador);
            List<CalcularPropuestaxIndicadorEL> listCalcularPropuestaxIndicador = js.Deserialize<List<CalcularPropuestaxIndicadorEL>>(conecRest.ConectREST("locales", "POST", postdata));
            return listCalcularPropuestaxIndicador;
        }

        public List<ComboEL> GetAllCombo(ComboEL Combo)
        {
            string postdata = js.Serialize(Combo);
            List<ComboEL> _Combo = js.Deserialize<List<ComboEL>>(conecRest.ConectREST("Combo", "POST", postdata));
            return _Combo;
        }
    }
}
