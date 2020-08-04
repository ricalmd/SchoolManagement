using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SchoolManagement.Data.Entities;
using SchoolManagement.Helpers;

namespace SchoolManagement.Data
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

            var user = await _userHelper.GetUserByEmailAsync("ricardo.formando.cinel@gmail.com");
            if (user == null)
            {
                user = new User
                {
                    Name = "Ricardo Nuno Neiva de Almeida",
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
                if(result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Utilizador não pode ser criado no seeder");
                }
            }

            if (!_context.Subjects.Any())
            {
                this.AddSubject("Matemática", user);
                this.AddSubject("Português", user);
                this.AddSubject("História", user);

                await _context.SaveChangesAsync();
            }
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
                Reference = Convert.ToString(_random.Next(2000)),
                User = user
            }); 
        }
    }
}
