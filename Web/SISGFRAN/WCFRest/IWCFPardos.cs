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


    }

}
