using Marginean_Daria_Lab2;
namespace Marginean_Daria_Lab2.Models.ViewModels;

public class CategoryIndexData
{
    public IEnumerable<Category> Categories { get; set; }
    public IEnumerable<Book> Books { get; set; }
}