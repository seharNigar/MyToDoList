using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Models;
using Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication.Controllers
{   [Authorize]
  
    public class AccountController : Controller
    {
        private readonly IListData data;
        private readonly UserManager<ApplicationViewModel> _userManager;

        public AccountController(IListData data, 
            UserManager<ApplicationViewModel> userManager)
        {
            this.data = data;
            _userManager = userManager;
        }
       [HttpGet]
        public async Task<IActionResult> Display()
        {
            
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            ViewBag.name = user.UserName;
            var TempList = data.GetToDoListByUserId(user.Id.ToString());
            if (TempList != null)
            {
                data.commit();
               
            }

            return View(TempList);
           
        }
        [HttpPost]
        public IActionResult Display(ToDoList model)
        {
            return RedirectToAction("Create", "Account");
        }

        [HttpGet]
        public  IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ToDoList model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);


                var temp = new ToDoList()
                {
                    Name = model.Name,
                    Duration = model.Duration,
                    UserId = user.Id
                };
                data.Add(temp);
                data.commit();

                return RedirectToAction("Display", "Account");
            }
            return View(model);


        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var model = data.GetById(id);
            return View(model);
        }
        [HttpPost]
        public  IActionResult Edit(ToDoList model)
        {
            if (ModelState.IsValid)
            {

                data.update(model);
                data.commit();
                return RedirectToAction("Display", "Account");
            }
            return View(model);

        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            data.Delete(id);
            data.commit();
            return RedirectToAction("Display", "Account");
        }

     

    }
}
