using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WCFRest.Dominio
{
    [DataContract]
    public class SugerirTemporadaPromocionEL
    {
        [DataMember]
        public int codLocal { get; set; }
        [DataMember]
        public string NombreLocal { get; set; }
        [DataMember]
        public int codCombo { get; set; }
        [DataMember]
        public string NombreCombo { get; set; }
        [DataMember]
        public int periodo { get; set; }
        [DataMember]
        public int anioVenta { get; set; }
        [DataMember]
        public int porVentaxPeridoxAnio { get; set; }

        [DataMember]
        public int periodoini { get; set; }
        [DataMember]
        public int periodofin { get; set; }
        [DataMember]
        public int anioini { get; set; }
        [DataMember]
        public int aniofin { get; set; }
    }
}