using BookStore_MVCApp.Data;
using BookStore_MVCApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Channels;

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


        /*
         * ADD BOOK
         */
        //the show form
        [HttpGet]
        public IActionResult AddBook()
        {
            return View();
        }

        //adds a book to the database
        [HttpPost]
        public async Task<IActionResult> AddBook(Book book)
        {

            // add the book to the context
            appDBcontext.Add(book); 

            //send to the DB the changes done in the DBContext
            await appDBcontext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        /* 
 * DELETE
 */
        // GET: Books/Delete/XX
        public async Task<IActionResult> Delete(int? id)
        {
            //check if the book id is not null 
            if (id == null)
            {
                return NotFound();
            }

            //find a book that has the ID "id"
            var book = await appDBcontext.Books.FirstOrDefaultAsync(bookIterator => bookIterator.Id == id);

            //check if a book has beeen found
            if (book == null)
            {
                return NotFound();
            }
            
            return View(book); // show a conformation page
        }

        // POST: Books/Delete/XX
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //find a book that has the ID "id"
            var book = await appDBcontext.Books.FindAsync(id);

            if (book != null)
            {
                //remove the book from the DBContext
                appDBcontext.Books.Remove(book);
                //Apply the changes made in the DbContext to the database
                await appDBcontext.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index)); // back to list
        }

        /*
 * UPDATE
 */
        // GET: Books/Edit/XX
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                TempData["ErrorMessage"] = " In the meantime, this book has been deleted.";
                return RedirectToAction(nameof(Index));

            }

            var book = await appDBcontext.Books.FindAsync(id);
            if (book == null)
            {
                //what should be done here?

            }

            return View(book); // returns the edit form
        }


        // POST: Books/Edit/XX
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Book book)
        {
            if (id != book.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    //make the changes in the DBContext 
                    appDBcontext.Update(book);
                    
                    //apply the changes made in the DbContext to the database
                    await appDBcontext.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    // what does happen if the book doesn’t exist??
                    throw;
                }
            }

            return View(book);
        }

    }


}
