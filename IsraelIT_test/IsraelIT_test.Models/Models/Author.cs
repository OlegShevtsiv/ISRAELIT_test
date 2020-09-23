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
    public class Author
    {
        [DataMember]
        public int Id { get; set; }

        [Required]
        [DataMember]
        public string FullName { get; set; }

        [Required]
        [DataMember]
        public DateTime? BirthDate { get; set; }

        public List<BookAuthor> BookAuthors { get; set; }

        [NotMapped]
        [DataMember]
        public List<Book> Books
        {
            get
            {
                if (this.BookAuthors == null || !this.BookAuthors.Any())
                {
                    return new List<Book>();
                }

                return this.BookAuthors
                    .Where(ba => ba.Book != null)
                    .Select(ba => ba.Book)
                    .ToList();
            }
        }

        public Author()
        {
            this.BookAuthors = new List<BookAuthor>();
        }

    }
}
