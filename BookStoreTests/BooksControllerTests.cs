using BookStore_MVCApp.Controllers;
using BookStore_MVCApp.Data;
using BookStore_MVCApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookStoreTests
{
    public class BooksControllerTests
    {

        //create a context to use an in-memory database
        private AppDBContext GetInMemoryContext()
        {
            //create the context for the in-memory database
            var options = new DbContextOptionsBuilder<AppDBContext>()
                  .UseInMemoryDatabase(databaseName: "TestBookStoreDB")
                  .Options;
            var context = new AppDBContext(options);

            // Add data to the in-memory DB
            context.Books.AddRange(
            new Book { Id = 1, Title = "Book A", Author = "Author A", Price = 1 },
            new Book { Id = 2, Title = "Book B", Author = "Author B", Price = 2 }
            );
            context.SaveChanges();
            return context;
        }


        [Fact]
        public async Task Index_ReturnView_BookList()
        {
            // get the in-memory context
            var context = GetInMemoryContext();

            // create the controller using the provided DbContext
            var controller = new BooksController(context);

            // call the BooksController Index()
            var result = await controller.Index() as ViewResult;


            // Verify that the model is a collection of Book objects
            var model = Assert.IsAssignableFrom<IEnumerable<Book>>(result.Model);

            // Verify that the model has exactly 2 Book objects  
            Assert.Equal(2, ((List<Book>)model).Count);


        }
    }
}
