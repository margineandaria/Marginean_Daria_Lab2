using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Marginean_Daria_Lab2.Data;
using Marginean_Daria_Lab2.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Marginean_Daria_Lab2.Pages.Books
{
    public class CreateModel : BookCategoriesPageModel // Moștenește corect clasa ajutătoare
    {
        private readonly Marginean_Daria_Lab2.Data.Marginean_Daria_Lab2Context _context;

        public CreateModel(Marginean_Daria_Lab2.Data.Marginean_Daria_Lab2Context context)
        {
            _context = context;
        }

        // --- Metoda OnGet a fost modificată complet ---
        public IActionResult OnGet()
        {
            // Setăm dropdown-urile pentru Publisher și Author
            ViewData["PublisherID"] = new SelectList(_context.Set<Publisher>(), "ID", "PublisherName");
            ViewData["AuthorID"] = new SelectList(_context.Author, "ID", "FullName");

            // Cream o instanță goală de Book și o folosim pentru a popula
            // lista de categorii (checkbox-uri). Așa vor apărea toate nebifate.
            var book = new Book();
            book.BookCategories = new List<BookCategory>();
            PopulateAssignedCategoryData(_context, book);

            return Page();
        }

        [BindProperty]
        public Book Book { get; set; }

        // --- Metoda OnPostAsync a fost modificată complet ---
        public async Task<IActionResult> OnPostAsync(string[] selectedCategories)
        {
            // Cream o nouă carte (newBook) la care vom atașa categoriile
            var newBook = new Book();

            if (selectedCategories != null)
            {
                newBook.BookCategories = new List<BookCategory>();
                foreach (var cat in selectedCategories)
                {
                    // Pentru fiecare ID de categorie bifat, cream o nouă intrare în tabela de legătură
                    var catToAdd = new BookCategory
                    {
                        CategoryID = int.Parse(cat)
                    };
                    newBook.BookCategories.Add(catToAdd);
                }
            }

            // Copiem datele din formular (Titlu, Preț etc.) în cartea noastră
            // și îi atașăm lista de categorii pe care tocmai am creat-o.
            if (await TryUpdateModelAsync<Book>(
                   newBook,
                   "Book", // Prefixul pentru model binding
                   i => i.Title, i => i.AuthorID, i => i.Price, i => i.PublishingDate, i => i.PublisherID))
            {
                _context.Book.Add(newBook);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            
            // Dacă modelul nu e valid, repopulăm pagina cu datele necesare
            PopulateAssignedCategoryData(_context, newBook);
            return Page();
        }
    }
}