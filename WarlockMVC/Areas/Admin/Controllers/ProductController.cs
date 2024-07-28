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
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
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

            string wwwRootPath = _webHostEnvironment.WebRootPath;

            if (file != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string productPath = Path.Combine(wwwRootPath, @"images\product");

                using (
                    var fileStream = new FileStream(
                        Path.Combine(productPath, fileName),
                        FileMode.Create
                    )
                )
                {
                    file.CopyTo(fileStream);
                }

                productVM.Product.ImageUrl = @"\images\product\" + fileName;
            }

            _unitOfWork.Product.Update(productVM.Product);
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
