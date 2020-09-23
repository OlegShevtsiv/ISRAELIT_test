using IsraelIT_test.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace WebAPIXUnitTests
{
    public class BooksControllerTests
    {
        BooksApiController booksController;
        public BooksControllerTests()
        {

        }

        [Fact]
        public void GetSingleBookByIdTest_ReturnsSuccesWithSearchedObject()
        {
            // Arrange
            
            // Act

            // Assert
        }

        [Fact]
        public void GetSingleBookByIdTest_ReturnsBadReuest()
        {
            // Arrange

            // Act

            // Assert
        }

        [Fact]
        public void GetSingleBookByIdTest_ReturnsNotFound()
        {
            // Arrange

            // Act

            // Assert
        }

        [Fact]
        public void GetBooksByAuthorNameTest_ReturnsSuccesWithSearchedObject()
        {
            // Arrange

            // Act

            // Assert
        }

        [Fact]
        public void GetBooksByAuthorNameTest_ReturnsBedRequestOfLimitOrPage()
        {
            // Arrange

            // Act

            // Assert
        }

        [Fact]
        public void GetBooksByAuthorNameTest_ReturnsNotFoundOfAuthorNameOrBooks()
        {
            // Arrange

            // Act

            // Assert
        }

        [Fact]
        public void GetAllBooksTest()
        {
            // Arrange

            // Act

            // Assert
        }

        [Fact]
        public void CrateNewBookTest_ReturnsOkResult()
        {
            // Arrange

            // Act

            // Assert
        }

        [Fact]
        public void CrateNewBookTest_ReturnsBadRequest()
        {
            // Arrange

            // Act

            // Assert
        }

        [Fact]
        public void CrateNewBookTest_ReturnsNotFound()
        {
            // Arrange

            // Act

            // Assert
        }

        [Fact]
        public void UpdateBookTest_ReturnsOkResult()
        {
            // Arrange

            // Act

            // Assert
        }

        [Fact]
        public void UpdateBookTest_ReturnsBadRequest()
        {
            // Arrange

            // Act

            // Assert
        }

        [Fact]
        public void UpdateBookTest_ReturnsNotFound()
        {
            // Arrange

            // Act

            // Assert
        }

        [Fact]
        public void DeleteBookTest_ReturnsOkResult()
        {
            // Arrange

            // Act

            // Assert
        }

        [Fact]
        public void DeleteBookTest_ReturnsBadRequest()
        {
            // Arrange

            // Act

            // Assert
        }

        [Fact]
        public void DeleteBookTest_ReturnsNotFound()
        {
            // Arrange

            // Act

            // Assert
        }
    }
}
