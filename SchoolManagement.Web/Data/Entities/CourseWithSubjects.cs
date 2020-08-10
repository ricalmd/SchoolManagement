﻿using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Web.Data.Entities
{
    public class CourseWithSubjects : IEntity
    {
        public int Id { get; set; }

        [Required]
        public Subject Subject { get; set; }

        [Required]
        public Course Course { get; set; }
    }
}
