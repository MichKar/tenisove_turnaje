using System.ComponentModel.DataAnnotations;

namespace TenisoveTurnaje.ViewModels {
    public class UserVM {
        [Required]
        public string Name { get; set; }
        [Required]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$",ErrorMessage = "E-mail je neplatný.")]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
