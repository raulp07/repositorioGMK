using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WCFRest.Dominio
{
    [DataContract]
    public class PlanMarketingEL
    {
        ///

        /// Gets or Sets codPlanMkt
        ///
        [DataMember]
        public int codPlanMkt
        {
            get { return _codPlanMkt; }
            set { _codPlanMkt = value; }
        }
        private int _codPlanMkt;

        ///

        /// Gets or Sets nombrePlanMkt
        ///
        [DataMember]
        public string nombrePlanMkt
        {
            get { return _nombrePlanMkt; }
            set { _nombrePlanMkt = value; }
        }
        private string _nombrePlanMkt;

        ///

        /// Gets or Sets presupuestoPlanMkt
        ///
        [DataMember]
        public decimal presupuestoPlanMkt
        {
            get { return _presupuestoPlanMkt; }
            set { _presupuestoPlanMkt = value; }
        }
        private decimal _presupuestoPlanMkt;

        ///

        /// Gets or Sets fechaRegistro
        ///
        [DataMember]
        public DateTime fechaRegistro
        {
            get { return _fechaRegistro; }
            set { _fechaRegistro = value; }
        }
        private DateTime _fechaRegistro;

        ///

        /// Gets or Sets fechaInicio
        ///
        [DataMember]
        public DateTime fechaInicio
        {
            get { return _fechaInicio; }
            set { _fechaInicio = value; }
        }
        private DateTime _fechaInicio;

        ///

        /// Gets or Sets fechaFin
        ///
        [DataMember]
        public DateTime fechaFin
        {
            get { return _fechaFin; }
            set { _fechaFin = value; }
        }
        private DateTime _fechaFin;

        ///

        /// Gets or Sets fechaInicioReal
        ///
        [DataMember]
        public DateTime fechaInicioReal
        {
            get { return _fechaInicioReal; }
            set { _fechaInicioReal = value; }
        }
        private DateTime _fechaInicioReal;

        ///

        /// Gets or Sets fechaFinReal
        ///
        [DataMember]
        public DateTime fechaFinReal
        {
            get { return _fechaFinReal; }
            set { _fechaFinReal = value; }
        }
        private DateTime _fechaFinReal;

        ///

        /// Gets or Sets estadoPlanMkt
        ///
        [DataMember]
        public string estadoPlanMkt
        {
            get { return _estadoPlanMkt; }
            set { _estadoPlanMkt = value; }
        }
        private string _estadoPlanMkt;

    }
}