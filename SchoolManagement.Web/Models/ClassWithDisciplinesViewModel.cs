using SchoolManagement.Web.Data.Entities;

namespace SchoolManagement.Web.Models
{
    public class ClassWithDisciplinesViewModel : Discipline
    {
        public int ClassId { get; set; }
    }
}
