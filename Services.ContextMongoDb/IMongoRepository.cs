using Services.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.ContextMongoDb
{
    public interface IMongoRepository<TDocument> where TDocument : IDocument
    {
        Task<IEnumerable<TDocument>> GetAll();
        Task<TDocument> GetById(string Id);
        Task InsertDocument(TDocument document);
        Task UpdateDocument(TDocument document);
        Task DeleteById(string Id);

        Task<PaginationEntity<TDocument>> PaginationBy(
            Expression<Func<TDocument, bool>> filterExpression,
            PaginationEntity<TDocument> pagination
        );
        Task<PaginationEntity<TDocument>> PaginationByFilter(
           PaginationEntity<TDocument> pagination
        );

    }
}
