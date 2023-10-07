namespace TP1.Dto.Movie
{
    public class CreateMovieDto
    {
        public int ReleaseYear { get; set; }
        public int ReleaseMonth { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string VideoUrl { get; set; }

        public IFormFile? image { get; set; }
    }
}
