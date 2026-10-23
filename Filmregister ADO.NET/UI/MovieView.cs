using Filmregister_ADO.NET.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Filmregister_ADO.NET.UI
{
    internal class MovieView
    {
        public void PrintMovies(List<Movie> movies)
        {
            string movieTableHeader = $"| {"ID",-6} | {"Title",-50} | {"Year",-4} | {"Genre",-15} |";
            Console.WriteLine(movieTableHeader);
            Console.WriteLine(new string('-', movieTableHeader.Length));

            foreach (var movie in movies)
            {
                Console.WriteLine($"| {movie.Id,-6} | {movie.Title,-50} | {movie.ReleaseYear,-4} | {movie.GenreName,-15} |");
            }
        }
    }
}
