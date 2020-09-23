using IsraelIT_test.Controllers;
using IsraelIT_test.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace WebAPIXUnitTests
{
    public class AuthorsControllerTests
    {
        AuthorsApiController authorsController;
        public AuthorsControllerTests()
        {
            this.authorsController = new AuthorsApiController(new LibraryDBContext(new DbContextOptions<LibraryDBContext>()));
        }

        [Fact]
        public void GetSingleAuthorByIdTest_ReturnsSuccesWithSearchedObject()
        {
            // Arrange

            // Act

            // Assert
        }

        [Fact]
        public void GetSingleAuthorByIdTest_ReturnsBadReuest()
        {
            // Arrange

            // Act

            // Assert
        }

        [Fact]
        public void GetSingleAuthorByIdTest_ReturnsNotFound()
        {
            // Arrange

            // Act

            // Assert
        }

        [Fact]
        public void GetAllAuthorsTest_ReturnsSuccesWithSearchedObject()
        {
            // Arrange

            // Act

            // Assert
        }

        [Fact]
        public void GetAllAuthorsTest_ReturnsBedRequestOfLimitOrPage()
        {
            // Arrange

            // Act

            // Assert
        }

        [Fact]
        public void GetAllAuthorsTest_ReturnsNotFoundOfAuthorNameOrBooks()
        {
            // Arrange

            // Act

            // Assert
        }


        [Fact]
        public void CrateNewAuthorTest_ReturnsOkResult()
        {
            // Arrange

            // Act

            // Assert
        }

        [Fact]
        public void CrateNewAuthorTest_ReturnsBadRequest()
        {
            // Arrange

            // Act

            // Assert
        }

        [Fact]
        public void CrateNewAuthorTest_ReturnsNotFound()
        {
            // Arrange

            // Act

            // Assert
        }

        [Fact]
        public void UpdateAuthorTest_ReturnsOkResult()
        {
            // Arrange

            // Act

            // Assert
        }

        [Fact]
        public void UpdateAuthorTest_ReturnsBadRequest()
        {
            // Arrange

            // Act

            // Assert
        }

        [Fact]
        public void UpdateAuthorTest_ReturnsNotFound()
        {
            // Arrange

            // Act

            // Assert
        }

        [Fact]
        public void DeleteAuthorTest_ReturnsOkResult()
        {
            // Arrange

            // Act

            // Assert
        }

        [Fact]
        public void DeleteAuthorTest_ReturnsBadRequest()
        {
            // Arrange

            // Act

            // Assert
        }

        [Fact]
        public void DeleteAuthorTest_ReturnsNotFound()
        {
            // Arrange

            // Act

            // Assert
        }
    }
}
