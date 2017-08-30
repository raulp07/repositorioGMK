using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WCFRest.Dominio
{
    [DataContract]
    public class MedioComunicacionEL
    {

        ///

        /// Gets or Sets codMedioComunicacion
        ///
        [DataMember]
        public int codMedioComunicacion
        {
            get { return _codMedioComunicacion; }
            set { _codMedioComunicacion = value; }
        }
        private int _codMedioComunicacion;

        ///

        /// Gets or Sets nombreMedioComunicacion
        ///
        [DataMember]
        public string nombreMedioComunicacion
        {
            get { return _nombreMedioComunicacion; }
            set { _nombreMedioComunicacion = value; }
        }
        private string _nombreMedioComunicacion;

        ///

        /// Gets or Sets costoUnitarioMedioComunicacion
        ///
        [DataMember]
        public decimal costoUnitarioMedioComunicacion
        {
            get { return _costoUnitarioMedioComunicacion; }
            set { _costoUnitarioMedioComunicacion = value; }
        }
        private decimal _costoUnitarioMedioComunicacion;
    }
}