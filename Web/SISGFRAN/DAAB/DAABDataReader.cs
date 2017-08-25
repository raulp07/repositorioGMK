using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DAAB
{
    public abstract class DAABDataReader
    {
        public abstract IDataReader ReturnDataReader
        {
            get;
            set;
        }
    }
}
