using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WCFRest.Dominio
{
    [DataContract]
    public class PropuestaPublicidadEL
    {
        ///

        /// Gets or Sets codPropuestapublicidad
        ///
        [DataMember]
        public int codPropuestapublicidad
        {
            get { return _codPropuestapublicidad; }
            set { _codPropuestapublicidad = value; }
        }
        private int _codPropuestapublicidad;

        ///

        /// Gets or Sets fechaPropuestapublicidad
        ///
        [DataMember]
        public DateTime fechaPropuestapublicidad
        {
            get { return _fechaPropuestapublicidad; }
            set { _fechaPropuestapublicidad = value; }
        }
        private DateTime _fechaPropuestapublicidad;

        ///

        /// Gets or Sets precioPropuestaPublicidad
        ///
        [DataMember]
        public decimal precioPropuestaPublicidad
        {
            get { return _precioPropuestaPublicidad; }
            set { _precioPropuestaPublicidad = value; }
        }
        private decimal _precioPropuestaPublicidad;

        [DataMember]
        public string ObservacionPropuestaPublicidad { get; set; }
    }
}