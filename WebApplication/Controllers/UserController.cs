using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationViewModel> userManager;
        private readonly SignInManager<ApplicationViewModel> signInManager;
        private readonly IWebHostEnvironment hostingEnvironment;

        public UserController(UserManager<ApplicationViewModel> userManager, SignInManager<ApplicationViewModel> signInManager,
            IWebHostEnvironment hostingEnvironment
            )
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.hostingEnvironment = hostingEnvironment;
        }
        [HttpGet][HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> IsEmailExist(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if(user==null)
            {
                return Json(true);

            }
            else
            {
                return Json($"Email: {email} already exists");
            }



        }
        [HttpGet]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> IsUserNameExist(string name)
        {
            var user = await userManager.FindByNameAsync(name);
            if (user == null)
            {
                return Json(true);

            }
            else
            {
                return Json($"Username: {name} already exists");
            }



        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            string uniqueFileName = null;
            if (model.Photo != null)
            {
                var filename = Path.GetFileName(model.Photo.FileName);
                var encoded = HttpUtility.HtmlEncode(filename);
                string uploadFolder = Path.Combine(hostingEnvironment.WebRootPath, "images");
                 uniqueFileName = Guid.NewGuid().ToString() + "_" +encoded ;
                string newPath = Path.Combine(uploadFolder,  uniqueFileName);
                
                model.Photo.CopyTo(new FileStream(newPath, FileMode.Create));

            }



            if (ModelState.IsValid)
            {
               

                var user = new ApplicationViewModel { UserName = model.Name,
                    Email = model.Email,
                    City = model.City,
                    Address = model.Address,
                    PhoneNumber = model.Number,
                    DateOfBirth = model.DateOfBirth,
                    ImagePath = uniqueFileName,
                    PasswordHash=model.Password
  
                };

                var result = await userManager.CreateAsync(user,model.Password);
                if(result.Succeeded)
                {
                    return RedirectToAction("Login", "User");

                }
                foreach (var errors in result.Errors)
                {
                    ModelState.AddModelError("", errors.Description);

                }

            }
            return View(model);
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.UserName, model.Password,model.RememberMe,false);
                if (result.Succeeded)
                {
                    //var user = await userManager.FindByNameAsync(User.Identity.Name);
                    return RedirectToAction("Display", "Account");

                }
           
                
                    ModelState.AddModelError("", "Invalid Login attempt");

                

            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {

            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "User");

        }
    }
}
