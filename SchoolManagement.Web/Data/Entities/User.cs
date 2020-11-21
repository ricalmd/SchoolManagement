using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace SchoolManagement.Web.Data.Entities
{
    public class User : IdentityUser
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "Nome")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "NIF")]
        [MinLength(9, ErrorMessage = "O campo {0} deve conter {1} caracteres.")]
        [MaxLength(9, ErrorMessage = "O campo {0} deve conter {1} caracteres.")]
        public string TaxpayerNumber { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "Morada")]
        public string Address { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "Código Postal")]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "IBAN")]
        [MinLength(25, ErrorMessage = "O campo {0} deve conter {1} caracteres.")]
        [MaxLength(25, ErrorMessage = "O campo {0} deve conter {1} caracteres.")]
        public string IBAN { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "Data de Nascimento")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]   
        [Display(Name = "Género")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Telefone")]
        [MinLength(9, ErrorMessage = "O campo deve conter {1} caracteres.")]
        [MaxLength(9, ErrorMessage = "O campo deve conter {1} caracteres.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Status { get; set; }

        [Display(Name = "Fotografia")]
        public string ImageUrl { get; set; }
    }
}
