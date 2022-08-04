using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Entities
{
    [BSonCollection("Empleado")]
    public class EmpleadoEntity : Document
    {
        [BsonElement("nombre")]
        public string Nombre { get; set; }
    }
}
