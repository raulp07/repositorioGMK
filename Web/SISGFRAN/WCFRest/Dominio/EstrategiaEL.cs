using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WCFRest.Dominio
{
    [DataContract]
    public class EstrategiaEL
    {
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

        /// Gets or Sets descripcionEstrategia
        ///
        [DataMember]
        public string descripcionEstrategia
        {
            get { return _descripcionEstrategia; }
            set { _descripcionEstrategia = value; }
        }
        private string _descripcionEstrategia;

        ///

        /// Gets or Sets fechaCumplimiento
        ///
        [DataMember]
        public DateTime fechaCumplimiento
        {
            get { return _fechaCumplimiento; }
            set { _fechaCumplimiento = value; }
        }
        private DateTime _fechaCumplimiento;

        ///

        /// Gets or Sets estadoEstrategia
        ///
        [DataMember]
        public string estadoEstrategia
        {
            get { return _estadoEstrategia; }
            set { _estadoEstrategia = value; }
        }
        private string _estadoEstrategia;

        ///

        /// Gets or Sets codObjetivo
        ///
        [DataMember]
        public int codObjetivo
        {
            get { return _codObjetivo; }
            set { _codObjetivo = value; }
        }
        private int _codObjetivo;
    }
}