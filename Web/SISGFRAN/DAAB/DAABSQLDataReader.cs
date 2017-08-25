using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DAAB
{
    public class DAABSQLDataReader : DAABDataReader
    {
        IDataReader m_oReturnedDataReader;

        public override IDataReader ReturnDataReader
        {
            get
            {
                return m_oReturnedDataReader;
            }
            set
            {
                m_oReturnedDataReader = value;
            }
        }
    }
}
