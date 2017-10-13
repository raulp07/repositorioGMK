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

        private static PropuestaIndicadorBL propuestaIndicador;
        private PropuestaIndicadorBL() { }
        public static PropuestaIndicadorBL PropuestaIndicador
        {
            get
            {
                if (propuestaIndicador == null)
                {
                    propuestaIndicador = new PropuestaIndicadorBL();
                }
                return propuestaIndicador;
            }
        }


        REST conecRest = new REST();
        JavaScriptSerializer js = new JavaScriptSerializer();
        public List<PlanMarketingEL> GetAllPlanMarketing()
        {
            List<PlanMarketingEL> PlanMarketing = js.Deserialize<List<PlanMarketingEL>>(conecRest.ConectREST("planmarketing", "GET"));
            return PlanMarketing;


        }

        public List<ComboProductoEL> GetAllComboProducto()
        {
            try
            {
                List<ComboProductoEL> ComboProducto = js.Deserialize<List<ComboProductoEL>>(conecRest.ConectREST("comboproducto", "GET"));
                return ComboProducto;
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }

        }
        public List<LocalEL> ListaLocales()
        {
            try
            {
                List<LocalEL> Local = js.Deserialize<List<LocalEL>>(conecRest.ConectREST("locales", "GET"));
                return Local;
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }

        }

        public List<CalcularPropuestaxIndicadorEL> CalcularPropuestaxIndicadores(CalcularPropuestaxIndicadorEL CalcularPropuestaxIndicador)
        {
            try
            {
                string postdata = js.Serialize(CalcularPropuestaxIndicador);
                List<CalcularPropuestaxIndicadorEL> listCalcularPropuestaxIndicador = js.Deserialize<List<CalcularPropuestaxIndicadorEL>>(conecRest.ConectREST("PropuestaxIndicadores", "POST", postdata));
                return listCalcularPropuestaxIndicador;
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }

        }

        public List<ComboEL> GetAllCombo(ComboEL Combo)
        {
            try
            {
                string postdata = js.Serialize(Combo);
                List<ComboEL> _Combo = js.Deserialize<List<ComboEL>>(conecRest.ConectREST("Combo", "POST", postdata));
                return _Combo;
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }

        }

        public int proyectarPropuestaxIndicadores(proyectarPropuestaIndicadorEL proyectarPropuestaIndicador)
        {
            try
            {
                string postdata = js.Serialize(proyectarPropuestaIndicador);
                int respuesta = js.Deserialize<int>(conecRest.ConectREST("ProyectarPropuestaIndicador", "POST", postdata));
                return respuesta;
            }
            catch (Exception e)
            {
                throw new ArgumentException(e.Message);
            }
        }

    }
}
