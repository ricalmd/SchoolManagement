namespace SchoolManagement.Web.Data.Entities
{
    public class Student : IEntity
    {
        public int Id { get; set; }

        public int ClassId { get; set; }

        public Class Class { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }
    }
}
