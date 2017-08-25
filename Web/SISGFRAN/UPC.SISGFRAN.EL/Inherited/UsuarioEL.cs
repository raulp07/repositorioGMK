using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UPC.SISGFRAN.EL.Base;

namespace UPC.SISGFRAN.EL.Inherited
{
    public class UsuarioEL : BaseEL
    {
        public int Id { get; set; }
        public PerfilEL Perfil { get; set; }
        public string CtaUsuario { get; set; }
        public string Password { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Cargo { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
        public int Estado { get; set; }
        public bool CambiarContrasenia { get; set; }
        public DateTime FechaVenceCuenta { get; set; }
        public DateTime FechaVencePass { get; set; }
        public string NombreCompleto { get { return string.Concat(Apellidos, ", ", Nombres); } }
    }
}
