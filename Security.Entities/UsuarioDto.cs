using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.Entities
{
    public class UsuarioDto
    {
       
        public string UserName { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }

        public string Token { get; set; }
    }
}
