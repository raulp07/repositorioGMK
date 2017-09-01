using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WCFRest.Dominio
{
    [DataContract]
    public class CalcularPropuestaxIndicadorEL
    {
        [DataMember]
        public string listLocal { get; set; }
        [DataMember]
        public int codLocal { get; set; }
        [DataMember]
        public int codCombo { get; set; }
        [DataMember]
        public int indConsumo { get; set; }
        [DataMember]
        public int indSabor { get; set; }
        [DataMember]
        public int indCosto { get; set; }
        [DataMember]
        public int cantPuntuacionMax { get; set; }
        [DataMember]
        public int cantProyeccionVenta { get; set; }
        [DataMember]
        public string nombreCaractComboVenta { get; set; }
        [DataMember]
        public decimal impProyeccionCosto { get; set; }

    }
}