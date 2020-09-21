using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SchoolManagement.Web.Data.Entities;

namespace SchoolManagement.Web.Models
{
    public class ClassWithStudentsViewModel : Class
    {
        public List<User> Users { get; set; }
    }
}
