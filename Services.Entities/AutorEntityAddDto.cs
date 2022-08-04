using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Entities
{
    public class AutorEntityAddDto
    {
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string GradoAcademico { get; set; }
    }
}
