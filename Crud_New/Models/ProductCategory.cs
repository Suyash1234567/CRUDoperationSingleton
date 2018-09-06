using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Crud_New.Models
{
    public class ProductCategory
    {
        public int ProductId { get; set; }
        [Required]
        public string ProductName { get; set; }

        [Required]
        public string ProductPrice { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public string CategoryName { get; set; }

        [Required]
        public string DateCreated { get; set; }
    }
}
