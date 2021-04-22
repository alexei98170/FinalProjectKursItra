using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinalProjectKursItra.Models;
using FinalProjectKursItra.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FinalProjectKursItra.Controllers
{
    public class UsersController : Controller
    {
        UserManager<ApplicationUser> _userManager;
        RoleManager<IdentityRole> _roleManager;

        public UsersController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Edit(string userName)
        {
            ApplicationUser user = await _userManager.FindByNameAsync(userName);
            if (user == null)
            {
                return NotFound();
            }

            var userRoles = await _userManager.GetRolesAsync(user);
            var allRoles = _roleManager.Roles.ToList();

            EditUserViewModel model = new EditUserViewModel { UserName = userName, Email = user.Email, AllRoles = allRoles, UserRoles = userRoles.ToList() };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel model, List<string> roles)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await _userManager.FindByNameAsync(model.UserName);

                if (user != null)
                {
                    user.Email = model.Email;
                    user.UserName = model.UserName;
                    var userRoles = await _userManager.GetRolesAsync(user);
                   
                    var allRoles = _roleManager.Roles.ToList();
                   
                    var addedRoles = roles.Except(userRoles);
                 
                    var removedRoles = userRoles.Except(roles);

                    await _userManager.AddToRolesAsync(user, addedRoles);

                    await _userManager.RemoveFromRolesAsync(user, removedRoles);

                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
            }
            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<ActionResult> DeleteAsync(string id)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Delete(string id)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id);
            return View(user);
        }

        [HttpPost]
        [ActionName("Block")]
        public async Task<ActionResult> BlockAsync(string id)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                user.IsBlocked = true;
                await _userManager.UpdateAsync(user);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Block(string id)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id);
            return View(user);
        }

        [HttpPost]
        [ActionName("UnBlock")]
        public async Task<ActionResult> UnBlockAsync(string id)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                user.IsBlocked = false;
                await _userManager.UpdateAsync(user);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> UnBlock(string id)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id);
            return View(user);
        }

        [HttpPost]
        [ActionName("Appoint")]
        public async Task<ActionResult> AppointAsync(string id)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                if (await _userManager.IsInRoleAsync(user, "admin"))
                {
                    await _userManager.RemoveFromRoleAsync(user, "admin");
                    await _userManager.AddToRoleAsync(user, "author");
                } else if(await _userManager.IsInRoleAsync(user, "member")) {
                    await _userManager.RemoveFromRoleAsync(user, "member");
                    await _userManager.AddToRoleAsync(user, "author");
                } else if (await _userManager.IsInRoleAsync(user, "author"))
                {
                    await _userManager.RemoveFromRoleAsync(user, "author");
                    await _userManager.AddToRoleAsync(user, "admin");
                }
           
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Appoint(string id)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id);
            return View(user);
        }

        [Authorize(Roles = "admin")]
        public ViewResult Index()
        {
            ViewBag.UserManager = _userManager;
            return View(_userManager.Users.ToList());
        }

    }
}