using Microsoft.AspNetCore.Mvc;
using Warlock.DataAccess.Data;
using Warlock.DataAccess.Repository.IRepository;
using Warlock.Models;

namespace WarlockMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Product> objProductList = _unitOfWork.Product.GetAll().ToList();

            return View(objProductList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Product obj)
        {
            if (!ModelState.IsValid) return View();

            _unitOfWork.Product.Add(obj);
            _unitOfWork.Save();

            TempData["success"] = "Product created successfully";

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0) return NotFound();

            Product? categoryObj = _unitOfWork.Product.Get(x => x.Id == id);
            if (categoryObj == null) return NotFound();

            return View(categoryObj);
        }

        [HttpPost]
        public IActionResult Edit(Product obj)
        {

            if (!ModelState.IsValid) return View();

            _unitOfWork.Product.Update(obj);
            _unitOfWork.Save();

            TempData["success"] = "Product updated successfully";

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0) return NotFound();

            Product? categoryObj = _unitOfWork.Product.Get(x => x.Id == id);
            if (categoryObj == null) return NotFound();

            return View(categoryObj);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Product? categoryObj = _unitOfWork.Product.Get(x => x.Id == id);

            if (categoryObj == null) return NotFound();

            _unitOfWork.Product.Delete(categoryObj);
            _unitOfWork.Save();

            TempData["success"] = "Product deleted successfully";

            return RedirectToAction("Index");
        }
    }
}
