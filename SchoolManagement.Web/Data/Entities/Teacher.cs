namespace SchoolManagement.Web.Data.Entities
{
    public class Teacher : IEntity
    {
        public int Id { get; set; }

        public int DisciplineId { get; set; }

        public Discipline Discipline { get; set; }

        public User User { get; set; }
    }
}
