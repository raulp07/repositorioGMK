using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using WCFRest.Dominio;

namespace WCFRest
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IWCFPardos
    {

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "Local", ResponseFormat = WebMessageFormat.Json)]
        List<LocalEL> GetAllDatoMedio_X_Local();


        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "Encuesta", ResponseFormat = WebMessageFormat.Json)]
        List<ResultadoEncuestaEL> GetAllDatoMedioPublicitario();

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "Propuesta", ResponseFormat = WebMessageFormat.Json)]
        int RegistroPropuesta(PropuestaPublicidadEL PropuestaPublicidad);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "DetallePropuesta", ResponseFormat = WebMessageFormat.Json)]
        int RegistroDetallePropuesta(DetallePropuestaPublicidadEL DetallePropuestaPublicidad);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "Login", ResponseFormat = WebMessageFormat.Json)]
        UsuarioEL Login(UsuarioEL usuario);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "Usuarios", ResponseFormat = WebMessageFormat.Json)]
        List<UsuarioEL> GetUsuarios(UsuarioEL usuario);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "Usuario", ResponseFormat = WebMessageFormat.Json)]
        UsuarioEL GetUsuarioById(int? idUsuario);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "ListaMenu", ResponseFormat = WebMessageFormat.Json)]
        List<OpcionXPerfilEL> ListMenu(OpcionXPerfilEL opcionPerfil);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "planmarketing", ResponseFormat = WebMessageFormat.Json)]
        List<PlanMarketingEL> GetAllPlanMarketing();

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "comboproducto", ResponseFormat = WebMessageFormat.Json)]
        List<ComboProductoEL> GetAllComboProducto();

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "locales", ResponseFormat = WebMessageFormat.Json)]
        List<LocalEL> GetAllLocal();

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "PropuestaxIndicadores", ResponseFormat = WebMessageFormat.Json)]
        List<CalcularPropuestaxIndicadorEL> CalcularPropuestaxIndicadores(CalcularPropuestaxIndicadorEL CalcularPropuestaxIndicador);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "Combo", ResponseFormat = WebMessageFormat.Json)]
        List<ComboEL> GetAllCombo(ComboEL Combo);

        [OperationContract]
        [WebInvoke(Method = "POST", UriTemplate = "ProyectarPropuestaIndicador", ResponseFormat = WebMessageFormat.Json)]
        int proyectarPropuestaxIndicadores(proyectarPropuestaIndicadorEL proyectarPropuestaIndicador);
    }

}
