using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Services.Configurations;
using Services.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ContextMongoDb
{
    public class AutorContext : IAutorContext
    {
        private readonly IMongoDatabase _db;
        public AutorContext(IOptions<MongoSettings> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);
            _db = client.GetDatabase(options.Value.Database);
        }

        public IMongoCollection<Autor> Autores => _db.GetCollection<Autor>("Autor");
    }
}
