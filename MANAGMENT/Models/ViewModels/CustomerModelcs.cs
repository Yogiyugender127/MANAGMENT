using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MANAGMENT.Models.ViewModels
{
    public class CustomerModelcs
    {
        public int CustomerID { get; set; }

        [MaxLength(24)]
        [Required(ErrorMessage = "Please enter student name.")]
        public string CustomerName { get; set; }


        [Required(ErrorMessage = "Please enter Password.")]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Confirm password is not same")]
        public string ConfirmPassword { get; set; }


        [Required(ErrorMessage = "Please enter age.")]
        public Nullable<int> Age { get; set; }

        [Required(ErrorMessage = "Please enter student name.")]
        [EmailAddress]
        public string EmailID { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
    }
}