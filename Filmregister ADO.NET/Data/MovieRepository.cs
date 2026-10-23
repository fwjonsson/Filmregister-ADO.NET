using Filmregister_ADO.NET.Models;
using Microsoft.Data.SqlClient;

namespace Filmregister_ADO.NET.Data
{
    internal class MovieRepository
    {
        private string _connectionString =
                "Server=localhost;Database=AdoNetMoviesDb;" +
                "Trusted_Connection=True;" +
                "TrustServerCertificate=True;";

        public List<Movie> GetAllMovies()
        {
            var movies = new List<Movie>();

            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            string sqlGetAllMovies = "SELECT Movie.Id AS MovieId, Title, ReleaseYear, GenreId, Genre.Name AS GenreName " +
                "FROM Movie " +
                "JOIN Genre ON Movie.GenreId = Genre.Id";
            using var command = new SqlCommand(sqlGetAllMovies, connection);
            using var reader = command.ExecuteReader();


            while (reader.Read())
            {
                var movie = new Movie
                {
                    Id = (int)reader["MovieId"],
                    Title = (string)reader["Title"],
                    ReleaseYear = (int)reader["ReleaseYear"],
                    GenreId = (int)reader["GenreId"],
                    GenreName = (string)reader["GenreName"]
                };
                movies.Add(movie);
            }
            return movies;
        }
        public List<Movie> GetMoviesByGenre(int genreId)
        {
            var movies = new List<Movie>();

            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            string sql = "SELECT Movie.Id AS MovieId, Title, ReleaseYear, GenreId, Genre.Name AS GenreName " +
                "FROM Movie " +
                "JOIN Genre ON Movie.GenreId = Genre.Id " +
                "WHERE Movie.GenreId = @genreId";
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@GenreId", genreId);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                movies.Add(new Movie
                {
                    Id = (int)reader["MovieId"],
                    Title = (string)reader["Title"],
                    ReleaseYear = (int)reader["ReleaseYear"],
                    GenreId = (int)reader["GenreId"],
                    GenreName = (string)reader["GenreName"]
                });
            }
            return movies;

        }

        public List<Genre> GetAllGenres()
        {
            var genres = new List<Genre>();

            using var connection = new SqlConnection(_connectionString);

            connection.Open();

            using var command = new SqlCommand("SELECT Id, Name FROM Genre", connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                genres.Add(new Genre
                {
                    Id = (int)reader["Id"],
                    Name = (string)reader["Name"]
                });
            }
            return genres;
        }
        public void AddMovie(Movie movie)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            string sql = "INSERT INTO Movie (Title, ReleaseYear, GenreId) " +
                "VALUES (@Title, @ReleaseYear, @GenreId)";
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Title", movie.Title);
            command.Parameters.AddWithValue("@ReleaseYear", movie.ReleaseYear);
            command.Parameters.AddWithValue("@GenreId", movie.GenreId);
            command.ExecuteNonQuery();
        }

        public bool DeleteMovie(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            using var command = new SqlCommand("DELETE FROM Movie WHERE Id = @Id", connection);
            command.Parameters.AddWithValue("@Id", id);

            return command.ExecuteNonQuery() > 0;
        }

    }
}
