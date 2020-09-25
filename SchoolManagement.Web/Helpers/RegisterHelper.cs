using System.Threading.Tasks;
using SchoolManagement.Web.Data.Entities;
using SchoolManagement.Web.Data.Repositories;

namespace SchoolManagement.Web.Helpers
{
    public class RegisterHelper : IRegisterHelper
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IDisciplineRepository _disciplineRepository;
        private readonly IClassificationRepository _classificationRepository;
        private readonly ITeacherRepository _teacherRepository;

        public RegisterHelper(
            IStudentRepository studentRepository,
            IDisciplineRepository disciplineRepository,
            IClassificationRepository classificationRepository,
            ITeacherRepository teacherRepository)
        {
            _studentRepository = studentRepository;
            _disciplineRepository = disciplineRepository;
            _classificationRepository = classificationRepository;
            _teacherRepository = teacherRepository;
        }

        public async Task AddStudentAsync(User user, int id)
        {
            var std = _studentRepository.GetStudent(id, user.Id);

            if (std == null)
            {
                var student = new Student
                {
                    UserId = user.Id,
                    ClassId = id
                };
                await _studentRepository.CreateAsync(student);

                var item = _disciplineRepository.GetDisciplinesFromClass(student.ClassId);

                foreach (var elemen in item)
                {
                    var cl = new Classification
                    {
                        DisciplineId = elemen.Id,
                        StudentId = student.Id
                    };
                    await _classificationRepository.CreateAsync(cl);
                }
            }
        }

        public async Task AddTeacherAsync(User user, int id)
        {
            var tcr = _teacherRepository.GetTeacher(id, user.Id);

            if (tcr == null)
            {
                var teacher = new Teacher
                {
                    User = user,
                    DisciplineId = id
                };
                await _teacherRepository.CreateAsync(teacher);
            }
        }
    }
}
