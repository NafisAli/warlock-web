using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Warlock.DataAccess.Data;
using Warlock.DataAccess.Repository.IRepository;
using Warlock.Models;
using Warlock.Models.ViewModels;

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

        public IActionResult Upsert(int? id)
        {
            ProductVM productVM =
                new()
                {
                    CategoryList = _unitOfWork
                        .Category.GetAll()
                        .Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }),
                    Product = new Product
                    {
                        Title = "",
                        Level = 0,
                        ListPrice = 0,
                        Price = 0,
                        Price50 = 0,
                        Price100 = 0,
                        Tier = ""
                    }
                };

            if (id == null || id == 0)
                return View(productVM);

            productVM.Product = _unitOfWork.Product.Get(x => x.Id == id);

            return View(productVM);
        }

        [HttpPost]
        public IActionResult Upsert(ProductVM productVM, IFormFile? file)
        {
            if (!ModelState.IsValid)
                return View();

            _unitOfWork.Product.Add(productVM.Product);
            _unitOfWork.Save();

            TempData["success"] = "Product created successfully";

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
                return NotFound();

            Product? productObj = _unitOfWork.Product.Get(x => x.Id == id);
            if (productObj == null)
                return NotFound();

            return View(productObj);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Product? productObj = _unitOfWork.Product.Get(x => x.Id == id);

            if (productObj == null)
                return NotFound();

            _unitOfWork.Product.Delete(productObj);
            _unitOfWork.Save();

            TempData["success"] = "Product deleted successfully";

            return RedirectToAction("Index");
        }
    }
}
