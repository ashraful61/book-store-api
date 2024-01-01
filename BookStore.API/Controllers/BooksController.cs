using BookStore.API.Models;
using BookStore.API.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        public BooksController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }


        //Get all books
        [HttpGet("")]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _bookRepository.GetAllBooksAsync();
            return Ok(books);
        }


        //Get book by id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookById([FromRoute] int id)
        {
            if (id == 0)
            {
                return BadRequest("Id cann't be zero");
            }
            var book = await _bookRepository.GetBookByIdAsync(id);
            if (book == null)
            {
                return NotFound("No record found!");
            }

            return Ok(book);
        }


        //Add a book
        [HttpPost("")]
        public async Task<IActionResult> AddBook([FromBody] BookModel bookModel)
        {
            var id = await _bookRepository.AddBookAsync(bookModel);

            return CreatedAtAction(nameof(GetBookById), new { id = id, controller = "books" }, id);
        }

        //Get book by id
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook([FromBody] BookModel bookModel, [FromRoute] int id)
        {
            await _bookRepository.UpdateBookAsync(id, bookModel);
            return Ok();
        }

    }
}
