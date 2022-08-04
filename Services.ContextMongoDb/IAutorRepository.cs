using Services.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ContextMongoDb
{
    public interface IAutorRepository
    {
        Task<IEnumerable<Autor>> GetAutores();
    }
}
