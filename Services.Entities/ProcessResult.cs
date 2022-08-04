using System.Collections.Generic;

namespace Services.Entities
{
    public class ProcessResult<TDocument>
    {
        public bool HasError { get; set; }
        public string Message { get; set; }
        public int Rows { get; set; }
        public IEnumerable<TDocument> Data { get; set; }
    }
}
