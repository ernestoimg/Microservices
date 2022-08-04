using MongoDB.Driver;
using Services.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Services.ContextMongoDb
{
    public interface IAutorContext
    {
        IMongoCollection<Autor> Autores { get; }
    }
}
