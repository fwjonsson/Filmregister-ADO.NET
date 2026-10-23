using Filmregister_ADO.NET.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Filmregister_ADO.NET.Menus
{
    internal enum MainMenuOption
    {
        ShowAllMovies,
        SearchMovieByGenre,
        AddMovie,
        DeleteMovie,
        Exit
    }
    
    internal class MainMenu
    {
        private readonly ConsoleMenu<MainMenuOption> _menu;
        internal MainMenu()
        {
            _menu = new ConsoleMenu<MainMenuOption>("--Main Menu--", new List<MenuChoice<MainMenuOption>>
            {
                new(MainMenuOption.ShowAllMovies, "Visa alla filmer"),
                new(MainMenuOption.SearchMovieByGenre, "Sök filmer efter genre"),
                new(MainMenuOption.AddMovie, "Lägg till film"),
                new(MainMenuOption.DeleteMovie, "Ta bort film"),
                new(MainMenuOption.Exit, "Avsluta")
            });
        }
        internal MainMenuOption Show()
        {
            var result = _menu.Show();
            if (result.WasCancelled) 
                return MainMenuOption.Exit;

            return result.Value;
        }
        internal void ResetIndex() => _menu.ResetMenu();
    }
}
