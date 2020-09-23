﻿using System;
using System.Collections.Generic;
using System.Text;

namespace IsraelIT_test.Models.Models
{
    /// <summary>
    /// Model for Book - Author relation many-to-many
    /// </summary>
    public class BookAuthor
    {
        public int BookId { get; set; }
        public Book Book { get; set; }

        public int AuthorId { get; set; }
        public Author Author { get; set; }

        public BookAuthor()
        {

        }

        public BookAuthor(int _bookId, int _authorId)
        {
            this.BookId = _bookId;
            this.AuthorId = _authorId;
        }
    }
}