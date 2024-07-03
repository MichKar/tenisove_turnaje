using TenisoveTurnaje.Models;

namespace TenisoveTurnaje.DTO {
    public class TournamentDTO {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Date { get; set; }
        public int CourtId { get; set; }
     
    }
}
