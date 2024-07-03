using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TenisoveTurnaje.DTO;
using TenisoveTurnaje.Models;
using TenisoveTurnaje.Services;

namespace TenisoveTurnaje.Controllers {
    public class CourtsController : Controller {
        private CourtService _courtService;

        public CourtsController(CourtService service) {
            _courtService = service;
        }

        // GET: Courts
        public async Task<IActionResult> Index() {
            return View(await _courtService.GetAllAsync());
        }


        // GET: Courts/Create
        public IActionResult Create() {
            return View();
        }

        // POST: Courts/Create
        [HttpPost]
        public async Task<IActionResult> CreateAsync(CourtDTO newCourt) {
            await _courtService.CreateAsync(newCourt);
            return RedirectToAction("Index");
        }

        // GET: Courts/Edit/5
        public async Task<IActionResult> UpdateAsync(int id) {
            var courtToEdit = await _courtService.GetByIdAsync(id);
            if (courtToEdit == null) {
                return View("NotFound");
            }
             return View(courtToEdit);
        }

        // POST: Courts/Edit/5
        [HttpPost]
       public async Task<IActionResult> Update(int id, CourtDTO court) {
            await _courtService.UpdateAsync(id, court);
            return RedirectToAction("Index");
        }

        // GET: Courts/Delete/5
        public async Task<IActionResult> Delete(int id) {
            var courtToDelete = await _courtService.GetByIdAsync(id);
            if (courtToDelete == null) {
                return View("NotFound");
            }
            await _courtService.DeleteAsync(id);
            return RedirectToAction("Index");
        }

    }
}
