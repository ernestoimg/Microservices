using Services.ContextMongoDb;
using Services.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Business
{
    public class LibreriaLibro
    {

        private readonly IMongoRepository<AutorEntity> _autorGenericoRepository;

        public LibreriaLibro(IMongoRepository<AutorEntity> autorGenericoRepository)
        {
            _autorGenericoRepository = autorGenericoRepository;
        }

        public static async Task<ProcessResult<LibroEntity>> GetAll(IMongoRepository<LibroEntity> libroGenericoRepository)
        {
            var result = new ProcessResult<LibroEntity>();

            try
            {
                var libros = await libroGenericoRepository.GetAll();

                result.Data = libros;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message.ToString();
                result.HasError = true;
            }
            finally
            {
                var validation = result.Data.ToArray();

                if (validation[0] != null)
                {
                    result.Rows = result.Data.Count();
                };
            }

            return result;
        }

        public static async Task<ProcessResult<LibroEntity>> GetById(IMongoRepository<LibroEntity> libroGenericoRepository, string id)
        {
            var result = new ProcessResult<LibroEntity>();
            try
            {
                var libro = new List<LibroEntity>();

                libro.Add(await libroGenericoRepository.GetById(id));

                result.Data = libro;

            }
            catch (Exception ex)
            {
                result.Message = ex.Message.ToString();
                result.HasError = true;
            }
            finally
            {
                var validation = result.Data.ToArray();

                if (validation[0] != null)
                {
                    result.Rows = result.Data.Count();
                };
            }

            return result;
        }

        public static async Task<PaginationEntity<LibroEntity>> Pagination(IMongoRepository<LibroEntity> libroGenericoRepository, Pagination pagination)
        {
            var resultados = new PaginationEntity<LibroEntity>();

            try
            {
                var paginationFilter = new PaginationEntity<LibroEntity>()
                {
                    Page = pagination.Page,
                    PageSize = pagination.PageSize,
                    Sort = pagination.Sort,
                    SortDirection = pagination.SortDirection,
                    FilterValue = pagination.FilterValue
                };

                resultados = await libroGenericoRepository.PaginationByFilter(
                    paginationFilter
                    );

            }
            catch (Exception ex)
            {
                resultados.Filter = ex.Message.ToString();
            }
            
            return resultados;
        }

        public static async Task<ProcessResult<LibroEntity>> Post(IMongoRepository<LibroEntity> libroGenericoRepository, IMongoRepository<AutorEntity> autorGenericoRepository, LibroEntityAddDto libro, string autorId)
        {
            var result = new ProcessResult<LibroEntity>();

            try
            {
                var autorExists = LibreriaAutor.GetAutorById(autorGenericoRepository, autorId).GetAwaiter().GetResult();

                if (autorExists != null)
                {
                    autorExists.NombreCompleto = libro.Autor.NombreCompleto;

                    var libroAdd = new LibroEntity()
                    {
                        Titulo = libro.Titulo,
                        Descripcion = libro.Descripcion,
                        Precio = libro.Precio,
                        FechaPublicacion = DateTime.Now,
                        Autor = autorExists
                    };


                    await libroGenericoRepository.InsertDocument(libroAdd);
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message.ToString();
                result.HasError = true;
            }

            return result;

        }

        public static async Task<ProcessResult<LibroEntity>> Update(IMongoRepository<LibroEntity> libroGenericoRepository,IMongoRepository<AutorEntity> autorGenericoRepository, string id, LibroEntityAddDto libro)
        {
            var result = new ProcessResult<LibroEntity>();
            try
            {
                var libroExist = GetLibroById(libroGenericoRepository, id).GetAwaiter().GetResult();

                if (libroExist == null)
                {
                    result.Message = "El libro a modificar no existe";
                    result.HasError = true;
                }

                var autorExist = LibreriaAutor.GetAutorById(autorGenericoRepository, libro.Autor.Id).GetAwaiter().GetResult();

                if (autorExist == null)
                {
                    result.Message = "El autor no existe";
                    result.HasError = true;
                }

                var libroAdd = new LibroEntity()
                {
                    Id = id,
                    Titulo = libro.Titulo,
                    Descripcion = libro.Descripcion,
                    Precio = libro.Precio,
                    FechaPublicacion = DateTime.Now,
                    Autor = autorExist
                };

                await libroGenericoRepository.UpdateDocument(libroAdd);

            }
            catch (Exception ex)
            {
                result.Message = ex.Message.ToString();
                result.HasError = true;
            }

            return result;
        }

        public static async Task<ProcessResult<LibroEntity>> Delete(IMongoRepository<LibroEntity> libroGenericoRepository, string id)
        {
            var result = new ProcessResult<LibroEntity>();
            try
            {
                var libroExists = GetLibroById(libroGenericoRepository, id).GetAwaiter().GetResult();

                if (libroExists == null)
                {
                    result.Message = "El libro a eliminar no existe";
                    result.HasError = true;
                }

                await libroGenericoRepository.DeleteById(id);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message.ToString();
                result.HasError = true;
            }

            return result;
        }

        public static async Task<LibroEntity> GetLibroById(IMongoRepository<LibroEntity> libroGenericoRepository, string id)
        {
            var result = await libroGenericoRepository.GetById(id);

            return result;
        }
    }
}
