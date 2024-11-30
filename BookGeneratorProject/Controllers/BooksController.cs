using BookGeneratorProject.Model;
using BookGeneratorProject.Repository.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static BookGeneratorProject.Repository.Services.BookGenerator;

namespace BookGeneratorProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookGenerator _bookGenerator;

        public BooksController()
        {
            _bookGenerator = new BookGenerator(seed: 123);
        }

        [HttpGet("generate")]
        public ActionResult<List<Book>> GenerateBooks(
            [FromQuery] Region region,
            [FromQuery] int bookCount = 10,
            [FromQuery] double averageLikes = 0.5,
            [FromQuery] double averageReviews = 20.0)
        {
            try
            {
                var books = _bookGenerator.GenerateBooks(region, bookCount, averageLikes, averageReviews);

                return Ok(books);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = $"Error generating books: {ex.Message}" });
            }
        }
    }
}
