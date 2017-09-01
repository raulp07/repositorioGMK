using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WCFRest.Dominio
{
    [DataContract]
    public class comboPlanMarketingEL
    {
        ///

        /// Gets or Sets codComboPlanMKT
        ///
        [DataMember]
        public int codComboPlanMKT
        {
            get { return _codComboPlanMKT; }
            set { _codComboPlanMKT = value; }
        }
        private int _codComboPlanMKT;

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
    }
}