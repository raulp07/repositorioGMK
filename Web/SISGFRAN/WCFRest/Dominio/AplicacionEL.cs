using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WCFRest.Dominio
{
    [DataContract]
    public class AplicacionEL : BaseEL
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public ParametroEL TipoAplicacion { get; set; }

        [DataMember]
        public string Descripcion { get; set; }

        [DataMember]
        public int Estado { get; set; }
    }
}