using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UPC.SISGFRAN.DAL.Repositorios;
using UPC.SISGFRAN.EL.Inherited;

namespace UPC.SISGFRAN.BL.Repositorios
{
    public class OpcionBL
    {
        public List<OpcionEL> GetOpciones(OpcionEL opcion)
        {
            return new OpcionDA().GetOpciones(opcion);
        }

        public OpcionEL GetOpcionByID(int? idOpcion)
        {
            return new OpcionDA().GetOpcionByID(idOpcion);
        }
    }
}
