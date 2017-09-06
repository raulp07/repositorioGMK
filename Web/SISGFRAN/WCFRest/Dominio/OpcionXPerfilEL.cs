using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WCFRest.Dominio
{
    [DataContract]
    public class OpcionXPerfilEL : BaseEL
    {
        [DataMember]
        public AplicacionEL Aplicacion { get; set; }
        [DataMember]
        public PerfilEL Perfil { get; set; }
        [DataMember]
        public OpcionEL Opcion { get; set; }
        [DataMember]
        public bool Escritura { get; set; }
        public ICollection<OpcionXPerfilEL> Hijos { get; set; }
    }
}