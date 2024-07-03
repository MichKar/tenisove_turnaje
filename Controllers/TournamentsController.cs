using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TenisoveTurnaje.DTO;
using TenisoveTurnaje.Services;
using TenisoveTurnaje.ViewModels;

namespace TenisoveTurnaje.Controllers {
    public class TournamentsController : Controller {
        private TournamentService _tournamentService;

        public TournamentsController(TournamentService service) {
            _tournamentService = service;
        }

        public async Task<IActionResult> Index() {
            var tourVM = await _tournamentService.GetAllVMsAsync();
            return View(tourVM);
        }

        public async Task<IActionResult> Create() {
            var tournamentsDropdownsData = await _tournamentService.GetNewTournamentsDropdownsValues();
            ViewBag.Courts = new SelectList(tournamentsDropdownsData.Courts, "Id", "Name");
            return View();
        }

        //metoda pro přidání nového turnaje (tlačítko Create a new Tournament)
        [HttpPost]
        public async Task<IActionResult> CreateAsync(TournamentDTO newTournament) {
            if (!ModelState.IsValid) {
                var tournamentsDropdownsData = await _tournamentService.GetNewTournamentsDropdownsValues();
                ViewBag.Courts = new SelectList(tournamentsDropdownsData.Courts, "Id", "Name");
                return View(newTournament);
            }
            await _tournamentService.CreateAsync(newTournament);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateAsync(int id) {
            var tournamentToEdit = await _tournamentService.GetByIdAsync(id);
            if (tournamentToEdit == null) {
                return View("NotFound");
            }
            var tournamentDto = _tournamentService.ModelToDto(tournamentToEdit);
            var tournamentsDropdownData = await _tournamentService.GetNewTournamentsDropdownsValues();
            ViewBag.Courts = new SelectList(tournamentsDropdownData.Courts, "Id", "Name");
            return View(tournamentDto);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, TournamentDTO updatedTournament) {
            await _tournamentService.UpdateAsync(id, updatedTournament);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id) {
                     await _tournamentService.DeleteAsync(id);
            return RedirectToAction("Index");
        }



    }
}
