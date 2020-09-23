using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IsraelIT_test.RequestModels
{
    public class BookRequestModel
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public DateTime? Year { get; set; }

        [Required]
        public int PagesAmount { get; set; }

        public int[] AuthorsIds { get; set; } 

    }
}
