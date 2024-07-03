using Microsoft.AspNetCore.Identity;

namespace TenisoveTurnaje.Models {
    public class RoleEdit {

        public IdentityRole Role { get; set; }
        public IEnumerable<User> RoleMembers { get; set; }
        public IEnumerable<User> RoleNonMembers { get; set; }


    }
}
