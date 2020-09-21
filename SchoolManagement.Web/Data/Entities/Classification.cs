namespace SchoolManagement.Web.Data.Entities
{
    public class Classification : IEntity
    {
        public int Id { get; set; }

        public int Score { get; set; }

        public int JustifiedAbsence { get; set; }

        public int UnjustifiedAbsence { get; set; }

        public Student Student { get; set; }

        public Discipline Discipline { get; set; }
    }
}
