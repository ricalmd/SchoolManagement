﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public int ClassId { get; set; }

        [Display(Name = "Lista de turmas")]
        public IEnumerable<SelectListItem> Class { get; set; }

        [Display(Name = "Fotografia")]
        public IFormFile Photo { get; set; }
    }
}
