using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using WebApi.DBOperations;

namespace WebApi.AddControllers{

    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _context;

        public BookController (BookStoreDbContext context)
        {
            _context = context;

        }
        // private static List<Book> BookList = new List<Book>()
        // {
        //     new Book{
        //         Id = 1,
        //         Title = "Kaşağı",
        //         GenreId = 1,
        //         PageCount ="200",
        //         PublishDate = new DateTime(2031,01,23)
        //     },
        //     new Book{
        //         Id = 2,
        //         Title = "nasrettin hoca",
        //         GenreId = 1,
        //         PageCount ="200",
        //         PublishDate = new DateTime(1954,04,13)
        //     },
        //     new Book{
        //         Id = 3,
        //         Title = "aşkı memnu",
        //         GenreId = 2,
        //         PageCount ="200",
        //         PublishDate = new DateTime(1999,10,03)
        //     }
        // };

        // [HttpGet]
        // public List<Book> GetBooks(){
        //     var bookList = BookList.OrderBy(x => x.Id).ToList<Book>();
        //     return bookList;
        // }


        [HttpGet]
        public List<Book> GetBooks()
        {
            var bookList = _context.Books.OrderBy(x => x.Id).ToList<Book>();
            return bookList;
        }

        [HttpGet("{id}")]
        public Book GetById(int id){
            var book = _context.Books.Where(book => book.Id == id).SingleOrDefault();
            return book;
        }
        //[HttpGet]
        // public Book Get([FromQuery] string id){
        //     var book = _context.Books.OrderBy(book => book.Id == Convert.ToInt32(id)).SingleOrDefault();
        //     return book;
        // }
        [HttpPost]
        public IActionResult AddBook([FromBody] Book newBook)
        {
            var book = _context.Books.SingleOrDefault(x=> x.Title == newBook.Title);
            if (book is not null) 
            {
                return BadRequest();
            }

            _context.Books.Add(newBook);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook (int Id, [FromBody] Book updateBook)
        {
            var book = _context.Books.SingleOrDefault(x=> x.Id == Id);

            if(book is null)
                return BadRequest();
            
            book.GenreId = updateBook.GenreId != default ? updateBook.GenreId : book.GenreId;
            book.PageCount = updateBook.PageCount != default ? updateBook.PageCount : book.PageCount;
            book.PublishDate = updateBook.PublishDate != default ? updateBook.PublishDate : book.PublishDate;
            book.Title = updateBook.Title != default ? updateBook.Title : book.Title;
            _context.SaveChanges();
            return Ok();

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook (int Id)
        {
            var book = _context.Books.SingleOrDefault(x=> x.Id == Id);
            if(book is null)
                return BadRequest();
            _context.Books.Remove(book);
            _context.SaveChanges();
            return Ok();

        }
        
    }

    
}