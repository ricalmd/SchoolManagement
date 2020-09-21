using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace SchoolManagement.Web.Data.Entities
{
    public class User : IdentityUser
    {
        [Required]
        [Display(Name = "Nome")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "NIF")]
        [MinLength(9, ErrorMessage = "O campo deve conter {1} caracteres.")]
        [MaxLength(9, ErrorMessage = "O campo deve conter {1} caracteres.")]
        public string TaxpayerNumber { get; set; }

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
        [Display(Name = "Data de Nascimento")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime BirthDate { get; set; }

        [Required]                                                                                                                                             
        public string Gender { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Telefone")]
        [MinLength(9, ErrorMessage = "O campo deve conter {1} caracteres.")]
        [MaxLength(9, ErrorMessage = "O campo deve conter {1} caracteres.")]
        public string Phone { get; set; }

        [Required]
        public string Status { get; set; }

        [Display(Name = "Fotografia")]
        public string ImageUrl { get; set; }
    }
}
