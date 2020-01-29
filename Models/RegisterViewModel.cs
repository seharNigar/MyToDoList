using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models
{
    public class RegisterViewModel
    {
       
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        [Remote (action:"IsEmailExist",controller:"User")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        [DataType(DataType.Password)]
        [Display(Name = "confirm password")]
        [Compare("Password", ErrorMessage = "password and confirmation password do not match")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Remote(action: "IsUserNameExist", controller: "User")]
        public string Name { get; set; }

      
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^[\d]{4}-[\d]{7}$")]
        public string Number { get; set; }

        [Required]
        [Display(Name = "Date Of Birth")]

        public DateTime DateOfBirth { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string City { get; set; }
        [DataType(DataType.Upload)]
        public IFormFile Photo { get; set; }
    }
}
