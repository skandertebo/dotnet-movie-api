using Microsoft.AspNetCore.Mvc;
using TP1.Context;
using TP1.Dto.Movie;
using TP1.Models;

namespace TP1.Controllers
{
    [Route("movie")]
    public class MovieController : Controller
    {
        private readonly MovieDbContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public MovieController(MovieDbContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet("")]
        public IActionResult Index(int? year = null, int? month = null, int page = 1, int offset = 10, string q = "")
        {
            var foundMovies = from m in _context.Movies where m.Title.Contains(q) select m;
            if(year != null)
            {
                foundMovies = foundMovies.Where(el => el.ReleaseYear == year);
            }
            if (month != null)
            {
                foundMovies = foundMovies.Where(el => el.ReleaseMonth == month);
            }
            var result = foundMovies.Skip((page-1) * offset).Take(offset).ToList();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var foundMovie = await _context.Movies.FindAsync(id);
            if (foundMovie != null)
                return Ok(foundMovie);
            else return NotFound();
        }


        [HttpPost()]
        public IActionResult Add([FromForm] string title, [FromForm] string description, [FromForm] int year, [FromForm] int month, [FromForm] IFormFile image, [FromForm] string videoUrl = null)
        {
            Guid uniqueId = Guid.NewGuid();

            if (image != null && image.Length > 0)
            {
                var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "files/images");
                var filePath = Path.Combine(uploads, uniqueId.ToString() + image.FileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    image.CopyTo(fileStream);
                }
            }
            var movie = new Movie
            {
                Title = title,
                ReleaseYear =  year,
                ReleaseMonth = month,
                Description = description,
                VideoUrl = videoUrl,
                ImageUrl = image != null ? Path.Combine("/files/images", uniqueId.ToString()).Replace("\\", "/") + image.FileName : null, // Assuming the URL format
            };
            _context.Add(movie);
            _context.SaveChanges();
            return Ok(movie);
        }

        [HttpPost("addmany")]
        public IActionResult AddMany([FromBody] List<Movie> movies)
        {
            movies.ForEach(movie => _context.Movies.Add(movie));
            _context.SaveChanges();
            return Ok(movies);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var movie = _context.Movies.FirstOrDefault(movie => movie.Id == id);
            if (movie == null) return NotFound();
            _context.Remove(movie);
            _context.SaveChanges();
            return Ok("Deleted Successfully");
        }
    }

}
