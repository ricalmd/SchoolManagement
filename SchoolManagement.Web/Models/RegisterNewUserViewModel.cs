using System.ComponentModel.DataAnnotations;
using SchoolManagement.Web.Data.Entities;

namespace SchoolManagement.Web.Models
{
    public class RegisterNewUserViewModel : User
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string Confirm { get; set; }
    }
}
