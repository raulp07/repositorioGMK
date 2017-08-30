using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WCFRest.Dominio
{
    [DataContract]
    public class BaseEL
    {
        [DataMember]
        public int CodeMessage { get; set; }

        [DataMember]
        public string MessageErr { get; set; }

        #region "Auditoria"
        [DataMember]
        public int UsuarioCreacion { get; set; }

        [DataMember]
        public DateTime FechaCreacion { get; set; }

        [DataMember]
        public int UsuarioModifica { get; set; }

        [DataMember]
        public DateTime FechaModifica { get; set; }
        #endregion
    }
}