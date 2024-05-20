using GameApp.Enums;
using GameApp.Models;
using GameApp.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GameApp.Controllers
{
    public class AccountController(SignInManager<AppUser> _signInManager, UserManager<AppUser> _userManager, RoleManager<IdentityRole> _roleManager) : Controller
    {
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM vm)
        {

            if(!ModelState.IsValid) return View(vm);

            AppUser user = new AppUser
            {
                Name = vm.Name,
                Email = vm.Email,
                Surname = vm.Surname,
                UserName = vm.Username,
            };

            IdentityResult result =  await _userManager.CreateAsync(user, vm.Password);

            if(!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            await _userManager.AddToRoleAsync(user, UserRoles.Member.ToString());
            await _userManager.AddToRoleAsync(user, UserRoles.Admin.ToString());

            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginVM vm)
        {

            if (!ModelState.IsValid) return View();

            AppUser user = await _userManager.FindByNameAsync(vm.UsernameOrEmail);

            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(vm.UsernameOrEmail);
                if (user == null)
                {
                    ModelState.AddModelError("", "Username or Password is false.");
                }
            }

            if (!ModelState.IsValid) return View();

            await _signInManager.CheckPasswordSignInAsync(user, vm.Password, true);
            var result = await _signInManager.PasswordSignInAsync(user, vm.Password, vm.RememberMe, true);

            if (result.IsLockedOut)
            {
                ModelState.AddModelError("", "You try more time. In that case, you must wait for " + user.LockoutEnd.Value.ToString("HH:mm:ss"));
                return View(vm);
            }

            return RedirectToAction(controllerName:"Home", actionName:"Index");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction(controllerName: "Home", actionName: "Index");
        }

        public async Task<IActionResult> CreateRole()
        {
            foreach (UserRoles role in Enum.GetValues(typeof(UserRoles)))
            {
                if(!await _roleManager.RoleExistsAsync(role.ToString()))
                {
                    await _roleManager.CreateAsync(new IdentityRole
                    {
                        Name = role.ToString(),
                    });
                }
            }
            return Content("OK");
        }
    }
}
