using Microsoft.AspNetCore.Identity;

namespace API.Entities
{
    //<int> ker uporablja string za primary key
    public class AppUser : IdentityUser<int>
    {
        public DateTime Created { get; set; } = DateTime.Now;

        public ICollection<Book> Books { get; set; }

        public ICollection<AppUserRole> UserRoles { get; set; }

    }
}