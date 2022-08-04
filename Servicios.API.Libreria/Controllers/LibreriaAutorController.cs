using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Business;
using Services.ContextMongoDb;
using Services.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Servicios.API.Libreria.Controllers
{
    ///<summary>
    /// Autores
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class LibreriaAutorController : ControllerBase
    {
        private readonly IMongoRepository<AutorEntity> _autorGenericoRepository;

        ///<summary>
        /// Constructor para instancia del IMongoRepository
        /// </summary>
        public LibreriaAutorController(IMongoRepository<AutorEntity> autorGenericoRepository)
        {
            _autorGenericoRepository = autorGenericoRepository;
        }

        ///<summary>
        /// Obtiene todos los autores
        /// </summary>
        /// 
        [HttpGet]
        public async Task<ProcessResult<AutorEntity>> Get()
        {
            var result = await LibreriaAutor.Get(_autorGenericoRepository);

            return result;
        }
        ///<summary>
        /// Obtiene un autor por Id
        /// </summary>
        /// <param name="id">Identificador del autor</param>
        [HttpGet("{id}")]
        public async Task<ProcessResult<AutorEntity>> GetById(string id)
        {
            var result = await LibreriaAutor.GetById(_autorGenericoRepository, id);

            return result;
        }

        [HttpPost("Pagination")]
        public async Task<ActionResult<PaginationEntity<AutorEntity>>> GetAutorsByFilters(Pagination pagination)
        {
            var result = await LibreriaAutor.Pagination(_autorGenericoRepository, pagination);

            return result;
        }

        ///<summary>
        /// Eliina un autor de la base de datos
        /// </summary>
        /// <param name="autor">Objeto de tipo a autor</param>
        [HttpPost]
        public async Task<ProcessResult<AutorEntity>> Post(AutorEntityAddDto autor)
        {
            var result = await LibreriaAutor.Post(_autorGenericoRepository, autor);

            return result;
        }

        ///<summary>
        /// Eliina un autor de la base de datos
        /// </summary>
        /// <param name="id">Identificador del autor</param>
        /// <param name="autor">Objeto de tipo a autor</param>
        [HttpPut("{id}")]
        public async Task<ProcessResult<AutorEntity>> Update(string id, AutorEntityAddDto autor)
        {
            var result = await LibreriaAutor.Update(_autorGenericoRepository,id,autor);

            return result;
        }

        ///<summary>
        /// Eliina un autor de la base de datos
        /// </summary>
        /// <param name="id">Identificador del autor</param>
        [HttpDelete("{id}")]
        public async Task<ProcessResult<AutorEntity>> Delete(string id)
        {
            var result =  await LibreriaAutor.Delete(_autorGenericoRepository,id);

            return result;
        }
    }
}
