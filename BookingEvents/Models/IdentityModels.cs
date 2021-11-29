using System.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using BookingEvents.Models.ShoppingCart;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;


namespace BookingEvents.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [Required, DisplayName(" Name")]
        public string Name { get; set; }
        [Required, DisplayName("Surname")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Address Required")]
        public string Address { get; set; }

        [Required, DisplayName("Phone Number")]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "PhoneNum")]
        public string PhoneNum { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        public virtual DbSet<Booking2> booking2 { get; set; }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }  

        public System.Data.Entity.DbSet<BookingEvents.Models.Venue1cs> Venue{ get; set; }
        public System.Data.Entity.DbSet<BookingEvents.Models.Contact> Contacts { get; set; }
        public System.Data.Entity.DbSet<BookingEvents.Models.Event_Type> Events { get; set; }
        public System.Data.Entity.DbSet<BookingEvents.Models.Equipment> Equipments { get; set; }
        public DbSet<FoodOrder> FoodOrders { get; set; }

        public DbSet<Item> Items { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Cart_Item> Cart_Items { get; set; }
        public DbSet<Cart> Carts { get; set; }
        //public DbSet<FoodDeliveryChoice> FoodDeliveryChoice { get; set; }
        public DbSet<MealOrder> MealOrders { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<Customer> Customers { get; set; }

        //Role Management
        public DbSet<IdentityUserRole> UserInRole { get; set; } 
        // public DbSet<ApplicationUser> appUsers { get; set; }
        public DbSet<ApplicationRole> appRoles { get; set; }

        public System.Data.Entity.DbSet<BookingEvents.Models.Staff> Staffs { get; set; }

        public System.Data.Entity.DbSet<BookingEvents.Models.AssignStaff> AssignStaffs { get; set; }


        //  public IEnumerable Booking2 { get; internal set; }


    }
}