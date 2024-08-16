using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Warlock.DataAccess.Repository.IRepository;
using Warlock.Models;
using Warlock.Utility;

namespace WarlockMVC.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> productList = _unitOfWork.Product.GetAll(
                includeProperties: "Category"
            );

            return View(productList);
        }

        public IActionResult Details(int productId)
        {
            ShoppingCart shoppingCart =
                new()
                {
                    Product = _unitOfWork.Product.Get(
                        x => x.Id == productId,
                        includeProperties: "Category"
                    ),
                    Count = 1,
                    ProductId = productId
                };

            return View(shoppingCart);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Details(ShoppingCart shoppingCart)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            shoppingCart.ApplicationUserId = userId;

            ShoppingCart cartFromDatabase = _unitOfWork.ShoppingCart.Get(x =>
                x.Id == shoppingCart.Id && x.ApplicationUserId == userId
            );

            if (cartFromDatabase != null)
            {
                cartFromDatabase.Count += shoppingCart.Count;
                _unitOfWork.ShoppingCart.Update(cartFromDatabase);
                _unitOfWork.Save();
            }
            else
            {
                _unitOfWork.ShoppingCart.Add(shoppingCart);
                _unitOfWork.Save();

                HttpContext.Session.SetInt32(
                    SD.SessionCart,
                    _unitOfWork.ShoppingCart.GetAll(x => x.ApplicationUserId == userId).Count()
                );
            }

            TempData["success"] = "Cart updated successfully";

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(
                new ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
                }
            );
        }
    }
}
