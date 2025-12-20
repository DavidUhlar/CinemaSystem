using System.Text.Json.Serialization;

namespace CinemaSystem.Services
{
    public class TmdbService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private const string BaseUrl = "https://api.themoviedb.org/3";

        public TmdbService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["Tmdb:ApiKey"]
                ?? throw new InvalidOperationException("TMDB API key not configured");
        }

        private static readonly Dictionary<int, string> GenreMap = new()
    {
        { 28, "Action" },
        { 12, "Adventure" },
        { 16, "Animation" },
        { 35, "Comedy" },
        { 80, "Crime" },
        { 99, "Documentary" },
        { 18, "Drama" },
        { 10751, "Family" },
        { 14, "Fantasy" },
        { 36, "History" },
        { 27, "Horror" },
        { 10402, "Music" },
        { 9648, "Mystery" },
        { 10749, "Romance" },
        { 878, "Science Fiction" },
        { 10770, "TV Movie" },
        { 53, "Thriller" },
        { 10752, "War" },
        { 37, "Western" }
    };

        public async Task<MovieInfo?> GetMovieInfoAsync(string movieName)
        {
            try
            {
                var searchUrl = $"{BaseUrl}/search/movie?api_key={_apiKey}&query={Uri.EscapeDataString(movieName)}&language=en-US";
                var searchResponse = await _httpClient.GetFromJsonAsync<TmdbSearchResponse>(searchUrl);

                if (searchResponse?.Results == null || !searchResponse.Results.Any())
                    return null;

                var movie = searchResponse.Results.First();

                return new MovieInfo
                {
                    Title = movie.Title,
                    OriginalTitle = movie.OriginalTitle,
                    Description = movie.Overview,
                    PosterUrl = !string.IsNullOrEmpty(movie.PosterPath)
                        ? $"https://image.tmdb.org/t/p/w500{movie.PosterPath}"
                        : null,
                    Rating = movie.VoteAverage,
                    ReleaseDate = movie.ReleaseDate,

                    Genres = movie.GenreIds
                        .Where(id => GenreMap.ContainsKey(id))
                        .Select(id => GenreMap[id])
                        .ToList()
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching movie info: {ex.Message}");
                return null;
            }
        }
    }

    public class TmdbSearchResponse
    {
        [JsonPropertyName("results")]
        public List<TmdbMovie> Results { get; set; } = new();
    }

    public class TmdbMovie
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; } = string.Empty;

        [JsonPropertyName("original_title")]
        public string OriginalTitle { get; set; } = string.Empty;

        [JsonPropertyName("overview")]
        public string Overview { get; set; } = string.Empty;

        [JsonPropertyName("poster_path")]
        public string? PosterPath { get; set; }

        [JsonPropertyName("vote_average")]
        public double VoteAverage { get; set; }

        [JsonPropertyName("release_date")]
        public string? ReleaseDate { get; set; }

        [JsonPropertyName("genre_ids")]
        public List<int> GenreIds { get; set; } = new();
    }

    // Výsledná trieda pre aplikáciu
    public class MovieInfo
    {
        public string Title { get; set; } = string.Empty;
        public string OriginalTitle { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? PosterUrl { get; set; }
        public double Rating { get; set; }
        public string? ReleaseDate { get; set; }
        public List<string> Genres { get; set; } = new();
    }
}
