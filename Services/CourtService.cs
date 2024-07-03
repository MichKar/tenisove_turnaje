using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TenisoveTurnaje.DTO;
using TenisoveTurnaje.Models;

namespace TenisoveTurnaje.Services {
    public class CourtService {

        private ApplicationDbContext _dbContext;

        public CourtService(ApplicationDbContext dbContext) {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<CourtDTO>> GetAllAsync() {
            var allCourts = await _dbContext.Courts.ToListAsync();
            var courtDtos = new List<CourtDTO>();
            foreach (var court in allCourts) {
                courtDtos.Add(ModelToDto(court));
            }
            return courtDtos;
        }


        private CourtDTO ModelToDto(Court court) {
            return new CourtDTO() {
                Id = court.Id,
                Name = court.Name,
                Address = court.Address,
                NumberOfCourts = court.NumberOfCourts
            };
        }

        private Court DtoToModel(CourtDTO courtDto) {
            return new Court() {
                Id = courtDto.Id,
                Name = courtDto.Name,
                Address = courtDto.Address,
                NumberOfCourts = courtDto.NumberOfCourts
            };
        }

        public async Task CreateAsync(CourtDTO newCourt) {
            await _dbContext.Courts.AddAsync(DtoToModel(newCourt));
            await _dbContext.SaveChangesAsync();
        }

        public async Task<CourtDTO> GetByIdAsync(int id) {
            var court = await _dbContext.Courts.FirstOrDefaultAsync(x => x.Id == id);
            if (court == null) {
                return null;
            }
            return ModelToDto(court);
        }


        public async Task UpdateAsync(int id, CourtDTO courtDto) {
            _dbContext.Courts.Update(DtoToModel(courtDto));
            await _dbContext.SaveChangesAsync();
           

        }




        public async Task DeleteAsync(int id) {
            var courtToDelete = await _dbContext.Courts.FirstOrDefaultAsync(court => court.Id == id);
            _dbContext.Courts.Remove(courtToDelete);
            await _dbContext.SaveChangesAsync();
        }

    }
}
