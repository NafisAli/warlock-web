using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Warlock.DataAccess.Repository.IRepository;
using Warlock.Utility;

namespace WarlockMVC.ViewComponents
{
    public class ShoppingCartViewComponent : ViewComponent
    {
        private readonly IUnitOfWork _unitOfWork;

        public ShoppingCartViewComponent(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            if (claim != null)
            {
                if (HttpContext.Session.GetInt32(SD.SessionCart) == null)
                {
                    int count = _unitOfWork
                        .ShoppingCart.GetAll(x => x.ApplicationUserId == claim.Value)
                        .Count();

                    HttpContext.Session.SetInt32(SD.SessionCart, count);

                    return View(HttpContext.Session.GetInt32(SD.SessionCart));
                }

                return View(HttpContext.Session.GetInt32(SD.SessionCart));
            }
            else
            {
                HttpContext.Session.Clear();

                return View(0);
            }
        }
    }
}
