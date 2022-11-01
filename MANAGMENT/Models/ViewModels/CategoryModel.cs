using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MANAGMENT.Models.ViewModels
{
    public class CategoryModel
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }

   
    }
}