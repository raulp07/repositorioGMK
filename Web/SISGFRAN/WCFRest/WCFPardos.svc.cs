using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WCFRest.Dominio;
using WCFRest.Persistencia;

namespace WCFRest
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class WCFPardos : IWCFPardos
    {

        private PropuestaPublicitariaDA PPDA = new PropuestaPublicitariaDA();
        private UsuarioDA UDA = new UsuarioDA();
        public List<LocalEL> GetAllDatoMedio_X_Local()
        {

            return PPDA.GetAllDatoMedio_X_Local();
        }

        public List<ResultadoEncuestaEL> GetAllDatoMedioPublicitario()
        {
            return PPDA.GetAllDatoMedioPublicitario();
        }

        public int RegistroPropuesta(PropuestaPublicidadEL PropuestaPublicidad)
        {
            return PPDA.RegistroPropuesta(PropuestaPublicidad);
        }
        public int RegistroDetallePropuesta(DetallePropuestaPublicidadEL DetallePropuestaPublicidad)
        {
            return PPDA.RegistroDetallePropuesta(DetallePropuestaPublicidad);
        }


        public UsuarioEL Login(UsuarioEL usuario)
        {
            return UDA.Login(usuario);
        }

        public List<UsuarioEL> GetUsuarios(UsuarioEL usuario)
        {
            return new UsuarioDA().GetUsuarios(usuario);
        }

        public UsuarioEL GetUsuarioById(int? idUsuario)
        {
            return new UsuarioDA().GetUsuarioById(idUsuario);
        }


        public List<PlanMarketingEL> GetAllPlanMarketing()
        {
            return new PlanMarketingDA().GetAllPlanMarketing();
        }


        public List<ComboProductoEL> GetAllComboProducto()
        {
            return new ComboProductoDA().GetAllComboProducto();
        }


        public List<LocalEL> GetAllLocal()
        {
            return new LocalDA().GetAllLocal();
        }


        public List<CalcularPropuestaxIndicadorEL> CalcularPropuestaxIndicadores(CalcularPropuestaxIndicadorEL CalcularPropuestaxIndicador)
        {
            return new PropuestaxIndicadorDA().CalcularPropuestaxIndicadores(CalcularPropuestaxIndicador);
        }


        public List<ComboEL> GetAllCombo(ComboEL Combo)
        {
            return new ComboDA().GetAllCombo(Combo);
        }
    }
}
