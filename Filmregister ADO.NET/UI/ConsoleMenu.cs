namespace Filmregister_ADO.NET.UI
{
    internal class ConsoleMenu<T>
    {
        private string _title = "";
        private List<List<MenuChoice<T>>> _choicePages;
        private List<MenuChoice<T>> _currentChoices;

        private int _index = 0;
        private int _currentPage = 0;



        internal ConsoleMenu(string title, List<MenuChoice<T>> choices)
        {
            _title = title;
            _choicePages = BuildPages(choices);
            _currentChoices = new List<MenuChoice<T>>();
        }
        private List<List<MenuChoice<T>>> BuildPages(List<MenuChoice<T>> choices)
        {
            return choices
                .Chunk(10)
                .Select(chunk =>
                chunk.ToList())
                .ToList();
        }

        internal MenuResult<T> Show()
        {
            if (_choicePages.Count == 0)
                return MenuResult<T>.Cancelled();
            while (true)
            {
                Console.Clear();
                Console.WriteLine(_title);

                _currentChoices = _choicePages[_currentPage];

                for (int choice = 0; choice < _currentChoices.Count; choice++)
                {
                    Console.Write(choice == _index ? "> " : "  ");
                    Console.WriteLine($"{_currentChoices[choice].Text}");
                }

                if (_choicePages.Count > 1)
                    PrintPageNumbers();

                Console.WriteLine("\nUse arrow keys to navigate.");

                Console.WriteLine("Enter to select");
                Console.WriteLine("Esc/Backspace to go back");
                Console.WriteLine();
                var key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        _index--;
                        if (_index < 0) _index = _currentChoices.Count - 1;
                        break;
                    case ConsoleKey.DownArrow:
                        _index++;
                        if (_index >= _currentChoices.Count) _index = 0;
                        break;
                    case ConsoleKey.RightArrow:
                        if (_choicePages.Count > 1)
                        {
                            _currentPage++;
                            if (_currentPage >= _choicePages.Count) _currentPage = 0;
                            ClampIndex();
                        }
                        break;

                    case ConsoleKey.LeftArrow:
                        if (_choicePages.Count > 1)
                        {
                            _currentPage--;
                            if (_currentPage < 0) _currentPage = _choicePages.Count - 1;

                            ClampIndex();
                        }
                        break;
                    case ConsoleKey.Enter:
                        return MenuResult<T>.Selected(_currentChoices[_index].Value);
                    case ConsoleKey.Escape or ConsoleKey.Backspace:
                        ResetPage();
                        return MenuResult<T>.Cancelled();
                }
            }
        }
        internal void SetChoices(List<MenuChoice<T>> choices)
        {
            _choicePages = BuildPages(choices);

            if (_choicePages.Count == 0)
            {
                _currentPage = 0;
                _index = 0;
                _currentChoices = new List<MenuChoice<T>>();
                return;
            }
            if (_currentPage >= _choicePages.Count)
                _currentPage = _choicePages.Count - 1;

            ClampIndex();
        }
        private void PrintPageNumbers()
        {
            Console.WriteLine();
            if (_choicePages.Count <= 10)
            {
                for (int page = 0; page < _choicePages.Count; page++)
                    PrintPageNumber(page);
            }
            else if (_choicePages.Count > 10 && _currentPage > 5 && _choicePages.Count - _currentPage < 6)
            {
                Console.Write(" ... ");
                for (int page = _currentPage - 4; page < _choicePages.Count; page++)
                    PrintPageNumber(page);
            }
            else if (_choicePages.Count > 10 && _currentPage > 5)
            {
                Console.Write(" ... ");
                for (int page = _currentPage - 4; page < _currentPage + 5; page++)
                    PrintPageNumber(page);
                Console.Write(" ... ");
            }
            else if (_choicePages.Count > 10)
            {
                for (int page = 0; page < 10; page++)
                    PrintPageNumber(page);
                Console.Write(" ... ");
            }
        }
        private void PrintPageNumber(int page)
        {
            bool isCurrent = page == _currentPage;
            string left = isCurrent ? ">" : " ";
            string right = isCurrent ? "<" : " ";
            Console.Write($"{left,-2}{page + 1,3}{right,2}");
        }
        internal void ClampIndex()
        {
            _currentChoices = _choicePages[_currentPage];

            if (_index >= _currentChoices.Count)
                _index = _currentChoices.Count - 1;
            if (_index < 0)
                _index = 0;
        }
        internal void ResetPage()
        {
            _currentPage = 0;
        }
        internal void SetTitle(string title)
        {
            _title = title;
        }
        internal void ResetIndex()
        {
            _index = 0;
        }
        internal void ResetMenu()
        {
            _index = 0;
            _currentPage = 0;
        }




    }
}
