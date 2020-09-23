using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IsraelIT_test.RequestModels
{
    public class AuthorRequestModel
    {
        [Required]
        public string FullName { get; set; }

        [Required]
        public DateTime? BirthDate { get; set; }

        public int[] BooksIds { get; set; }

    }
}
