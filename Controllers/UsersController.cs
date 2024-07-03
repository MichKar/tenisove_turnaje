using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;
using System.Configuration;
using TenisoveTurnaje.Migrations;
using TenisoveTurnaje.Models;
using TenisoveTurnaje.ViewModels;

namespace TenisoveTurnaje.Controllers {

    [Authorize(Roles = "admin")]
    public class UsersController : Controller {

        private UserManager<User> userManager;
        private IPasswordHasher<User> passwordHasher;
        private IPasswordValidator<User> passwordValidator;
        
        public UsersController(UserManager<User> userManager, IPasswordHasher<User> passwordHasher, IPasswordValidator<User> passwordValidator) {
            this.userManager = userManager;
            this.passwordHasher = passwordHasher;
            this.passwordValidator = passwordValidator;
        }

        public IActionResult Index() {
            return View(userManager.Users);
        }

        public ViewResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(UserVM user) {
            if (ModelState.IsValid) {
                User appUser = new User {
                    UserName = user.Name,
                    Email = user.Email
                };
                IdentityResult result = await userManager.CreateAsync(appUser, user.Password);
                if (result.Succeeded) {
                    return RedirectToAction("Index");
                } else {
                   AddErrors(result);
                }
            }
            return View(user);
        }

        [HttpGet]
        public async Task<IActionResult> Update(string id) {
            User userToEdit = await userManager.FindByIdAsync(id);
            if (userToEdit == null) {
                return View("NotFound");
            }
            return View(userToEdit);
        }

        [HttpPost]
        public async Task<IActionResult> Update(string id, string email, string password) {
            User userToEdit = await userManager.FindByIdAsync(id);
            if (userToEdit != null) {
                IdentityResult validPass;
                if (!string.IsNullOrWhiteSpace(email) && !string.IsNullOrWhiteSpace(password)) {
                    userToEdit.Email = email;
                    validPass = await passwordValidator.ValidateAsync(userManager, userToEdit, password);
                    if (validPass.Succeeded) {
                        userToEdit.PasswordHash = passwordHasher.HashPassword(userToEdit, password);
                        IdentityResult identityResult = await userManager.UpdateAsync(userToEdit);
                        if (identityResult.Succeeded) {
                            return RedirectToAction("Index");
                        }
                        else {
                            AddErrors(identityResult);
                        }
                    }
                    else {
                        AddErrors(validPass);
                    }
                }
            }
            else {
                ModelState.AddModelError("", "User not found");
            }
            return View(userToEdit);
        }

        private void AddErrors(IdentityResult identityResult) {
            foreach (var error in identityResult.Errors) {
                ModelState.AddModelError("", error.Description);
            }
        }


        [HttpPost]
        public async Task<IActionResult> Delete(string id) {
            User userToDelete = await userManager.FindByIdAsync(id);
            if (userToDelete != null) {
                var result = await userManager.DeleteAsync(userToDelete);
                if (result.Succeeded) {
                    return RedirectToAction("Index");
                }
                else {
                    AddErrors(result);
                }
            }
            else {
                ModelState.AddModelError("", "User not found");
            }
            return View("Index", userManager.Users);
        }
    }
}
