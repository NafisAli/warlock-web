using Microsoft.AspNetCore.Mvc;
using Warlock.DataAccess.Data;
using Warlock.DataAccess.Repository.IRepository;
using Warlock.Models;

namespace WarlockMVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository db)
        {
            _categoryRepository = db;
        }

        public IActionResult Index()
        {
            List<Category> objCategoryList = _categoryRepository.GetAll().ToList();

            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString()) ModelState.AddModelError("name", "The Display Order cannot exactly match the name");

            if (!ModelState.IsValid) return View();

            _categoryRepository.Add(obj);
            _categoryRepository.Save();

            TempData["success"] = "Category created successfully";

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0) return NotFound();

            Category? categoryObj = _categoryRepository.Get(x => x.Id == id);
            if (categoryObj == null) return NotFound();

            return View(categoryObj);
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString()) ModelState.AddModelError("name", "The Display Order cannot exactly match the name");

            if (!ModelState.IsValid) return View();

            _categoryRepository.Update(obj);
            _categoryRepository.Save();

            TempData["success"] = "Category updated successfully";

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0) return NotFound();

            Category? categoryObj = _categoryRepository.Get(x => x.Id == id);
            if (categoryObj == null) return NotFound();

            return View(categoryObj);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Category? categoryObj = _categoryRepository.Get(x => x.Id == id);

            if (categoryObj == null) return NotFound();

            _categoryRepository.Delete(categoryObj);
            _categoryRepository.Save();

            TempData["success"] = "Category deleted successfully";

            return RedirectToAction("Index");
        }
    }
}
