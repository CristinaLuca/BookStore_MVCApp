using BookStore_MVCApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStore_MVCApp.Controllers
{
    public class BooksController : Controller
    {
        //create an object of the database context defined in Program.cs
        //It represents a session with the database
        private readonly AppDBContext appDBcontext;

        public BooksController(AppDBContext context)
        {
            appDBcontext = context;
        }

        //get all books from the database and display them
        public async Task<IActionResult> Index()
        {
            //get the list from the DB asynchronously (it doesn’t block the server while waiting for the database)
            var books = await appDBcontext.Books.ToListAsync();
            //var books = appDBcontext.Books.ToList();  // synchronous version
            return View(books);
        }


    }
}
