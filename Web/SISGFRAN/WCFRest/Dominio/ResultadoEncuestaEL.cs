using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WCFRest.Dominio
{
    [DataContract]
    public class ResultadoEncuestaEL:MedioComunicacionEL
    {
        ///

        /// Gets or Sets codResultadoEncuesta
        ///
        [DataMember]
        public int codResultadoEncuesta
        {
            get { return _codResultadoEncuesta; }
            set { _codResultadoEncuesta = value; }
        }
        private int _codResultadoEncuesta;

        ///

        /// Gets or Sets puntajeCaracteristicaCombo
        ///
        [DataMember]
        public int puntajeCaracteristicaCombo
        {
            get { return _puntajeCaracteristicaCombo; }
            set { _puntajeCaracteristicaCombo = value; }
        }
        private int _puntajeCaracteristicaCombo;
        
        [DataMember]
        public int Porcentaje { get; set; }

        [DataMember]
        public int sumatoria { get; set; }

        [DataMember]
        public int cantidad { get; set; }

        [DataMember]
        public int promedio { get; set; }

    }
}