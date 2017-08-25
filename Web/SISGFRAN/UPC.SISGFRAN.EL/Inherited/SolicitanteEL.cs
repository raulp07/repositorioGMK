using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UPC.SISGFRAN.EL.Base;
using UPC.SISGFRAN.EL.NonInherited;

namespace UPC.SISGFRAN.EL.Inherited
{
    public class SolicitanteEL : BaseEL
    {
        public int Id { get; set; }
        public ParametroEL TipoSolicitante { get; set; }
        public ParametroEL TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public decimal MontoIngresosAnuales { get; set; }
        public string UbigeoDireccion { get; set; }
        public string Direccion { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public string InstitucionEducativa { get; set; }
        public string TituloObtenido { get; set; }
        public int DuracionEstudios { get; set; }
        public string Idiomas { get; set; }
        public string Empresa { get; set; }
        public string Cargo { get; set; }
        public decimal MontoIngresosMes { get; set; }
        public decimal MontoGastosMes { get; set; }
        public string Observacion { get; set; }

        #region "Natural o Representante Legal"
        public string Nombres { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string NombreCompleto { get { return string.Concat(ApellidoPaterno, " ", ApellidoMaterno, ", ", Nombres); } }
        public ParametroEL Sexo { get; set; }
        public ParametroEL EstadoCivil { get; set; }
        public DateTime FechaIniCargoActual { get; set; }
        public DateTime FechaFinCargoActual { get; set; }
        public string NombreJefeDirecto { get; set; }
        public string TelefonoJefeDirecto { get; set; }

        public DateTime FechaNacimiento { get; set; }

        #endregion
        
        #region "Juridico"
        public string RazonSocial { get; set; }
        public DateTime FechaConstitucion { get; set; }
        public string ActividadEconomica { get; set; }
        public int NumeroEmpleados { get; set; }
        #endregion
        
        //Aquí comienza la mariconada!!!
        public bool FueAprobado { get; set; }
    }
}
