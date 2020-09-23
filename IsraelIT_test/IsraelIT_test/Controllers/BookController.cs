using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IsraelIT_test.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        /// <summary>
        /// Get book by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        public object Get(int id = 0) 
        {
            return $"This is book with id {id}";
        }


        private const int BooksPerPage = 10;
        /// <summary>
        /// This is API action
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAll/")]
        public string GetAll(int limit = BooksPerPage, int offset = 0) 
        {
            return $"{limit} elements on {offset} page";
        }

        /// <summary>
        /// Create new book
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        [HttpPost]
        public object Post(object book)
        {
            return "Here we add new book";
        }

        /// <summary>
        /// Update any book
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        [HttpPut]
        public object Put(object book)
        {
            return "Here we update any book";
        }

        /// <summary>
        /// Delete any book
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public object Delete(int id)
        {
            return $"Book with id {id} was deleted succesfully";
        }
    }
}
