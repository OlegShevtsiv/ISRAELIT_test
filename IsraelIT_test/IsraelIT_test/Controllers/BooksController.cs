using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IsraelIT_test.Models;
using IsraelIT_test.Models.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using IsraelIT_test.RequestModels;

namespace IsraelIT_test.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly LibraryDBContext libraryDBContext;

        public BooksController(LibraryDBContext _libraryDBContext)
        {
            this.libraryDBContext = _libraryDBContext;
        }


        /// <summary>
        /// Get book by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Book>> Get(int id) 
        {
            if (id < 0)
            {
                return BadRequest($"'{nameof(id)}' have to be bigger than Zero!");
            }

            Book book = await libraryDBContext.Books
                .Include(b => b.BookAuthors)
                .ThenInclude(ba => ba.Author)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (book == null) 
            {
                return NotFound($"Author with id '{id}' wasn't found.");
            }

            return new ObjectResult(book);
        }


        /// <summary>
        /// If author name not emthy -- filter books by this parametr, otherwise return not filtered list of books.
        /// </summary>
        /// <param name="authorName"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetByAuthor/")]
        public async Task<ActionResult<IEnumerable<Book>>> GetByAuthor(string authorName = "", int page = 0, int limit = 10) 
        {
            if (limit < 0)
            {
                return BadRequest($"'{nameof(limit)}' have to be bigger than Zero!");
            }

            if (page < 0)
            {
                return BadRequest($"'{nameof(page)}' have to be bigger than Zero!");
            }

            IEnumerable<Book> books;

            if (string.IsNullOrEmpty(authorName))
            {
                books = libraryDBContext.Books
                    .Include(b => b.BookAuthors)
                    .ThenInclude(ba => ba.Author)
                    .Skip(page * limit)
                    .Take(limit);
            }
            else
            {
                authorName = authorName.ToLower();

                var author = (await libraryDBContext.Authors
                    .Include(a => a.BookAuthors)
                    .ThenInclude(ba => ba.Book)
                    .FirstOrDefaultAsync(a => a.FullName.ToLower() == authorName));

                if (author == null) 
                {
                    return NotFound($"Author with name'{authorName}' wasn't found.");
                }

                books = author.Books
                    .Skip(page * limit)
                    .Take(limit);
            }

            if (books == null || !books.Any())
            {
                return NotFound("No books found of this request.");
            }

            return new ObjectResult(books);
        }

        /// <summary>
        /// Create new book
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post([FromBody]BookRequestModel book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Fill out all the required fields!");
            }

            Book newBook = new Book() {
                Name = book.Name,
                Description = book.Description,
                Year = book.Year,
                PagesAmount = book.PagesAmount
            };

            if (book.AuthorsIds != null && book.AuthorsIds.Any())
            {
                foreach (var aid in book.AuthorsIds)
                {
                    if (!libraryDBContext.Authors.Any(a => a.Id == aid)) 
                    {
                        return NotFound($"Author with id '{aid}' wasn't found.");
                    }
                    newBook.BookAuthors.Add(new BookAuthor(newBook.Id, aid));
                }
            }

            libraryDBContext.Books.Add(newBook);

            libraryDBContext.Entry<Book>(newBook).State = EntityState.Added;

            libraryDBContext.SaveChanges();

            return Ok();
        }

        /// <summary>
        /// Update any book
        /// </summary>
        /// <param name="id"></param>
        /// <param name="book"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Put(int id, [FromBody]BookRequestModel book)
        {

            if (id < 0)
            {
                return BadRequest($"'{nameof(id)}' have to be bigger than Zero!");
            }

            Book bookToUpdate = await libraryDBContext.Books
                                            .Include(a => a.BookAuthors)
                                            .FirstOrDefaultAsync(a => a.Id == id);

            if (bookToUpdate == null)
            {
                return NotFound($"Book with id '{id}' doesn't exist!");
            }

            bookToUpdate.Name = book.Name;
            bookToUpdate.Description = book.Description;
            bookToUpdate.PagesAmount = book.PagesAmount;
            bookToUpdate.Year = book.Year;


            if (book.AuthorsIds != null && book.AuthorsIds.Any())
            {
                foreach (var aid in book.AuthorsIds)
                {
                    if (bookToUpdate.BookAuthors.Any(ba => ba.AuthorId == aid))
                    {
                        continue;
                    }

                    if (!libraryDBContext.Authors.Any(a => a.Id == aid))
                    {
                        return NotFound($"Author with id '{aid}' donesn't exist.");
                    }
                    bookToUpdate.BookAuthors.Add(new BookAuthor(bookToUpdate.Id, aid));
                }

                bookToUpdate.BookAuthors = bookToUpdate.BookAuthors.Where(ba => book.AuthorsIds.Any(a => a == ba.AuthorId)).ToList();
            }
            else
            {
                bookToUpdate.BookAuthors = new List<BookAuthor>();
            }

            libraryDBContext.Books.Update(bookToUpdate);

            libraryDBContext.Entry<Book>(bookToUpdate).State = EntityState.Modified;

            libraryDBContext.SaveChanges();

            return Ok();
        }

        /// <summary>
        /// Delete any book
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 0)
            {
                return BadRequest($"'{nameof(id)}' have to be bigger than Zero!");
            }

            Book bookToDelete = await libraryDBContext.Books.FindAsync(id);
            if (bookToDelete == null) 
            {
                return NotFound($"Book with id '{id}' doesn't exist");
            }

            libraryDBContext.Books.Remove(bookToDelete);

            libraryDBContext.Entry<Book>(bookToDelete).State = EntityState.Deleted;

            libraryDBContext.SaveChanges();

            return Ok();
        }
    }
}
