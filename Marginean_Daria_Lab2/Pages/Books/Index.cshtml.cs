using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Marginean_Daria_Lab2.Data;
using Marginean_Daria_Lab2.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Marginean_Daria_Lab2.Pages.Books
{
    public class IndexModel : PageModel
    {
        private readonly Marginean_Daria_Lab2.Data.Marginean_Daria_Lab2Context _context;

        public IndexModel(Marginean_Daria_Lab2.Data.Marginean_Daria_Lab2Context context)
        {
            _context = context;
        }
        public SelectList AuthorSL { get; set; }
        [BindProperty(SupportsGet = true)]
        public int? SearchAuthorId { get; set; }
        public IList<Book> Book { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Book = await _context.Book.Include(b=>b.Publisher).Include(b =>b.Author).ToListAsync();
        }
    }
}
