using Services.ContextMongoDb;
using Services.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Business
{
    public class LibreriaAutor
    {
        
        public static async Task<ProcessResult<AutorEntity>> Get(IMongoRepository<AutorEntity> autorGenericoRepository)
        {
            var result = new ProcessResult<AutorEntity>();
            try
            {
                result.Data = await autorGenericoRepository.GetAll();
                
            }
            catch(Exception ex)
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

        public static async Task<ProcessResult<AutorEntity>> GetById(IMongoRepository<AutorEntity> autorGenericoRepository,string id)
        {
            var result = new ProcessResult<AutorEntity>();
            try
            {
                var autor = new List<AutorEntity>();

                autor.Add(await autorGenericoRepository.GetById(id));

                result.Data = autor;

            }
            catch (Exception ex)
            {
                result.Message = ex.Message.ToString();
                result.HasError = true;
            }
            finally
            {
                var validation = result.Data.ToArray();

                if(validation[0] != null)
                {
                    result.Rows = result.Data.Count();
                };
            }

            return result;
        }

        public static async Task<PaginationEntity<AutorEntity>> Pagination(IMongoRepository<AutorEntity> autorGenericoRepository, Pagination pagination)
        {
            var resultados = new PaginationEntity<AutorEntity>();
            
            try
            {
                var paginationFilter = new PaginationEntity<AutorEntity>()
                {
                    Page = pagination.Page,
                    PageSize = pagination.PageSize,
                    Sort = pagination.Sort,
                    SortDirection = pagination.SortDirection,
                    FilterValue = pagination.FilterValue
                };

                resultados = await autorGenericoRepository.PaginationByFilter(
                    paginationFilter
                    );

                
            }
            catch(Exception ex)
            {
                resultados.Filter = ex.Message.ToString();
            }
            finally
            {
                resultados.TotalRows = resultados.Data.Count();
            }
            return resultados;
        }

        public static async Task<ProcessResult<AutorEntity>> Update(IMongoRepository<AutorEntity> autorGenericoRepository, string id, AutorEntityAddDto autor)
        {
            var result = new ProcessResult<AutorEntity>();
            try
            {
                 var autorExists = GetAutorById(autorGenericoRepository, id).GetAwaiter().GetResult();

                if (autorExists == null)
                {
                    result.Message = "El autor a modificar no existe";
                    result.HasError = true;
                }

                var autorAdd = new AutorEntity()
                {
                    Id = id,
                    Nombre = autor.Nombre,
                    Apellido = autor.Apellido,
                    GradoAcademico = autor.GradoAcademico
                };

                await autorGenericoRepository.UpdateDocument(autorAdd);
            }
            catch(Exception ex)
            {
                result.Message = ex.Message.ToString();
                result.HasError = true;
            }

            return result;
        }

        public static async Task<ProcessResult<AutorEntity>> Post(IMongoRepository<AutorEntity> autorGenericoRepository, AutorEntityAddDto autor)
        {
            var result = new ProcessResult<AutorEntity>();

            try
            {
                var autorAdd = new AutorEntity()
                {
                    Nombre = autor.Nombre,
                    Apellido = autor.Apellido,
                    GradoAcademico = autor.GradoAcademico
                };

                await autorGenericoRepository.InsertDocument(autorAdd);
            }
            catch(Exception ex)
            {
                result.Message = ex.Message.ToString();
                result.HasError = true;
            }

            return result;
            
        }

        public static async Task<ProcessResult<AutorEntity>> Delete(IMongoRepository<AutorEntity> autorGenericoRepository, string id)
        {
            var result = new ProcessResult<AutorEntity>();
            try
            {
                var autorExists = GetAutorById(autorGenericoRepository, id).GetAwaiter().GetResult();

                if (autorExists == null)
                {
                    result.Message = "El autor a eliminar no existe";
                    result.HasError = true;
                }

                await autorGenericoRepository.DeleteById(id);
            }
            catch(Exception ex)
            {
                result.Message = ex.Message.ToString();
                result.HasError = true;
            }

            return result;
        }

        public static async Task<AutorEntity> GetAutorById(IMongoRepository<AutorEntity> autorGenericoRepository, string id)
        {
            var result = await autorGenericoRepository.GetById(id);

            return result;
        }
    }
}
