using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace MANAGMENT.Models.ViewModels
{
    public class LoginModel
    {
        public int Id { get; set; }


        //[Required(ErrorMessage = "User name  is required")]
        [Required]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Password { get; set; }
        public string Role { get; set; }
        public bool Isactive { get; set; }
    }
}