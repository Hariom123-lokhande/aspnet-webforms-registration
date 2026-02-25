using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcTrainingProject.Models
{
    public class Student
    {
        [Required(ErrorMessage = "Full Name is required")]
        [StringLength(50, MinimumLength = 3,
            ErrorMessage = "Full Name must be between 3 and 50 characters")]
        [RegularExpression(@"^[a-zA-Z\s]+$",
            ErrorMessage = "Full Name should contain only letters and spaces")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [StringLength(100)]
        [EmailAddress(ErrorMessage = "Enter a valid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, MinimumLength = 6,
            ErrorMessage = "Password must be at least 6 characters long")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&]).+$",
            ErrorMessage = "Password must contain uppercase, lowercase, number and special character")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [StringLength(200, MinimumLength = 5,
            ErrorMessage = "Address must be between 5 and 200 characters")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please select gender")]
        [RegularExpression(@"^(Male|Female|Other)$",
            ErrorMessage = "Invalid gender selection")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Please select city")]
        [StringLength(50)]
        public string City { get; set; }
    }

}