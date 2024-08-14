using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Warlock.DataAccess.Repository.IRepository;
using Warlock.Models;
using Warlock.Models.ViewModels;
using Warlock.Utility;

namespace WarlockMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(int orderId)
        {
            OrderVM orderDetail =
                new()
                {
                    OrderHeader = _unitOfWork.OrderHeader.Get(
                        x => x.Id == orderId,
                        includeProperties: "ApplicationUser"
                    ),
                    OrderDetails = _unitOfWork.OrderDetail.GetAll(
                        x => x.OrderHeader.Id == orderId,
                        includeProperties: "Product"
                    )
                };

            return View(orderDetail);
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll(string status)
        {
            IEnumerable<OrderHeader> objOrderHeaders = _unitOfWork
                .OrderHeader.GetAll(includeProperties: "ApplicationUser")
                .ToList();

            switch (status)
            {
                case "pending":
                    objOrderHeaders = objOrderHeaders.Where(x =>
                        x.PaymentStatus == SD.PaymentStatusPending
                    );
                    break;
                case "inprocess":
                    objOrderHeaders = objOrderHeaders.Where(x =>
                        x.OrderStatus == SD.StatusInProcess
                    );
                    break;
                case "completed":
                    objOrderHeaders = objOrderHeaders.Where(x => x.OrderStatus == SD.StatusShipped);
                    break;
                case "approved":
                    objOrderHeaders = objOrderHeaders.Where(x =>
                        x.OrderStatus == SD.StatusApproved
                    );
                    break;
                default:
                    break;
            }

            return Json(new { data = objOrderHeaders });
        }

        #endregion
    }
}
