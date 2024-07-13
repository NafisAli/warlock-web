using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WarlockMVC_Razor.Data;
using WarlockMVC_Razor.Models;

namespace WarlockMVC_Razor.Pages.Categories
{
    [BindProperties]
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public Category Category { get; set; }

        public EditModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public void OnGet(int? id)
        {
            if (id != null && id != 0)
            {
                Category = _db.Categories.Find(id);
            }
        }

        public IActionResult OnPost()
        {
            if (Category.Name == Category.DisplayOrder.ToString()) ModelState.AddModelError("Category.Name", "The Display Order cannot exactly match the name");

            if (!ModelState.IsValid) return Page();

            _db.Categories.Update(Category);
            _db.SaveChanges();

            TempData["success"] = "Category updated successfully";

            return RedirectToPage("Index");
        }
    }
}
