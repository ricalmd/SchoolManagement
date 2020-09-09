using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolManagement.Web.Data.Entities;

namespace SchoolManagement.Web.Models
{
    public class ClassAndCourseViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Designação")]
        public string NameClass { get; set; }

        [Required]
        [Display(Name = "Início")]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime BeginSchedule { get; set; }

        [Required]
        [Display(Name = "Fim")]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime EndSchedule { get; set; }

        [Display(Name = "Curso")]
        [Range(1, int.MaxValue, ErrorMessage = "Deve selecionar um curso")]
        public int CourseId { get; set; }

        public IEnumerable<SelectListItem> Courses { get; set; }

        public User User { get; set; }
    }
}
