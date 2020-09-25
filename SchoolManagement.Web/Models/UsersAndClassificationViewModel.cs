using SchoolManagement.Web.Data.Entities;

namespace SchoolManagement.Web.Models
{
    public class UsersAndClassificationViewModel : Classification
    {
        public User User { get; set; }

        public Class Class { get; set; }
    }
}
