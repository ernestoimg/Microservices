using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Entities
{
    public class Pagination
    {
        [Required]
        public int PageSize { get; set; }
        [Required]
        public int Page { get; set; }
        [Required]
        public string Sort { get; set; }
        [Required]
        public string SortDirection { get; set; }
        public FilterValueClass FilterValue { get; set; }
    }

}
