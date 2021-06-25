using Attendance.Models;
using Attendance.ViewModels.Forms;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Attendance.Controllers
{

    public class AuthController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly DataContext _ctx;

        public AuthController(SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager,
            DataContext ctx)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _ctx = ctx;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegisterForm());
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterForm form)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Something not correct");
                return View();
            }

            var user = new IdentityUser(form.Username) { Email = form.Email };
            var createUserResult = await _userManager.CreateAsync(user, form.Password);
            if (createUserResult.Succeeded)
            {
                if (form.Role == Role.Teacher)
                {
                    await _userManager.AddClaimAsync(user, new Claim(
                        AttendanceConstants.Claims.Role,
                        AttendanceConstants.Roles.Teacher));

                    _ctx.Add(new User
                    {
                        Id = user.Id,
                        Email = user.Email,
                        Name = form.Name,
                        Role = Role.Teacher
                    });
                }
                else if (form.Role == Role.Student)
                {

                    await _userManager.AddClaimAsync(user, new Claim(
                        AttendanceConstants.Claims.Role,
                        AttendanceConstants.Roles.Student));

                    _ctx.Add(new User
                    {
                        Id = user.Id,
                        Email = user.Email,
                        Name = form.Name,
                        Role = Role.Student
                    });
                }
                await _ctx.SaveChangesAsync();
                await _signInManager.SignInAsync(user, true);
                return RedirectToAction("Index", "Home");
            }

            // For errors
            foreach (var error in createUserResult.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginForm());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginForm form)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "please enter valid username and password");
                return View();
            }

            var result = await _signInManager.PasswordSignInAsync(form.Username, form.Password, false, false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "username or password is incorrect");
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
