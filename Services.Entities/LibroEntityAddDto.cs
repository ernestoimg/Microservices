using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Entities
{
    public class LibroEntityAddDto
    {
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public AutorEntity Autor { get; set; }
    }
}
