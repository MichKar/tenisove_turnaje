using TenisoveTurnaje.Models;

namespace TenisoveTurnaje.ViewModels {
    public class TournamentsVM {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Date { get; set; }
        public string CourtName { get; set; }

        public int NumberOfCourts { get; set; }
    }
}
