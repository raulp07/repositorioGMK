using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WCFRest.Dominio
{
    [DataContract]
    public class PerfilEL
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public AplicacionEL Aplicacion { get; set; }

        [DataMember]
        public string Nombre { get; set; }

        [DataMember]
        public string Descripcion { get; set; }
    }
}