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

        private PropuestaPublicitariaDA PPDA = PropuestaPublicitariaDA.PropuestaPublicitaria;
        private UsuarioDA UDA = UsuarioDA.Usuario;
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
            return UDA.GetUsuarios(usuario);
        }

        public UsuarioEL GetUsuarioById(int? idUsuario)
        {
            return UDA.GetUsuarioById(idUsuario);
        }


        public List<PlanMarketingEL> GetAllPlanMarketing()
        {
            return PlanMarketingDA.PlanMarketing.GetAllPlanMarketing();
        }


        public List<ComboProductoEL> GetAllComboProducto()
        {
            return ComboProductoDA.ComboProducto.GetAllComboProducto();
        }


        public List<LocalEL> GetAllLocal()
        {
            return LocalDA.Local.GetAllLocal();
        }


        public List<CalcularPropuestaxIndicadorEL> CalcularPropuestaxIndicadores(CalcularPropuestaxIndicadorEL CalcularPropuestaxIndicador)
        {
            return PropuestaxIndicadorDA.PropuestaxIndicador.CalcularPropuestaxIndicadores(CalcularPropuestaxIndicador);
        }


        public List<ComboEL> GetAllCombo(ComboEL Combo)
        {
            return ComboDA.Combo.GetAllCombo(Combo);
        }


        public int proyectarPropuestaxIndicadores(proyectarPropuestaIndicadorEL proyectarPropuestaIndicador)
        {
            return PropuestaxIndicadorDA.PropuestaxIndicador.proyectarPropuestaxIndicadores(proyectarPropuestaIndicador);
        }


        public List<OpcionXPerfilEL> ListMenu(OpcionXPerfilEL opcionPerfil)
        {
            return OpcionXPerfilDA.OpcionXPerfil.ListMenu(opcionPerfil);
        }
    }
}
