using Microsoft.AspNetCore.Mvc;
using WarlockMVC.Data;
using WarlockMVC.Models;

namespace WarlockMVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            List<Category> objCategoryList = _db.Categories.ToList();

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

            _db.Categories.Add(obj);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0) return NotFound();

            Category? categoryObj = _db.Categories.Find(id);
            if (categoryObj == null) return NotFound();

            return View(categoryObj);
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (!ModelState.IsValid) return View();

            _db.Categories.Update(obj);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
