using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace WebApi.Controllers{

    [ApiController]
    [Route("[controller]s")]
    public class BookController : ControllerBase
    {
        private static List<Book> BookList = new List<Book>()
        {
            new Book{
                Id = 1,
                Title = "Kaşağı",
                GenreId = 1,
                PageCount ="200",
                PublishDate = new DateTime(2031,11,23)
            },
            new Book{
                Id = 2,
                Title = "Kaşağı",
                GenreId = 1,
                PageCount ="200",
                PublishDate = new DateTime(2031,11,13)
            }
        };
        // [HttpGet]
        // public List<Book> GetBooks(){
        //     var bookList = BookList.OrderBy(x => x.Id).ToList<Book>();
        //     return bookList;
        // }

        // [HttpGet("{id}")]
        // public Book GetById(int id){
        //     var book = BookList.Where(book => book.Id == id).SingleOrDefault();
        //     return book;
        // }
        [HttpGet]
        public Book Get([FromQuery] string id){
            var book = BookList.Where(book => book.Id == Convert.ToInt32(id)).SingleOrDefault();
            return book;
        }
    }

    
}