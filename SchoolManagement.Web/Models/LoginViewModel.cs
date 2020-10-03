using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SchoolManagement.Web.Data.Entities;

namespace SchoolManagement.Web.Models
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Username { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }

        public List<User> Users { get; set; }
    }
}
