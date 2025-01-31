using EDT_Book_Library.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net;
using System.Text;

namespace BookLibraryTests
{
    public class BookLibraryTests : IClassFixture<BookLibraryWebApplicationFactory<Program>>
    {
        private readonly BookLibraryWebApplicationFactory<Program> _factory;
        private readonly HttpClient _httpClient;

        public BookLibraryTests(BookLibraryWebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _httpClient = _factory.CreateClient();
        }

        [Fact]
        public async Task AddBook_ReturnsCreatedSuccess()
        {
            //Arrange
            var book = new Book { Name = "test book", Author = "test author", Description = "test description", IsAvailable =  true };
            var content = new StringContent(System.Text.Json.JsonSerializer.Serialize(book), Encoding.UTF8, "application/json");

            //Act
            var response = await _httpClient.PostAsync("/books/", content);
            var responseContent = await response.Content.ReadAsStringAsync();
            var item = System.Text.Json.JsonSerializer.Deserialize<Book>(responseContent, new System.Text.Json.JsonSerializerOptions(System.Text.Json.JsonSerializerDefaults.Web));

            //Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.NotNull(item);
            Assert.NotNull(response.Headers.Location);
            Assert.Equal(response.Headers.Location.ToString(), $"/books/{item.Id}");
        }

        //[Fact]
        //public async Task CheckoutBookThatIsAlreadyCheckedOut_ReturnsProblem()

        //[Fact]
        //public async Task CheckInBookThatIsAlreadyCheckedIn_ReturnsProblem()
    }
}
