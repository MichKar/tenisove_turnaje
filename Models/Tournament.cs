namespace TenisoveTurnaje.Models {
    public class Tournament {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Date { get; set; }
        public Court Court { get; set; }
    }
}
