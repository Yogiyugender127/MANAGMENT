using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MANAGMENT.Models.ViewModels
{
    public class OrderItemsModel
    {
        public int OrderID { get; set; }
        public Nullable<int> CatergoryID { get; set; }
        public string CategoryName { get; set; }
        public Nullable<int> ProductID { get; set; }
        public string ProductName { get; set; }
        public int price { get; set; }
        public Nullable<int> CustomerID { get; set; }
        public string CustomerNAME { get; set; }
        public string Email { get; set; }
        public Nullable<int> Qty { get; set; }
        public Nullable<int> Discount { get; set; }
        public Nullable<decimal> OrderTotal { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }

    }
}