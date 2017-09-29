using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WCFRest.Dominio
{
    public class ObjetivoEL
    {

        ///

        /// Gets or Sets codObjetivo
        ///

        public int codObjetivo
        {
            get { return _codObjetivo; }
            set { _codObjetivo = value; }
        }
        private int _codObjetivo;

        ///

        /// Gets or Sets nombreObjetivo
        ///

        public string nombreObjetivo
        {
            get { return _nombreObjetivo; }
            set { _nombreObjetivo = value; }
        }
        private string _nombreObjetivo;

        ///

        /// Gets or Sets estadoObjetivo
        ///

        public string estadoObjetivo
        {
            get { return _estadoObjetivo; }
            set { _estadoObjetivo = value; }
        }
        private string _estadoObjetivo;

        ///

        /// Gets or Sets idPlanMkt
        ///

        public int idPlanMkt
        {
            get { return _idPlanMkt; }
            set { _idPlanMkt = value; }
        }
        private int _idPlanMkt;

    }
}