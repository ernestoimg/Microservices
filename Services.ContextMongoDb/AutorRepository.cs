using MongoDB.Driver;
using Services.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ContextMongoDb
{
    public class AutorRepository : IAutorRepository
    {

        private readonly IAutorContext _autorContext; 
        public AutorRepository(IAutorContext autorContext)
        {
            _autorContext = autorContext;
        }

        public async Task<IEnumerable<Autor>> GetAutores()
        {
            return await _autorContext.Autores.Find(p=>true).ToListAsync();
        }
    }
}
