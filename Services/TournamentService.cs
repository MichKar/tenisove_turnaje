using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;
using TenisoveTurnaje.DTO;
using TenisoveTurnaje.Models;
using TenisoveTurnaje.ViewModels;

namespace TenisoveTurnaje.Services {
    public class TournamentService {
        private ApplicationDbContext _dbContext;

        public TournamentService(ApplicationDbContext dbContext) {
            _dbContext = dbContext;
        }


        public async Task<IEnumerable<TournamentsVM>> GetAllVMsAsync() {
            List<Tournament> tournaments = await _dbContext.Tournaments.Include(t => t.Court).ToListAsync();
            List<TournamentsVM> tourVMs = new List<TournamentsVM>();
            foreach (Tournament tournament in tournaments) {
                tourVMs.Add(new TournamentsVM {
                    Date = tournament.Date,
                    Id = tournament.Id,
                    Name = tournament.Name,
                    CourtName = tournament.Court.Name,
                    NumberOfCourts = tournament.Court.NumberOfCourts
                });
            }
            return tourVMs;
        }


        internal TournamentDTO ModelToDto(Tournament tournament) {
            return new TournamentDTO() {
                Id = tournament.Id,
                Name = tournament.Name,
                Date = tournament.Date,
                CourtId = tournament.Court.Id
            };
        }


        private Tournament DtoToModel(TournamentDTO tournamentDto) {
            return new Tournament() {
                Court = _dbContext.Courts.FirstOrDefault(s => s.Id == tournamentDto.CourtId),
                Id = tournamentDto.Id,
                Name = tournamentDto.Name,
                Date = tournamentDto.Date,

            };
        }
        public async Task CreateAsync(TournamentDTO newTournament) {
            Tournament tournamentToInsert = DtoToModel(newTournament);
            if (tournamentToInsert.Court != null) {
                await _dbContext.Tournaments.AddAsync(tournamentToInsert);
                await _dbContext.SaveChangesAsync();

            }
        }

        //metoda pro editaci turnaje (tlačítko Edit)
        public async Task<Tournament> GetByIdAsync(int id) {
            return await _dbContext.Tournaments.Include(n => n.Court).FirstOrDefaultAsync(x => x.Id == id);
        }
        

        //meotda pro aktualizaci hodnot v DB a přesměrování do seznamu turnajů
        [HttpPost]
        public async Task UpdateAsync(int id, TournamentDTO tournamentDto) {
            Tournament updatedTournament = DtoToModel(tournamentDto);
            _dbContext.Tournaments.Update(updatedTournament);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id) {
            var tournamentToDelete = await _dbContext.Tournaments.FirstOrDefaultAsync(tournament => tournament.Id == id);
            _dbContext.Tournaments.Remove(tournamentToDelete);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<TournamentsDropdownsVM> GetNewTournamentsDropdownsValues() {
            var tournamentsDropdownsData = new TournamentsDropdownsVM() {
                Courts = await _dbContext.Courts.OrderBy(n => n.Name).ToListAsync()
            };
            return tournamentsDropdownsData;
        }



     

    }
}
