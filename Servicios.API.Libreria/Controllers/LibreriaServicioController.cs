using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class LibreriaServicioController : ControllerBase
    {
        private readonly IAutorRepository _autorRepository;

        private readonly IMongoRepository<AutorEntity> _autorGenericoRepository;
        private readonly IMongoRepository<EmpleadoEntity> _empleadoGenericoRepository;

        public LibreriaServicioController(IAutorRepository autorRepository, IMongoRepository<AutorEntity> autorGenericoRepository, IMongoRepository<EmpleadoEntity> empleadoGenericoRepository)
        {
            _autorRepository = autorRepository;
            _autorGenericoRepository = autorGenericoRepository;
            _empleadoGenericoRepository = empleadoGenericoRepository;
        }

        [HttpGet("Autores")]
        public async Task<ActionResult<IEnumerable<Autor>>> GetAutores()
        {
            var autores = await _autorRepository.GetAutores();

            return Ok(autores);
        }

        [HttpGet("AutorGenerico")]
        public async Task<ActionResult<IEnumerable<AutorEntity>>> GetAutorGenerico()
        {

            var autores = await _autorGenericoRepository.GetAll();

            return Ok(autores);
        }

        [HttpGet("EmpleadoGenerico")]
        public async Task<ActionResult<IEnumerable<AutorEntity>>> GetEmpleadoGenerico()
        {

            var empleados = await _empleadoGenericoRepository.GetAll();

            return Ok(empleados);
        }
    }
}
