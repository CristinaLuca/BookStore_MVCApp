using System.ComponentModel.DataAnnotations;

namespace BookStore_MVCApp.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }

        [Range(1, 500, ErrorMessage = "Price must be between 1 and 500.")]
        public decimal Price { get; set; }

    }
}
