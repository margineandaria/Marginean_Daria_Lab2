using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Marginean_Daria_Lab2.Data;
using Marginean_Daria_Lab2.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marginean_Daria_Lab2.Pages.Books
{
    public class CreateModel : BookCategoriesPageModel
    {
        // Am scos 'readonly' de aici
        private Marginean_Daria_Lab2.Data.Marginean_Daria_Lab2Context _context;

        public CreateModel(Marginean_Daria_Lab2.Data.Marginean_Daria_Lab2Context context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["PublisherID"] = new SelectList(_context.Set<Publisher>(), "ID", "PublisherName");
            ViewData["AuthorID"] = new SelectList(_context.Author, "ID", "FullName");

            var book = new Book();
            book.BookCategories = new List<BookCategory>();
            PopulateAssignedCategoryData(_context, book);

            return Page();
        }

        [BindProperty]
        public Book Book { get; set; }

        public async Task<IActionResult> OnPostAsync(string[] selectedCategories)
        {
            var newBook = new Book();

            if (selectedCategories != null)
            {
                newBook.BookCategories = new List<BookCategory>();
                foreach (var cat in selectedCategories)
                {
                    var catToAdd = new BookCategory
                    {
                        CategoryID = int.Parse(cat)
                    };
                    newBook.BookCategories.Add(catToAdd);
                }
            }

            // Pasul cheie din laborator: se copiază categoriile în cartea 'Book'
            Book.BookCategories = newBook.BookCategories;

            _context.Book.Add(Book);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}