using Filmregister_ADO.NET.Data;
using Filmregister_ADO.NET.Menus;
using Filmregister_ADO.NET.Models;
using Filmregister_ADO.NET.UI;
using Microsoft.Data.SqlClient;

namespace Filmregister_ADO.NET
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MainMenu mainMenu = new MainMenu();
            var repository = new MovieRepository();
            var movieView = new MovieView();
            bool isRunning = true;

            while (isRunning)
            {
                MainMenuOption choice = mainMenu.Show();

                switch (choice)
                {
                    case MainMenuOption.ShowAllMovies:
                        {
                            Console.Clear();
                            movieView.PrintMovies(repository.GetAllMovies());
                            Console.ReadKey();
                            break;
                        }
                    case MainMenuOption.SearchMovieByGenre: 
                        {
                            var genre = ChooseGenre(repository);
                            if ( genre != null)
                            {
                                Console.Clear();
                                movieView.PrintMovies(repository.GetMoviesByGenre(genre.Id));
                                Console.ReadKey();
                            }
                            break;
                        }
                    case MainMenuOption.AddMovie:
                        {
                            Console.Clear();
                            Console.Write("Titel: ");


                            string title = Console.ReadLine() ?? "";
                            if (title.IsWhiteSpace())
                            {
                                Console.WriteLine("Du måste ange en titel.");
                                Console.ReadKey();
                                break;
                            }

                            Console.Write("Utgivningsår: ");
                            if (!int.TryParse(Console.ReadLine(), out int year))
                            {
                                Console.WriteLine("Ogilitigt år.");
                                Console.ReadKey();
                                break;
                            }

                            var genre = ChooseGenre(repository);
                            if (genre != null)
                            {
                                repository.AddMovie(new Movie
                                {
                                    Title = title,
                                    ReleaseYear = year,
                                    GenreId = genre.Id
                                });
                                Console.WriteLine("Filmen har lagts till!");
                                Console.ReadKey();
                            }
                            break;
                        }
                    case MainMenuOption.DeleteMovie:
                        {
                            var choices = repository.GetAllMovies()
                                .Select(m => new MenuChoice<Movie>(m, $"{m.Title} ({m.ReleaseYear})"))
                                .ToList();
                            var result = new ConsoleMenu<Movie>("--Ta bort film--", choices).Show();

                            if (!result.WasCancelled)
                            {
                                bool deleted = repository.DeleteMovie(result.Value!.Id);
                                Console.WriteLine(deleted ? "Filmen togs bort." : "Ingen film togs bort.");
                                Console.ReadKey();
                            }

                            break;
                        }
                    case MainMenuOption.Exit:
                        isRunning = false;
                        break;
                }
            }

        }

        private static Genre? ChooseGenre(MovieRepository repository)
        {
            var choices = repository.GetAllGenres()
                .Select(g => new MenuChoice<Genre>(g, g.Name))
                .ToList();
            var result = new ConsoleMenu<Genre>("--Välj genre--", choices).Show();
            return result.WasCancelled ? null : result.Value;
        }
    }
}
