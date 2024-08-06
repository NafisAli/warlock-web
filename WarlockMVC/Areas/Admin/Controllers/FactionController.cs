using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Warlock.DataAccess.Data;
using Warlock.DataAccess.Repository.IRepository;
using Warlock.Models;
using Warlock.Models.ViewModels;
using Warlock.Utility;

namespace WarlockMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    // [Authorize(Roles = SD.Role_Game_Master)]
    public class FactionController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public FactionController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Faction> objFactionList = _unitOfWork.Faction.GetAll().ToList();

            return View(objFactionList);
        }

        public IActionResult Upsert(int? id)
        {
            if (id == null || id == 0)
                return View(new Faction { Name = "" });

            Faction factionObj =
                _unitOfWork.Faction.Get(x => x.Id == id) ?? new Faction { Name = "" };

            return View(factionObj);
        }

        [HttpPost]
        public IActionResult Upsert(Faction FactionObj, IFormFile? file)
        {
            if (!ModelState.IsValid)
                return View();

            if (FactionObj.Id == 0)
            {
                _unitOfWork.Faction.Add(FactionObj);
                TempData["success"] = "Faction created successfully";
            }
            else
            {
                _unitOfWork.Faction.Update(FactionObj);
                TempData["success"] = "Faction updated successfully";
            }

            _unitOfWork.Save();

            return RedirectToAction("Index");
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Faction> objFactionList = _unitOfWork.Faction.GetAll().ToList();

            return Json(new { data = objFactionList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var FactionToBeDeleted = _unitOfWork.Faction.Get(x => x.Id == id);
            if (FactionToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            _unitOfWork.Faction.Delete(FactionToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete successful" });
        }

        #endregion
    }
}
