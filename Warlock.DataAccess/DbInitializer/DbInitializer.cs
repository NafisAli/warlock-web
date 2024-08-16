using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Warlock.DataAccess.Data;
using Warlock.Models;
using Warlock.Utility;

namespace Warlock.DataAccess.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _db;

        public DbInitializer(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext db
        )
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _db = db;
        }

        public void Initialize()
        {
            // Apply pending migrations
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception ex) { }

            // Create roles if they don't exist
            if (!_roleManager.RoleExistsAsync(SD.Role_Player).GetAwaiter().GetResult())
            {
                _roleManager
                    .CreateAsync(new IdentityRole(SD.Role_Game_Master))
                    .GetAwaiter()
                    .GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Player)).GetAwaiter().GetResult();
                _roleManager
                    .CreateAsync(new IdentityRole(SD.Role_Officer))
                    .GetAwaiter()
                    .GetResult();
                _roleManager
                    .CreateAsync(new IdentityRole(SD.Role_Faction))
                    .GetAwaiter()
                    .GetResult();

                // Create the admin user
                _userManager
                    .CreateAsync(
                        new ApplicationUser
                        {
                            UserName = "GM",
                            Email = "nafis.ali0123@gmail.com",
                            EmailConfirmed = true,
                            Name = "Nafis Ali",
                            PhoneNumber = "0123456789",
                            StreetAddress = "123 Admin St",
                            City = "Admin City",
                            State = "Admin State",
                            PostCode = "1234"
                        },
                        "Admin123*"
                    )
                    .GetAwaiter()
                    .GetResult();

                ApplicationUser user = _db.ApplicationUsers.FirstOrDefault(u =>
                    u.Email == "nafis.ali0123@gmail.com"
                );

                _userManager.AddToRoleAsync(user, SD.Role_Game_Master).GetAwaiter().GetResult();
            }

            return;
        }
    }
}
