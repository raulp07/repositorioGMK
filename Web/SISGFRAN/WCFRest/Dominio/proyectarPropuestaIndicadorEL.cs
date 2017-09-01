using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WCFRest.Dominio
{
    [DataContract]
    public class proyectarPropuestaIndicadorEL
    {
        [DataMember]
        public int codProyectarPropuestaIndicador { get; set; }

        [DataMember]
        public int indicadorConsumo { get; set; }

        [DataMember]
        public int indicadorSabor { get; set; }

        [DataMember]
        public int indicadorCosto { get; set; }

        [DataMember]
        public DateTime fechaRegistroIndicador { get; set; }

        [DataMember]
        public int codComboLocal { get; set; }

        [DataMember]
        public int codCombo { get; set; }

        [DataMember]
        public int codLocal { get; set; }
    }
}