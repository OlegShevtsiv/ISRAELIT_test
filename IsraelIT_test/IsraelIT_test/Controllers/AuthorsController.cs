using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IsraelIT_test.Models;
using IsraelIT_test.Models.Models;
using IsraelIT_test.RequestModels;

namespace IsraelIT_test.Controllers
{
    [Route("api/authors")]
    [ApiController]
    public class AuthorsApiController : ControllerBase
    {
        private readonly LibraryDBContext libraryDBContext;

        public AuthorsApiController(LibraryDBContext context)
        {
            libraryDBContext = context;
        }

        /// <summary>
        /// Get author by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Author>> Get(int id)
        {
            if (id < 0)
            {
                return BadRequest($"'{nameof(id)}' have to be bigger than Zero!");
            }

            Author author = await libraryDBContext.Authors
                .Include(b => b.BookAuthors)
                .ThenInclude(ba => ba.Book)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (author == null)
            {
                return NotFound($"Book with id '{id}' wasn't found.");
            }

            return new ObjectResult(author);
        }

        /// <summary>
        /// Get authors limited by page and amount of items on page
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAll/")]
        public ActionResult<IEnumerable<Author>> GetAll(int page = 0, int limit = 10)
        {
            if (limit < 0)
            {
                return BadRequest($"'{nameof(limit)}' have to be bigger than Zero!");
            }

            if (page < 0)
            {
                return BadRequest($"'{nameof(page)}' have to be bigger than Zero!");
            }

            IEnumerable<Author> authors = libraryDBContext.Authors
                                            .Include(a => a.BookAuthors)
                                            .ThenInclude(ba => ba.Book)
                                            .Skip(page * limit)
                                            .Take(limit);


            if (authors == null || !authors.Any())
            {
                return NotFound("No authors found of this request.");
            }

            return new ObjectResult(authors);
        }

        /// <summary>
        /// Create new Author
        /// </summary>
        /// <param name="author"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post([FromBody]AuthorRequestModel author)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Fill out all the required fields!");
            }

            Author newAuthor = new Author() {
                FullName = author.FullName,
                BirthDate = author.BirthDate
            };

            if (author.BooksIds != null && author.BooksIds.Any()) 
            {
                for (int i = 0; i < author.BooksIds.Length; i++) 
                {
                    if (!libraryDBContext.Authors.Any(a => a.Id == author.BooksIds[i])) 
                    {
                        return NotFound($"Book with id '{author.BooksIds[i]}' wasn't found.");
                    }
                    newAuthor.BookAuthors.Add(new BookAuthor(newAuthor.Id, author.BooksIds[i]));
                }
            }

            libraryDBContext.Authors.Add(newAuthor);

            libraryDBContext.Entry<Author>(newAuthor).State = EntityState.Added;

            libraryDBContext.SaveChanges();

            return Ok();
        }

        /// <summary>
        /// Update any author
        /// </summary>
        /// <param name="id"></param>
        /// <param name="author"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody]AuthorRequestModel author)
        {
            if (id < 0)
            {
                return BadRequest($"'{nameof(id)}' have to be bigger than Zero!");
            }

            Author authorToUpdate = await libraryDBContext.Authors
                                            .Include(a => a.BookAuthors)
                                            .FirstOrDefaultAsync(a => a.Id == id);

            if (authorToUpdate == null) 
            {
                return NotFound($"Author with id '{id}' doesn't exist!");
            }

            authorToUpdate.FullName = author.FullName;
            authorToUpdate.BirthDate = author.BirthDate;


            if (author.BooksIds != null && author.BooksIds.Any())
            {
                foreach (var bid in author.BooksIds)
                {
                    if (authorToUpdate.BookAuthors.Any(ba => ba.BookId == bid))
                    {
                        continue;
                    }

                    if (!libraryDBContext.Books.Any(a => a.Id == bid))
                    {
                        return NotFound($"Book with id '{bid}' wasn't found.");
                    }
                    authorToUpdate.BookAuthors.Add(new BookAuthor(bid, authorToUpdate.Id));
                }

                authorToUpdate.BookAuthors = authorToUpdate.BookAuthors.Where(ba => author.BooksIds.Any(b => b == ba.BookId)).ToList();
            }
            else 
            {
                authorToUpdate.BookAuthors = new List<BookAuthor>();
            }

            libraryDBContext.Authors.Update(authorToUpdate);

            libraryDBContext.Entry<Author>(authorToUpdate).State = EntityState.Modified;

            libraryDBContext.SaveChanges();

            return Ok();
        }        

        /// <summary>
        /// Delete any author
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

            Author authorToDelete = await libraryDBContext.Authors.FindAsync(id);
            if (authorToDelete == null)
            {
                return NotFound($"Author with id '{id}' doesn't exist");
            }

            libraryDBContext.Authors.Remove(authorToDelete);

            libraryDBContext.Entry<Author>(authorToDelete).State = EntityState.Deleted;

            libraryDBContext.SaveChanges();

            return Ok();
        }

    }
}
