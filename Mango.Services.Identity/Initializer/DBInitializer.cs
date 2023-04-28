using IdentityModel;
using Mango.Services.Identity.DBContexts;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Mango.Services.Identity.Initializer
{
    public class DBInitializer : IDBInitializer
    {
        private readonly ApplicationDBContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManger;

        public DBInitializer(ApplicationDBContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManger)
        {
            _db = db;
            _userManager = userManager;
            _roleManger = roleManger;
        }
        public void Initialize()
        {
            if (_roleManger.FindByNameAsync(SD.Admin).Result == null)
            {
                _roleManger.CreateAsync(new IdentityRole(SD.Admin)).GetAwaiter().GetResult();
                _roleManger.CreateAsync(new IdentityRole(SD.Customer)).GetAwaiter().GetResult();
            }
            else
            {
                //{ return; }
                ApplicationUser admin = new ApplicationUser()
                {
                    UserName = "admin1@gmail.com",
                    Email = "admin1@gmail.com",
                    EmailConfirmed = true,
                    PhoneNumber = "1111111111",
                    FirstName = "Ben",
                    LastName = "Admin"
                };
                var admin1 = _userManager.CreateAsync(admin, "Admin123*").GetAwaiter().GetResult();
                var role1 = _userManager.AddToRoleAsync(admin, SD.Admin).GetAwaiter().GetResult();

                var temp = _userManager.AddClaimsAsync(admin, new Claim[]
                {
                new Claim(JwtClaimTypes.Name,admin.FirstName+" "+admin.LastName),
                new Claim(JwtClaimTypes.GivenName,admin.FirstName),
                new Claim(JwtClaimTypes.FamilyName,admin.LastName),
                new Claim(JwtClaimTypes.Role,SD.Admin),
                }).Result;

                ApplicationUser customer = new ApplicationUser()
                {
                    UserName = "customer1@gmail.com",
                    Email = "customer1@gmail.com",
                    EmailConfirmed = true,
                    PhoneNumber = "1111111111",
                    FirstName = "Ben",
                    LastName = "Cust"
                };
                var admin2 = _userManager.CreateAsync(customer, "Customer123*").GetAwaiter().GetResult();
                var role2 = _userManager.AddToRoleAsync(customer, SD.Customer).GetAwaiter().GetResult();

                var temp2 = _userManager.AddClaimsAsync(customer, new Claim[]
                {
                new Claim(JwtClaimTypes.Name,customer.FirstName+" "+customer.LastName),
                new Claim(JwtClaimTypes.GivenName,customer.FirstName),
                new Claim(JwtClaimTypes.FamilyName,customer.LastName),
                new Claim(JwtClaimTypes.Role,SD.Customer),
                }).Result;
            }
        }
    }
}
