using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Entities
{
    [BSonCollection("Libro")]
    public class LibroEntity : Document
    {
        [BsonElement("titulo")]
        public string Titulo { get; set; }

        [BsonElement("decripcion")]
        public string Descripcion { get; set; }
        
        [BsonElement("precio")]
        public decimal Precio { get; set; }
        
        [BsonElement("fechaPublicacion")]
        public DateTime? FechaPublicacion { get; set; }
        
        [BsonElement("autor")]
        public AutorEntity Autor { get; set; }
    }
}
