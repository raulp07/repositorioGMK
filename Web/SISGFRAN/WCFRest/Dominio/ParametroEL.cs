using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WCFRest.Dominio
{
    [DataContract]
    public class ParametroEL
    {

        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int CodigoGrupo { get; set; }
        [DataMember]
        public string Grupo { get; set; }
        [DataMember]
        public int Codigo { get; set; }
        [DataMember]
        public string Nombre { get; set; }
        [DataMember]
        public string Valor { get; set; }
        [DataMember]
        public string Descripcion { get; set; }
        [DataMember]
        public string Value1 { get; set; }
        [DataMember]
        public string Value2 { get; set; }
        [DataMember]
        public string Value3 { get; set; }

    }
}