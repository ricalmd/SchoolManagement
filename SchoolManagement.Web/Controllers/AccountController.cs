using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SchoolManagement.Web.Data.Entities;
using SchoolManagement.Web.Data.Repositories;
using SchoolManagement.Web.Helpers;
using SchoolManagement.Web.Models;

namespace SchoolManagement.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserHelper _userHelper;
        private readonly IConfiguration _configuration;
        private readonly IStudentRepository _studentRepository;
        private readonly IClassRepository _classRepository;
        private readonly IImageHelper _imageHelper;
        private readonly IMailHelper _mailHelper;

        public AccountController(
            IUserHelper userHelper,
            IConfiguration configuration,
            IStudentRepository studentRepository,
            IClassRepository classRepository,
            IImageHelper imageHelper,
            IMailHelper mailHelper)
        {
            _userHelper = userHelper;
            _configuration = configuration;
            _studentRepository = studentRepository;
            _classRepository = classRepository;
            _imageHelper = imageHelper;
            _mailHelper = mailHelper;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>ChangePassword</returns>
        public IActionResult ChangePassword()
        {
            return this.View();
        }

        /// <summary>
        /// It receives as an parameter an object of the ChangePasswordViewModel class. If the validation
        /// of the view is successful, a variable, user, receives the value of the User property through
        /// the GetUserByEmailAsync() method. If user is null, a view is created with an error message, 
        /// otherwise a result variable is created with the parameters of the ChangePasswordAsync() method.
        /// If the operation was successful, it is switched to the ChangeUser view.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>ChangePassword view with values</returns>
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                if (user != null)
                {
                    var result = await _userHelper.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return this.RedirectToAction("ChangeUser");
                    }
                    else
                    {
                        this.ModelState.AddModelError(string.Empty, result.Errors.FirstOrDefault().Description);
                    }
                }
                else
                {
                    this.ModelState.AddModelError(string.Empty, "Utilizador não encontrado");
                }
            }

            return this.View(model);
        }

        public IActionResult ChangeStatus()
        {
            var model = new ChangeStatusViewModel();
            model.Email = _userHelper.GetComboUsers();
            model.Class = _studentRepository.GetComboClasses();

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeStatus(ChangeStatusViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByIdAsync(model.EmailId);
                await _userHelper.AddUserToRoleAsync(user, model.Status, user.Status);

                if (user.Status == "Aluno")
                {
                    var student = new Student
                    {
                        UserId = user.Id,
                        ClassId = model.ClassId
                    };
                    await _studentRepository.CreateAsync(student);
                }

                if (user != null)
                {
                    user.Status = model.Status;

                    var response = await _userHelper.UpdateUserAsync(user);
                    if (response.Succeeded)
                    {
                        this.ViewBag.UserMessage = "Utilizador atualizado";
                    }
                    else
                    {
                        this.ModelState.AddModelError(string.Empty, response.Errors.FirstOrDefault().Description);
                    }

                    _mailHelper.SendMail(user.UserName, "Informação", $"<h1>O tipo de utilizador foi alterado para" +
                        $" {model.Status}</h1>");
                }
                else
                {
                    this.ModelState.AddModelError(string.Empty, "Utilizador não encontrado");
                }
            }

            return this.View(model);
        }

        public async Task<IActionResult> ChangeUser()
        {
            var user = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
            var model = new ChangeUserViewModel(); 

            if (user != null)
            {
                model.Name = user.Name;
                model.IBAN = user.IBAN;
                model.Phone = user.Phone;
                model.PostalCode = user.PostalCode;
                model.Address = user.Address;
            }
            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeUser(ChangeUserViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name); 
                if (user != null)
                {
                    user.Name = model.Name;
                    user.IBAN = model.IBAN;
                    user.Phone = model.Phone;
                    user.PostalCode = model.PostalCode;
                    user.Address = model.Address;
                    
                    var response = await _userHelper.UpdateUserAsync(user);
                    if (response.Succeeded)
                    {
                        this.ViewBag.UserMessage = "Utilizador atualizado";
                    }
                    else
                    {
                        this.ModelState.AddModelError(string.Empty, response.Errors.FirstOrDefault().Description);
                    }
                }
                else
                {
                    this.ModelState.AddModelError(string.Empty, "Utilizador não encontrado");
                }
            }

            return this.View(model);
        }

        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token))
            {
                return this.NotFound();
            }

            var user = await _userHelper.GetUserByIdAsync(userId);
            if (user == null)
            {
                return this.NotFound();
            }

            var result = await _userHelper.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
            {
                return this.NotFound();
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateToken([FromBody] LoginViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(model.Username);
                if (user != null)
                {
                    var result = await _userHelper.ValidatePasswordAsync(
                        user,
                        model.Password);

                    if (result.Succeeded)
                    {
                        var claims = new[]
                        {
                            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                        };

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));
                        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                        var token = new JwtSecurityToken(
                            _configuration["Tokens:Issuer"],
                            _configuration["Tokens:Audience"],
                            claims,
                            expires: DateTime.UtcNow.AddDays(15),
                            signingCredentials: credentials);
                        var results = new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo
                        };

                        return this.Created(string.Empty, results);
                    }
                }
            }

            return this.BadRequest();
        }

        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userHelper.LoginAsync(model);
                if (result.Succeeded)
                {
                    if (Request.Query.Keys.Contains("ReturnUrl"))
                    {
                        return Redirect(Request.Query["ReturnUrl"].First());
                    }

                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError(string.Empty, "Failed to login");
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _userHelper.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult RecoverPassword()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> RecoverPassword(RecoverPasswordViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(model.Email);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "O email não corresponde a um utilizador registado.");
                    return this.View(model);
                }

                var myToken = await _userHelper.GeneratePasswordResetTokenAsync(user);

                var link = this.Url.Action(
                    "ResetPassword",
                    "Account",
                    new { token = myToken }, protocol: HttpContext.Request.Scheme);

                _mailHelper.SendMail(model.Email, "Alteração da password", $"<h1>Alteração da password</h1>" +
                    $"Para alterar a password, use este link:</br></br>" +
                    $"<a href = \"{link}\">Alterar Password</a>");
                this.ViewBag.Message = "As instruções para recuperar a password, foram enviadas por email.";
                return this.View();
            }
            return this.View(model);
        }

        [Authorize(Roles = "Administrativo")]
        public IActionResult Register()
        {
            var model = new RegisterNewUserViewModel();
            model.Class = _studentRepository.GetComboClasses();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterNewUserViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = await _userHelper.GetUserByEmailAsync(model.Username);

                var path = string.Empty;

                if (model.Photo != null && model.Photo.Length > 0)
                {
                    path = await _imageHelper.UploadImageAsync(model.Photo, "Photos");
                }

                if (user == null)
                {
                    user = new User
                    {
                        Name = model.Name,
                        TaxpayerNumber = model.TaxpayerNumber,
                        Address = model.Address,
                        PostalCode = model.PostalCode,
                        IBAN = model.IBAN,
                        BirthDate = model.BirthDate,
                        Gender = model.Gender,
                        Email = model.Username,
                        Phone = model.Phone,
                        UserName = model.Username,
                        Status = model.Status,
                        ImageUrl = path
                    };

                    var result = await _userHelper.AddUserAsync(user, model.Password);
                    await _userHelper.AddUserToRoleAsync(user, model.Status, string.Empty);

                    if (user.Status == "Aluno")
                    {
                        var student = new Student
                        {
                            UserId = user.Id,
                            ClassId = model.ClassId
                        };
                        await _studentRepository.CreateAsync(student);
                    }

                    if (result != IdentityResult.Success)
                    {
                        this.ModelState.AddModelError(string.Empty, "O utilizador não pode ser criado");
                        return this.View(model);
                    }
                    var myToken = await _userHelper.GenerateEmailConfirmationTokenAsyc(user);
                    var tokenLink = this.Url.Action("ConfirmEmail", "Account", new
                    {
                        userid = user.Id,
                        token = myToken
                    }, protocol: HttpContext.Request.Scheme);

                    _mailHelper.SendMail(model.Username, "Email de confirmação", $"<h1>Email de confirmação</h1>" +
                        $"Para confirmar a abertura de conta, " +
                        $"favor de abrir este link:</br></br><a href = \"{tokenLink}\">Confirmar email</a>");
                    this.ViewBag.Message = "As instruções para completar a abertura de conta, foram enviadas por email.";
                    
                    return this.View(model);
                }
                this.ModelState.AddModelError(string.Empty, "O nome de utilizador já está registado");
            }
            return this.View(model);
        }

        public IActionResult ResetPassword(string token)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            var user = await _userHelper.GetUserByEmailAsync(model.UserName);
            if (user != null)
            {
                var result = await _userHelper.ResetPasswordAsync(user, model.Token, model.Password);
                if (result.Succeeded)
                {
                    this.ViewBag.Message = "Password alterada com sucesso.";
                    return this.View();
                }

                this.ViewBag.Message = "Erro na alteração do email.";
                return View(model);
            }

            this.ViewBag.Message = "Utilizador não encontrado.";
            return View(model);
        }
    }
}
