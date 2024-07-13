﻿using Microsoft.AspNetCore.Mvc;
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
    }
}