﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WCFRest.Dominio
{
    [DataContract]
    public class DetallePropuestaPublicidadEL : PropuestaPublicidadEL
    {
        ///

        /// Gets or Sets codDetallePropuestapublicidad
        ///
        [DataMember]
        public int codDetallePropuestapublicidad
        {
            get { return _codDetallePropuestapublicidad; }
            set { _codDetallePropuestapublicidad = value; }
        }
        private int _codDetallePropuestapublicidad;

        ///

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

        /// Gets or Sets codLocal
        ///
        [DataMember]
        public int codLocal
        {
            get { return _codLocal; }
            set { _codLocal = value; }
        }
        private int _codLocal;

        ///

        /// Gets or Sets porcentaje
        ///
        [DataMember]
        public int porcentaje
        {
            get { return _porcentaje; }
            set { _porcentaje = value; }
        }
        private int _porcentaje;

        ///

        /// Gets or Sets promedio
        ///
        [DataMember]
        public int promedio
        {
            get { return _promedio; }
            set { _promedio = value; }
        }
        private int _promedio;
    }
}