using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SchoolManagement.Web.Models
{
    public class ChangeUserViewModel
    {
        [Required]
        [Display(Name = "Nome")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Morada")]
        public string Address { get; set; }

        [Required]
        public string PostalCode { get; set; }

        [Required]
        [Display(Name = "IBAN")]
        [MinLength(25, ErrorMessage = "O campo deve conter {1} caracteres.")]
        [MaxLength(25, ErrorMessage = "O campo deve conter {1} caracteres.")]
        public string IBAN { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Telefone")]
        [MinLength(9, ErrorMessage = "O campo deve conter {1} caracteres.")]
        [MaxLength(9, ErrorMessage = "O campo deve conter {1} caracteres.")]
        public string Phone { get; set; }
    }
}
