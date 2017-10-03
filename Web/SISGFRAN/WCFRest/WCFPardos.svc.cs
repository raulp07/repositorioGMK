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
        WebOperationContext ctx = WebOperationContext.Current;
        public List<LocalEL> GetAllDatoMedio_X_Local()
        {
            string request = ctx.IncomingRequest.Headers["token-client"];
            if (!UDA.Validarusuario(request))
                return null;
            return PPDA.GetAllDatoMedio_X_Local();
        }

        public List<ResultadoEncuestaEL> GetAllDatoMedioPublicitario()
        {
            string request = ctx.IncomingRequest.Headers["token-client"];
            if (!UDA.Validarusuario(request))
                throw new FaultException("No hay tiene autorización");
            return PPDA.GetAllDatoMedioPublicitario();
        }

        public int RegistroPropuesta(PropuestaPublicidadEL PropuestaPublicidad)
        {
            string request = ctx.IncomingRequest.Headers["token-client"];
            if (!UDA.Validarusuario(request))
                return 0;
            return PPDA.RegistroPropuesta(PropuestaPublicidad);
        }
        public int RegistroDetallePropuesta(DetallePropuestaPublicidadEL DetallePropuestaPublicidad)
        {
            string request = ctx.IncomingRequest.Headers["token-client"];
            if (!UDA.Validarusuario(request))
                return 0;
            return PPDA.RegistroDetallePropuesta(DetallePropuestaPublicidad);
        }


        public UsuarioEL Login(UsuarioEL usuario)
        {
            UsuarioEL usuarioEL = UDA.Login(usuario);
            ctx.OutgoingResponse.Headers.Add("token-client", TokensEL.token_Client);
            return usuarioEL;
        }

        public List<UsuarioEL> GetUsuarios(UsuarioEL usuario)
        {
            string request = ctx.IncomingRequest.Headers["token-client"];
            if (!UDA.Validarusuario(request))
                return null;
            return UDA.GetUsuarios(usuario);
        }

        public UsuarioEL GetUsuarioById(int? idUsuario)
        {
            string request = ctx.IncomingRequest.Headers["token-client"];
            if (!UDA.Validarusuario(request))
                return null;
            return UDA.GetUsuarioById(idUsuario);
        }


        public List<PlanMarketingEL> GetAllPlanMarketing()
        {
            string request = ctx.IncomingRequest.Headers["token-client"];
            if (!UDA.Validarusuario(request))
                return null;
            return PlanMarketingDA.PlanMarketing.GetAllPlanMarketing();
        }


        public List<ComboProductoEL> GetAllComboProducto()
        {
            string request = ctx.IncomingRequest.Headers["token-client"];
            if (!UDA.Validarusuario(request))
                return null;
            return ComboProductoDA.ComboProducto.GetAllComboProducto();
        }


        public List<LocalEL> GetAllLocal()
        {
            string request = ctx.IncomingRequest.Headers["token-client"];
            if (!UDA.Validarusuario(request))
                return null;
            return LocalDA.Local.GetAllLocal();
        }


        public List<CalcularPropuestaxIndicadorEL> CalcularPropuestaxIndicadores(CalcularPropuestaxIndicadorEL CalcularPropuestaxIndicador)
        {
            string request = ctx.IncomingRequest.Headers["token-client"];
            if (!UDA.Validarusuario(request))
                return null;
            return PropuestaxIndicadorDA.PropuestaxIndicador.CalcularPropuestaxIndicadores(CalcularPropuestaxIndicador);
        }


        public List<ComboEL> GetAllCombo(ComboEL Combo)
        {
            string request = ctx.IncomingRequest.Headers["token-client"];
            if (!UDA.Validarusuario(request))
                return null;
            return ComboDA.Combo.GetAllCombo(Combo);
        }


        public int proyectarPropuestaxIndicadores(proyectarPropuestaIndicadorEL proyectarPropuestaIndicador)
        {
            string request = ctx.IncomingRequest.Headers["token-client"];
            if (!UDA.Validarusuario(request))
                return 0;
            return PropuestaxIndicadorDA.PropuestaxIndicador.proyectarPropuestaxIndicadores(proyectarPropuestaIndicador);
        }


        public List<OpcionXPerfilEL> ListMenu(OpcionXPerfilEL opcionPerfil)
        {
            string request = ctx.IncomingRequest.Headers["token-client"];
            if (!UDA.Validarusuario(request))
                return null;
            return OpcionXPerfilDA.OpcionXPerfil.ListMenu(opcionPerfil);
        }
        public List<ObjetivoEL> GetAllObjetivo(ObjetivoEL DE)
        {
            string request = ctx.IncomingRequest.Headers["token-client"];
            if (!UDA.Validarusuario(request))
                return null;
            return ObjetivoDA.Objetivo.GetAllObjetivo(DE);
        }
        public List<SugerirTemporadaPromocionEL> GetCalcularPorcentajexPeriodo(SugerirTemporadaPromocionEL DE)
        {
            string request = ctx.IncomingRequest.Headers["token-client"];
            if (!UDA.Validarusuario(request))
                return null;
            return SugerirTemporadaPromocionDA.SugerirTemporadaPromocion.GetCalcularPorcentajexPeriodo(DE);
        }

        public List<EstrategiaEL> GetAllEstrategia(EstrategiaEL DE)
        {
            string request = ctx.IncomingRequest.Headers["token-client"];
            if (!UDA.Validarusuario(request))
                return null;
            return EstrategiaDA.Estrategia.GetAllEstrategia(DE);
        }
        public List<AccionEL> GetAllAccion(AccionEL DE)
        {
            string request = ctx.IncomingRequest.Headers["token-client"];
            if (!UDA.Validarusuario(request))
                return null;
            return AccionDA.Accion.GetAllAccion(DE);
        }

        public int ActualizacionAccion(AccionEL DE)
        {
            string request = ctx.IncomingRequest.Headers["token-client"];
            if (!UDA.Validarusuario(request))
                return 0;
            return AccionDA.Accion.ActualizacionAccion(DE);
        }

    }
}
