using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WCFRest.Dominio
{
    [DataContract]
    public class AccionEL
    {
        ///

        /// Gets or Sets codAccion
        ///
        [DataMember]
        public int codAccion
        {
            get { return _codAccion; }
            set { _codAccion = value; }
        }
        private int _codAccion;

        ///

        /// Gets or Sets nombreAccion
        ///
        [DataMember]
        public string nombreAccion
        {
            get { return _nombreAccion; }
            set { _nombreAccion = value; }
        }
        private string _nombreAccion;

        ///

        /// Gets or Sets descripcionAccion
        ///
        [DataMember]
        public string descripcionAccion
        {
            get { return _descripcionAccion; }
            set { _descripcionAccion = value; }
        }
        private string _descripcionAccion;

        ///

        /// Gets or Sets fechaRegistroAccion
        ///
        [DataMember]
        public DateTime fechaRegistroAccion
        {
            get { return _fechaRegistroAccion; }
            set { _fechaRegistroAccion = value; }
        }
        private DateTime _fechaRegistroAccion;

        ///

        /// Gets or Sets costoAccion
        ///
        [DataMember]
        public decimal costoAccion
        {
            get { return _costoAccion; }
            set { _costoAccion = value; }
        }
        private decimal _costoAccion;

        ///

        /// Gets or Sets fechaInicioAccion
        ///
        [DataMember]
        public DateTime fechaInicioAccion
        {
            get { return _fechaInicioAccion; }
            set { _fechaInicioAccion = value; }
        }
        private DateTime _fechaInicioAccion;

        ///

        /// Gets or Sets codEstrategia
        ///
        [DataMember]
        public int codEstrategia
        {
            get { return _codEstrategia; }
            set { _codEstrategia = value; }
        }
        private int _codEstrategia;

        ///

        /// Gets or Sets fechaFinAccion
        ///
        [DataMember]
        public DateTime fechaFinAccion
        {
            get { return _fechaFinAccion; }
            set { _fechaFinAccion = value; }
        }
        private DateTime _fechaFinAccion;

        ///

        /// Gets or Sets fechaInicioRealAccion
        ///
        [DataMember]
        public DateTime fechaInicioRealAccion
        {
            get { return _fechaInicioRealAccion; }
            set { _fechaInicioRealAccion = value; }
        }
        private DateTime _fechaInicioRealAccion;

        ///

        /// Gets or Sets fechaFinRealAccion
        ///
        [DataMember]
        public DateTime fechaFinRealAccion
        {
            get { return _fechaFinRealAccion; }
            set { _fechaFinRealAccion = value; }
        }
        private DateTime _fechaFinRealAccion;

        ///

        /// Gets or Sets estadoAccion
        ///
        [DataMember]
        public string estadoAccion
        {
            get { return _estadoAccion; }
            set { _estadoAccion = value; }
        }
        private string _estadoAccion;
    }
}