using TenisoveTurnaje.Models;

namespace TenisoveTurnaje.ViewModels {
    public class TournamentsDropdownsVM {
        public TournamentsDropdownsVM() {
            Courts = new List<Court>();
        }

        public List<Court> Courts { get; set; }

    }
}
