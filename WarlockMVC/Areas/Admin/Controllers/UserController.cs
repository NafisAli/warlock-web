using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Warlock.DataAccess.Data;
using Warlock.DataAccess.Repository.IRepository;
using Warlock.Models;
using Warlock.Models.ViewModels;
using Warlock.Utility;

namespace WarlockMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Game_Master)]
    public class UserController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _db;

        public UserController(UserManager<IdentityUser> userManager, ApplicationDbContext db)
        {
            _userManager = userManager;
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult RoleManagement(string userId)
        {
            string roleId = _db.UserRoles.FirstOrDefault(x => x.UserId == userId).RoleId;

            RoleManagementVM RoleVM = new RoleManagementVM
            {
                ApplicationUser = _db
                    .ApplicationUsers.Include(x => x.Faction)
                    .FirstOrDefault(x => x.Id == userId),
                RoleList = _db.Roles.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Name
                }),
                FactionList = _db.Factions.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                })
            };

            RoleVM.ApplicationUser.Role = _db.Roles.FirstOrDefault(x => x.Id == roleId).Name;

            return View(RoleVM);
        }

        [HttpPost]
        public IActionResult RoleManagement(RoleManagementVM roleManagementVM)
        {
            string roleId = _db
                .UserRoles.FirstOrDefault(x => x.UserId == roleManagementVM.ApplicationUser.Id)
                .RoleId;

            string oldRole = _db.Roles.FirstOrDefault(x => x.Id == roleId).Name;

            if (!(roleManagementVM.ApplicationUser.Role == oldRole))
            {
                ApplicationUser applicationUser = _db.ApplicationUsers.FirstOrDefault(x =>
                    x.Id == roleManagementVM.ApplicationUser.Id
                );

                if (roleManagementVM.ApplicationUser.Role == SD.Role_Faction)
                {
                    applicationUser.FactionId = roleManagementVM.ApplicationUser.FactionId;
                }

                if (oldRole == SD.Role_Faction)
                {
                    applicationUser.FactionId = null;
                }

                _db.SaveChanges();

                _userManager.RemoveFromRoleAsync(applicationUser, oldRole).GetAwaiter().GetResult();
                _userManager
                    .AddToRoleAsync(applicationUser, roleManagementVM.ApplicationUser.Role)
                    .GetAwaiter()
                    .GetResult();
            }

            return RedirectToAction("Index");
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<ApplicationUser> objUserLst = _db
                .ApplicationUsers.Include(x => x.Faction)
                .ToList();

            var userRoles = _db.UserRoles.ToList();
            var roles = _db.Roles.ToList();

            foreach (var user in objUserLst)
            {
                var roleId = userRoles.FirstOrDefault(x => x.UserId == user.Id).RoleId;
                user.Role = roles.FirstOrDefault(x => x.Id == roleId).Name;

                if (user.Faction == null)
                {
                    user.Faction = new() { Name = "" };
                }
            }

            return Json(new { data = objUserLst });
        }

        [HttpPost]
        public IActionResult ToggleLock([FromBody] string userId)
        {
            var objFromDb = _db.ApplicationUsers.FirstOrDefault(x => x.Id == userId);

            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while locking/unlocking" });
            }

            if (objFromDb.LockoutEnd != null && objFromDb.LockoutEnd > DateTime.Now)
            {
                objFromDb.LockoutEnd = DateTime.Now;
            }
            else
            {
                objFromDb.LockoutEnd = DateTime.Now.AddYears(1000);
            }

            _db.ApplicationUsers.Update(objFromDb);
            _db.SaveChanges();

            return Json(new { success = true, message = "Lock status updated" });
        }

        #endregion
    }
}
