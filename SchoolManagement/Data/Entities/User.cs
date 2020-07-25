using System;
using Microsoft.AspNetCore.Identity;

namespace SchoolManagement.Data.Entities
{
    public class User : IdentityUser
    {
        public string Name { get; set; }

        public string TaxpayerNumber { get; set; }

        public string Address { get; set; }

        public string PostalCode { get; set; }

        public string IBAN { get; set; }

        public DateTime BirthDate { get; set; }

        public string Gender { get; set; }
    }
}
