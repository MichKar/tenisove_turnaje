using Microsoft.AspNetCore.Identity;

namespace TenisoveTurnaje.Models {
    public class User : IdentityUser {
        public int Points { get; set; }

    }
}
