using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MANAGMENT.Models.ViewModels
{
    public class ProductModel
    {
        public int id { get; set; }
       
        [Required(ErrorMessage = "Enter Product Name")]
        public string Name { get; set; }
        [Required]
        public int price { get; set; }
        [Required]
        public string area { get; set; }
        public string Image { get; set; }
        [Required]
        public string Description { get; set; }
    }
}