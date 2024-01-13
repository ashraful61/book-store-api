using BookStore.API.Data;
using BookStore.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.API.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly BookStoreContext _context;
        public BookRepository(BookStoreContext context)
        {
            _context = context;
        }
        public async Task<List<BookModel>> GetAllBooksAsync()
        {
            var records = await _context.Books.Select(x => new BookModel
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
            }).ToListAsync();

            return records;
        }

        public async Task<BookModel> GetBookByIdAsync(int bookId)
        {
            var record = await _context.Books.Where(x => x.Id == bookId).Select(x => new BookModel
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
            }).FirstOrDefaultAsync();

            return record;
        }


        public async Task<int> AddBookAsync(BookModel bookModel)
        {
            var book = new Books
            {
                Id = bookModel.Id,
                Title = bookModel.Title,
                Description = bookModel.Description,
            };

            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return book.Id;
        }




        public async Task UpdateBookAsync(int bookId, BookModel bookModel)
        {
            //var book = await _context.Books.FindAsync(bookId);
            //if (book != null)
            //{
            //    book.Title = bookModel.Title;
            //    book.Description = bookModel.Description;

            //    await _context.SaveChangesAsync();
            //}

            var book = new Books()
            {
                Id = bookId,
                Title = bookModel.Title,
                Description = bookModel.Description
            };

            _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }


        //public async Task<int> UpdateBookByIdAsync(int bookId, BookModel bookModel)
        //{
        //    var book = new Books()
        //    {
        //        Id = bookId,
        //        Title = bookModel.Title,
        //        Description = bookModel.Description,
        //    };

        //    _context.Books.Update(book);
        //    await _context.SaveChangesAsync();
        //    return book.Id;
        //    //if (book != null)
        //    //{
        //    //    // Update the book properties with the new values
        //    //    book.Title = bookModel.Title;
        //    //    book.Description = bookModel.Description;

        //    //    // Save changes to the database
        //    //    await _context.SaveChangesAsync();

        //    //    // Return the updated BookModel after the update
        //    //    return new BookModel
        //    //    {
        //    //        Id = book.Id,
        //    //        Title = book.Title,
        //    //        Description = book.Description
        //    //    };
        //    //}

        //}


    }
}
