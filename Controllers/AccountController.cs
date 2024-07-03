using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TenisoveTurnaje.Migrations;
using TenisoveTurnaje.Models;
using TenisoveTurnaje.ViewModels;

namespace TenisoveTurnaje.Controllers {

    [Authorize]
    public class AccountController : Controller {
        private UserManager<User> userManager;
        private SignInManager<User> signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager) {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }



        [AllowAnonymous] //odeslání vyplněného formuláře dělá uživatel jako nepřihlášený, proto AllowAnonymous
        public IActionResult Login(string returnUrl) {
            LoginVM login = new LoginVM();
            login.ReturnUrl = returnUrl;
            return View(login);
        }



        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken] // pro prevenci cross-site request forgery (útok který nutí uživ.sopuštět nevyžádané akce v appce, k níž jsou přihlášení)
        public async Task<IActionResult> Login(LoginVM login) {
            if (ModelState.IsValid) {
            
                User appUser = await userManager.FindByEmailAsync(login.UserEmail);
                if (appUser != null) {
                    await signInManager.SignOutAsync();
                    Microsoft.AspNetCore.Identity.SignInResult result = await signInManager.PasswordSignInAsync(appUser, login.Password, login.Remember, false);
                    if (result.Succeeded) {
                        return Redirect(login.ReturnUrl ?? "/");
                    }
                }
                ModelState.AddModelError(nameof(login.UserEmail), "Přihlášení selhalo: Neplatný e-mail nebo heslo!");
            }
            return View(login);
        }


        public async Task<IActionResult> Logout() {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied() {
            return View();
        }


    }




}
