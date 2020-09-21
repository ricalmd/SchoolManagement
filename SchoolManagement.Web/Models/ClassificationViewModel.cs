using System.Collections.Generic;
using SchoolManagement.Web.Data.Entities;

namespace SchoolManagement.Web.Models
{
    public class ClassificationViewModel
    {
        public User User { get; set; }

        public List<Classification> Classifications { get; set; }
    }
}
