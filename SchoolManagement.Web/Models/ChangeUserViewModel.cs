using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Web.Models
{
    public class ChangeUserViewModel
    {
        [Required]
        [Display(Name = "Nome")]
        public string Name { get; set; }
    }
}
