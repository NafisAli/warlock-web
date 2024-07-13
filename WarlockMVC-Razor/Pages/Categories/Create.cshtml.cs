using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WarlockMVC_Razor.Data;
using WarlockMVC_Razor.Models;

namespace WarlockMVC_Razor.Pages.Categories
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        [BindProperty]
        public Category Category { get; set; }

        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (Category.Name == Category.DisplayOrder.ToString()) ModelState.AddModelError("Category.Name", "The Display Order cannot exactly match the name");

            if (!ModelState.IsValid) return Page();

            _db.Categories.Add(Category);
            _db.SaveChanges();

            return RedirectToPage("Index");

        }
    }
}
