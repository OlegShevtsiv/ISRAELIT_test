using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IsraelIT_test.Controllers
{
    [ApiController]
    [Route("Book")]
    public class BookController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public object Get(int id = 0) 
        {
            return $"This is book with id {id}";
        }

        /// <summary>
        /// This is API action
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/GetAll")]
        public string GetAll() 
        {
            return "Hello";
        }
    }
}
