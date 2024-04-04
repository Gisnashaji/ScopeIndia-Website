using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;

namespace Scope.Models
{
    public class Editprofile
    {
        [Required(ErrorMessage = " Enter your first name")]
        [StringLength(100)]
        [Display(Name = "First Name")]
        public  string FirstName { get; set; }


        [Required(ErrorMessage = " Enter your last name")]
        [StringLength(100)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = " choose your gender")]
        [Display(Name = "Gender")]
        public string Gender { get; set; }

        [Required(ErrorMessage = " Enter date of birth")]

        [Display(Name = "Date of Birth")]

        [DataType(DataType.DateTime)]
        public DateTime DateofBirth { get; set; }


        [Required(ErrorMessage = " Enter email address")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [StringLength(100, ErrorMessage = "Email should be no longer than 100 characters.")]
        [Display(Name = "Email Address")]


        public string Email { get; set; }


        [Required(ErrorMessage = " Enter phone number")]
        [Phone(ErrorMessage = "Invalid phone number.")]
        [StringLength(20, ErrorMessage = "Phone number should be no longer than 20 characters.")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }


        [Required(ErrorMessage = "Country is required")]
        [StringLength(50, ErrorMessage = "Country should be no longer than 50 characters.")]
        public string Country { get; set; }

        [Required(ErrorMessage = "State is required")]
        [StringLength(50, ErrorMessage = "State should be no longer than 50 characters.")]
        public string State { get; set; }

        [Required(ErrorMessage = "City is required")]
        [StringLength(50, ErrorMessage = "City should be no longer than 50 characters.")]
        public string City { get; set; }


        [Required(ErrorMessage = " Enter your  Password")]
        [StringLength(100)]
        [Display(Name = "Password")]
        public string Password { get; set; }


        [Required(ErrorMessage = " Enter your Confirm Password")]
        [StringLength(100)]
        [Display(Name = " Confirm Password")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = " select at least Three hobbies")]
        public List<string> Hobbies { get; set; }

        public string SelectedHobbies { get; set; }


        public int Id { get; set; }
    }
}
