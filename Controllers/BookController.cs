using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApi.DBOperations;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _contex;

        public BookController(BookStoreDbContext contex)
        {
            _contex = contex;
        }

        // Get

        [HttpGet]
        public List<Book> GetBooks()
        {
            var bookList = _contex.Books.OrderBy(x => x.Id).ToList<Book>();
            return bookList;
        }

        [HttpGet("{id}")]

        public Book GetById(int id)
        {
            var book = _contex.Books.Where(book => book.Id == id).SingleOrDefault();
            return book;
        }

        // Post

        [HttpPost]
        public IActionResult AddBook([FromBody] Book newBook)
        {
            var book = _contex.Books.SingleOrDefault(x => x.Title == newBook.Title);
            if (book != null)
                return BadRequest();


            _contex.Books.Add(newBook);
            _contex.SaveChanges();
            return Ok();
        }

        // Put

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] Book updatedBook)
        {
            var book = _contex.Books.SingleOrDefault(x => x.Id == id);

            if (book == null)
                return BadRequest();

            book.GenreId = updatedBook.GenreId != default ? updatedBook.GenreId : default;
            book.PageCount = updatedBook.PageCount != default ? updatedBook.PageCount : default;
            book.PublishDate = updatedBook.PublishDate != default ? updatedBook.PublishDate : default;
            book.Title = updatedBook.Title != default ? updatedBook.Title : default;

            _contex.SaveChanges();
            return Ok();
        }

        [HttpDelete("{id}")]

        public IActionResult DeleteBook(int id)
        {
            var book = _contex.Books.SingleOrDefault(x => x.Id == id);
            if (book == null)
                return BadRequest();

            _contex.Books.Remove(book);
            _contex.SaveChanges();
            return Ok();

        }

    }
}
