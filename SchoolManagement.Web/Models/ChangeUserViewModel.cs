using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Web.Models
{
    public class ChangeUserViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "Nome")]
        public string Name { get; set; }

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
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Telefone")]
        [MinLength(9, ErrorMessage = "O campo {0} deve conter {1} caracteres.")]
        [MaxLength(9, ErrorMessage = "O campo {0} deve conter {1} caracteres.")]
        public string Phone { get; set; }
    }
}
