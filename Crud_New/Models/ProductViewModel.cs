using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crud_New.Models
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }
        public List<ProductCategory> ProductList { get; set; }
    }
}
