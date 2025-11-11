using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Marginean_Daria_Lab2.Data;
using Marginean_Daria_Lab2.Models;

namespace Marginean_Daria_Lab2.Pages.Borrowings
{
    public class DetailsModel : PageModel
    {
        private readonly Marginean_Daria_Lab2.Data.Marginean_Daria_Lab2Context _context;

        public DetailsModel(Marginean_Daria_Lab2.Data.Marginean_Daria_Lab2Context context)
        {
            _context = context;
        }

        public Borrowing Borrowing { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Modificarea cerută de Sarcina 2 este aici:
            var borrowing = await _context.Borrowing
                .Include(b => b.Member)
                .Include(b => b.Book)
                .ThenInclude(b => b.Author) // Asta încarcă și autorul cărții
                .FirstOrDefaultAsync(m => m.ID == id);


            if (borrowing == null)
            {
                return NotFound();
            }
            else
            {
                Borrowing = borrowing;
            }
            return Page();
        }
    }
}