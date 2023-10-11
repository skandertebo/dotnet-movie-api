using Microsoft.AspNetCore.Mvc;
using TP1.Context;
using TP1.Dto.Movie;
using TP1.Middleware;
using TP1.Models;
using TP1.ReponseExceptions;
using TP1.Services;

namespace TP1.Controllers
{
    [Route("movie")]
    public class MovieController : Controller
    {
        private readonly IMovieService _movieService;
        public MovieController(IMovieService movieService)
        {
           _movieService = movieService;
        }

        [HttpGet("")]
        public IActionResult Index(int? year = null, int? month = null, int page = 1, int offset = 10, string q = "")
        {
            var result = _movieService.GetAll(year, month, page, offset, q);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = _movieService.FindById(id);
            if (result == null)
                throw new NotFoundException("Movie not found");
            return Ok(result);
        }

        [ServiceFilter(typeof(AuthMiddleware))]
        [AdminMiddleware]
        [HttpPost()]
        public IActionResult Add([FromForm] string title, [FromForm] string description, [FromForm] int year, [FromForm] int month, [FromForm] IFormFile image, [FromForm] string videoUrl = null)
        {
            var result = _movieService.Add(title, description, year, month, image, videoUrl);
            return Ok(result);
        }

        

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
           var result = _movieService.Delete(id);
           return Ok(result);
        }
    }

}
