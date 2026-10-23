using System;
using System.Collections.Generic;
using System.Text;

namespace Filmregister_ADO.NET.Models
{
    internal class Movie
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public int ReleaseYear { get; set; }
        public int GenreId { get; set; }
        public string GenreName { get; set; } = "";
    }
}
