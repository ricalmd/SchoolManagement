using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SchoolManagement.Web.Data.Entities;
using SchoolManagement.Web.Helpers;

namespace SchoolManagement.Web.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        private Random _random;

        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
            _random = new Random();
        }

        public UserManager<User> UserManager { get; }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();

            await _userHelper.CheckRoleAsync("Admin");
            await _userHelper.CheckRoleAsync("Student");
            
            var user = await _userHelper.GetUserByEmailAsync("ricardo.formando.cinel@gmail.com");
            if (user == null)
            {
                user = new User
                {
                    Name = "Ricardo Nuno Neiva de Admin",
                    TaxpayerNumber = "123456789",
                    Address = "Rua da Avenida, n.º 1",
                    PostalCode = "1111-111 Lisboa",
                    IBAN = "PT1234567890",
                    BirthDate = Convert.ToDateTime("25/11/82"),
                    Gender = "Masculino",
                    Email = "ricardo.formando.cinel@gmail.com",
                    UserName = "ricardo.formando.cinel@gmail.com",
                    PhoneNumber = "123123123"
                };
                var result = await _userHelper.AddUserAsync(user, "123456");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Utilizador não pode ser criado no seeder");
                }
                var isInRole = await _userHelper.IsUserInRoleAsync(user, "Admin");
                if (!isInRole)
                {
                    await _userHelper.AddUserToRoleAsync(user, "Admin");
                }
            }

            if (!_context.Subjects.Any())
            {
                AddSubject("Matemática", user);
                AddSubject("Português", user);
                AddSubject("História", user);

                await _context.SaveChangesAsync();
            }

            if (!_context.Courses.Any())
            {
                AddCourses("Arqueologia", user);

                await _context.SaveChangesAsync();
            }

            if (!_context.CoursesWithSubjects.Any())
            {
                AddCourseWithSubjects(user);
                await _context.SaveChangesAsync();
            }
        }

        private void AddCourses(string name, User user)
        {
             _context.Courses.Add(new Course 
             {
                Name = name,
                QNQ = "4",
                QEQ = "4",
                Profile = "Ter estofo para usar picareta!",
                Reference = "1234",
                User = user
             });
        }

        private void AddSubject(string name, User user)
        {
            _context.Subjects.Add(new Subject
            {
                Name = name,
                Workload = _random.Next(51),
                Code = Convert.ToString(_random.Next(1000)),
                Credit = 25,
                Objectiv = "Testar isto",
                Content = "Testar",
                User = user
            });
        }

        private void AddCourseWithSubjects(User user)
        {
            _context.CoursesWithSubjects.Add(new CourseWithSubjects 
            {
                SubjectId = 1,
                CourseId = 2
            });
        }
    }
}
