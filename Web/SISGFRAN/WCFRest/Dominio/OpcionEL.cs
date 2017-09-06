using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WCFRest.Dominio
{
    [DataContract]
    public class OpcionEL:BaseEL
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public AplicacionEL Aplicacion { get; set; }
        [DataMember]
        public int Nivel { get; set; }
        [DataMember]
        public string Nombre { get; set; }
        [DataMember]
        public int PadreId { get; set; }
        [DataMember]
        public int NivelPadre { get; set; }
        [DataMember]
        public string Imagen { get; set; }
        [DataMember]
        public string Controlador { get; set; }
        [DataMember]
        public string Accion { get; set; }
        [DataMember]
        public int Orden { get; set; }
        [DataMember]
        public string Observacion { get; set; }
        [DataMember]
        public int Estado { get; set; }
    }
}