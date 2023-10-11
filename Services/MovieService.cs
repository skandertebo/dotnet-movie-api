using TP1.Context;
using TP1.Models;

namespace TP1.Services
{
    public interface IMovieService : IGenericService<Movie>
    {
        public List<Movie> GetAll(int? year = null, int? month = null, int page = 1, int offset = 10, string q = "");
        public Movie Add(string title, string description, int year, int month, IFormFile image, string videoUrl = null);
    }
    public class MovieService: GenericService<Movie>, IMovieService
    {
        private readonly IUserService _userService;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public MovieService(MovieDbContext context, IUserService userService, IWebHostEnvironment hostingEnvironment) : base(context)
        {
            _userService = userService;
            _hostingEnvironment = hostingEnvironment;
        }

        public List<Movie> GetAll(int? year = null, int? month = null, int page = 1, int offset = 10, string q = "")
        {
            var foundMovies = from m in _repository where m.Title.Contains(q) select m;
            if (year != null)
            {
                foundMovies = foundMovies.Where(el => el.ReleaseYear == year);
            }
            if (month != null)
            {
                foundMovies = foundMovies.Where(el => el.ReleaseMonth == month);
            }
            var result = foundMovies.Skip((page - 1) * offset).Take(offset).ToList();
            return result;
        }

        public Movie Add(string title, string description, int year, int month, IFormFile image, string videoUrl = null)
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
                ReleaseYear = year,
                ReleaseMonth = month,
                Description = description,
                VideoUrl = videoUrl,
                ImageUrl = image != null ? Path.Combine("/files/images", uniqueId.ToString()).Replace("\\", "/") + image.FileName : null, // Assuming the URL format
            };
            _context.Add(movie);
            _context.SaveChanges();
            return movie;
        }
    }
}
