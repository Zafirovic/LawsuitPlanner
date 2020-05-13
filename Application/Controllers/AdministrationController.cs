using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Models;
using Application.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdministrationController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<User> userManager;

        public AdministrationController(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        [HttpGet]
        public ActionResult EntitiesManagement()
        {
            return View();
        }

        [HttpGet]
        public ActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if(ModelState.IsValid)
            {
                IdentityRole iRole = new IdentityRole { Name = model.roleName };

                IdentityResult result = await roleManager.CreateAsync(iRole);
                
                if(result.Succeeded)
                {
                    return RedirectToAction("ListRoles", "Administration");
                }

                foreach(IdentityError err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult ListRoles()
        {
            var roles = roleManager.Roles;
            return View(roles);
        }

        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Uloga sa id = {id} nije pronadjena.";
                return View("NotFound");
            }

            var model = new EditRoleViewModel
            {
                id = role.Id,
                roleName = role.Name,
            };
            
            foreach(var user in userManager.Users)
            {
                if(await userManager.IsInRoleAsync(user, role.Name))
                {
                    model.users.Add(user.UserName);
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            var role = await roleManager.FindByIdAsync(model.id);

            if(role == null)
            {
                ViewBag.ErrorMessage = $"Uloga sa id = {model.id} nije pronadjena.";
                return View("NotFound");
            }
            else
            {
                role.Name = model.roleName;
                var result = await roleManager.UpdateAsync(role);

                if(result.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }

                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return ViewBag(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            if (role == null)
            {
                ViewBag.ErrorMessage = $"Uloga sa id = {id} nije pronadjena.";
                return View("NotFound");
            } 
            else
            {
                var result = await roleManager.DeleteAsync(role);
                
                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }

                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(role);
            } 
        }

        [HttpGet]
        public async Task<IActionResult> EditUsersInRole(string roleID)
        {
            ViewBag.roleID = roleID;

            var role = await roleManager.FindByIdAsync(roleID);

            if(role == null)
            {
                ViewBag.ErrorMessage = $"Uloga sa id = {roleID} nije pronadjena";
                return View("NotFound");
            }
            
            var model = new List<UserRoleViewModel>();
            
            foreach(var user in userManager.Users)
            {
                var userRoleViewModel = new UserRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };

                if(await userManager.IsInRoleAsync(user, role.Name))
                {
                    userRoleViewModel.IsSelected = true;
                }
                else
                {
                    userRoleViewModel.IsSelected = false;
                }

                model.Add(userRoleViewModel);
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUsersInRole(List<UserRoleViewModel> model, string roleID)
        {
            var role = await roleManager.FindByIdAsync(roleID);

            if(role == null)
            {
                ViewBag.ErrorMessage = $"Uloga sa id = {roleID} nije pronadjena";
                return View("NotFound");
            }

            for(int i = 0; i < model.Count; i++)
            {
                var user = await userManager.FindByIdAsync(model[i].UserId);

                IdentityResult result = null;

                if (model[i].IsSelected && !(await userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!model[i].IsSelected && await userManager.IsInRoleAsync(user, role.Name))
                {
                    result  = await userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                    continue;

                if(result.Succeeded)
                {
                    if(i < (model.Count - 1))
                        continue;
                    else
                        return RedirectToAction("EditRole", new { Id = roleID });
                }
            }

            return RedirectToAction("EditRole", new { id = roleID });
        }

        [HttpGet]
        public ActionResult ListUsers()
        {
            var users = userManager.Users;
            return View(users);
        }

        [HttpGet]
        public async Task<IActionResult> EditUser(string Id)
        {
            var user = await userManager.FindByIdAsync(Id);

            if(user == null)
            {
                ViewBag.ErrorMessage = $"Korisnik sa id = {Id} ne postoji u sistemu.";
                return View("NotFound");
            }

            var userRoles = await userManager.GetRolesAsync(user);

            var model = new EditUserViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                email = user.Email,
                name = user.name,
                surname = user.surname,
                roles = userRoles
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(EditUserViewModel model)
        {
            if(ModelState.IsValid)
            {
                var user = await userManager.FindByIdAsync(model.Id);

                if(user == null)
                {
                    ViewBag.ErrorMessage = $"Korisnik sa id = {model.Id} ne postoji u sistemu.";
                    return View("NotFound");
                }
                else
                {
                    user.name = model.name;
                    user.surname = model.surname;
                    user.Email = model.email;
                    user.UserName = model.UserName;
                    if (model.password != null)
                    {
                        await userManager.RemovePasswordAsync(user);
                        await userManager.AddPasswordAsync(user, model.password);
                    }

                    var result =  await userManager.UpdateAsync(user);

                    if(result.Succeeded)
                    {
                        return RedirectToAction("ListUsers");
                    }

                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            
            if(user == null)
            {
                ViewBag.ErrorMessage = $"Korisnik sa id = {id} nije pronadjen.";
                return View("NotFound");
            }
            else
            {
                var result = await userManager.DeleteAsync(user);

                if(result.Succeeded)
                {
                    return RedirectToAction("ListUsers");
                }

                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View("ListUsers");
            }
        }
    }

}