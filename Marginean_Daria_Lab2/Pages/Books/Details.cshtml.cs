using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Marginean_Daria_Lab2.Data;
using Marginean_Daria_Lab2.Models;

namespace Marginean_Daria_Lab2.Pages.Books
{
    public class DetailsModel : PageModel
    {
        private readonly Marginean_Daria_Lab2.Data.Marginean_Daria_Lab2Context _context;

        public DetailsModel(Marginean_Daria_Lab2.Data.Marginean_Daria_Lab2Context context)
        {
            _context = context;
        }

        public Book Book { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            Book = await _context.Book
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .Include(b => b.BookCategories)
                .ThenInclude(b => b.Category)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (Book == null)
            {
                return NotFound();
            }
            
            return Page();
        }
    }
}
