using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TenisoveTurnaje.Migrations;
using TenisoveTurnaje.Models;

namespace TenisoveTurnaje.Controllers {
    public class RolesController : Controller {


        private RoleManager<IdentityRole> roleManager;
        private UserManager<User> userManager;

        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<User> userManager) {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public IActionResult Index() {
            return View(roleManager.Roles);
        }

        private void AddErrors(IdentityResult identityResult) {
            foreach (IdentityError error in identityResult.Errors) {
                ModelState.AddModelError("", error.Description);
            }
        }

        public IActionResult Create() {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> CreateAsync(string name) {
            if (ModelState.IsValid) {
                IdentityResult result = await roleManager.CreateAsync(new IdentityRole(name));
                if (result.Succeeded) {
                    return RedirectToAction("Index");
                }
                else {
                    AddErrors(result);
                }
            }
            return View(name);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAsync(string id) {
            var roleToDelete = await roleManager.FindByIdAsync(id);
            if (roleToDelete != null) {
                IdentityResult result = await roleManager.DeleteAsync(roleToDelete);
                if (result.Succeeded) {
                    return RedirectToAction("Index");
                }
                else {
                    AddErrors(result);
                }
            }
            ModelState.AddModelError("", "Role nenalezena.");
            return View("Index", roleManager.Roles);
        }

        public async Task<IActionResult> Update(string id) {
           var role = await roleManager.FindByIdAsync(id);
            if (role == null) {
                return View("NotFound");
            }
            List<User> members = new List<User>();
            List<User> nonmembers = new List<User>();
            var users = await userManager.Users.ToListAsync();
            foreach (User user in users) {
                
                var list = await userManager.IsInRoleAsync(user, role.Name) ? members : nonmembers;
                list.Add(user);
            }
            return View(new RoleEdit {
                Role = role,
                RoleMembers = members,
                RoleNonMembers = nonmembers
            });

        }

        [HttpPost]
        public async Task<IActionResult> Update(RoleModifications roleModification) {
            User user;
            IdentityResult result;
            //if (!ModelState.IsValid) {
            //    return View(roleModification);
            //}
            foreach (var id in roleModification.AddIds ?? new string[] { }) {
                user = await userManager.FindByIdAsync(id);
                if (user != null) {
                    result = await userManager.AddToRoleAsync(user, roleModification.RoleName);
                    if (!result.Succeeded) {
                        AddErrors(result);
                    }
                }
            }

            foreach (var id in roleModification.DeleteIds ?? new string[] { }) {
                user = await userManager.FindByIdAsync(id);
                if (user != null) {
                    result = await userManager.RemoveFromRoleAsync(user, roleModification.RoleName);
                    if (!result.Succeeded) {
                        AddErrors(result);
                    }
                }
            }
            return RedirectToAction("Index");
        }





    }
}
