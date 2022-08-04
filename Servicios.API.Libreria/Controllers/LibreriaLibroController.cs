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
    [Route("api/[controller]")]
    [ApiController]
    public class LibreriaLibroController : ControllerBase
    {
        private readonly IMongoRepository<LibroEntity> _libroGenericoRepository;
        private readonly IMongoRepository<AutorEntity> _autorGenericoRepository;

        public LibreriaLibroController(IMongoRepository<LibroEntity> libroGenericoRepository, IMongoRepository<AutorEntity> autorGenericoRepository)
        {
            _libroGenericoRepository = libroGenericoRepository;
            _autorGenericoRepository = autorGenericoRepository;
        }

        ///<summary>
        /// Obtiene todos los autores
        /// </summary>
        [HttpGet]
        public async Task<ProcessResult<LibroEntity>> Get()
        {
            var result = await LibreriaLibro.GetAll(_libroGenericoRepository);

            return result;
        }

        ///<summary>
        /// Obtiene todos los autores
        /// </summary>
        /// <param name="id">Id del libro a buscar</param>
        [HttpGet("{id}")]
        public async Task<ProcessResult<LibroEntity>> GetById(string id)
        {
            var result = await LibreriaLibro.GetById(_libroGenericoRepository, id);

            return result;
        }

        [HttpPost("Pagination")]
        public async Task<ActionResult<PaginationEntity<LibroEntity>>> GetLibrosByFilter(Pagination pagination)
        {
            var result = await LibreriaLibro.Pagination(_libroGenericoRepository, pagination);

            return result;
        }

        ///<summary>
        /// Eliina un autor de la base de datos
        /// </summary>
        /// <param name="libro">Objeto de tipo a autor</param>
        /// <param name="autorId">Objeto de tipo a autor</param>
        [HttpPost("{autorId}")]
        public async Task<ProcessResult<LibroEntity>> Post(LibroEntityAddDto libro, string autorId)
        {
            var result = await LibreriaLibro.Post(_libroGenericoRepository, _autorGenericoRepository, libro, autorId);

            return result;

        }

        ///<summary>
        /// Eliina un autor de la base de datos
        /// </summary>
        /// <param name="libro">Objeto de tipo a autor</param>
        /// <param name="id">Objeto de tipo a autor</param>
        [HttpPut("{id}")]
        public async Task<ProcessResult<LibroEntity>> Update(string id, LibroEntityAddDto libro)
        {

            var result = await LibreriaLibro.Update(_libroGenericoRepository, _autorGenericoRepository, id, libro);

            return result;
        }

        ///<summary>
        /// Obtiene todos los autores
        /// </summary>
        /// <param name="id">Id del libro a buscar</param>
        /// 
        [HttpDelete("{id}")]
        public async Task<ProcessResult<LibroEntity>> Delete(string id)
        {
            var result = await LibreriaLibro.Delete(_libroGenericoRepository, id);

            return result;
        }
    }
}
