using BookStore_MVCApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore_MVCApp.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }

        // connects the C# model Book to a table in the DB
        public DbSet<Book> Books { get; set; }


    }
}
