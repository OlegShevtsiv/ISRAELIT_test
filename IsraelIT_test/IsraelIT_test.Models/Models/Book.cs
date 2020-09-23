using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;

namespace IsraelIT_test.Models.Models
{
    [DataContract]
    public class Book
    {
        [DataMember]
        public int Id { get; set; }

        [Required]
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        [Required]
        [DataMember]
        public DateTime? Year { get; set; }

        [Required]
        [DataMember]
        public int PagesAmount { get; set; }

        public List<BookAuthor> BookAuthors { get; set; }

        [NotMapped]
        [DataMember]
        public List<Author> Authors
        {
            get
            {
                if (this.BookAuthors == null || !this.BookAuthors.Any())
                {
                    return new List<Author>();
                }

                return this.BookAuthors
                    .Where(ba => ba.Author != null)
                    .Select(ba => ba.Author)
                    .ToList();
            }
        }


        public Book()
        {
            this.BookAuthors = new List<BookAuthor>();
        }
    }
}
