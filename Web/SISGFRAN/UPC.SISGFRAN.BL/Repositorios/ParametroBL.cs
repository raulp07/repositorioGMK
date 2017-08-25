using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UPC.SISGFRAN.DAL.Repositorios;
using UPC.SISGFRAN.EL.NonInherited;

namespace UPC.SISGFRAN.BL.Repositorios
{
    public class ParametroBL
    {
        public List<ParametroEL> GetParametros(ParametroEL codigo)
        {
            return new ParametroDA().GetParametros(codigo);
        }

        public ParametroEL GetParametroByID(int? codigoId)
        {
            return new ParametroDA().GetParametroByID(codigoId);
        }
    }
}
