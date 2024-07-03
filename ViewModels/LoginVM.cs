using System.ComponentModel.DataAnnotations;

namespace TenisoveTurnaje.ViewModels {
    public class LoginVM {

        [Required]
        public string UserEmail { get; set; }

        [Required]
        public string Password { get; set; }
        public string? ReturnUrl { get; set; }
        public bool Remember { get; set; }
    }
}
