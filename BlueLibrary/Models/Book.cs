using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlueLibrary.Models
{
    public class Book
    {
        public int Id { get; set; }
        public String BookName { get; set; }
        public String Author { get; set; }
        public DateTime ReleaseDate { get; set; }
        public String Description { get; set; }
        public BookImage Image { get; set; }
        public List<Genre> Genres { get; set; }
        public Publisher BookPublisher { get; set; }
    }
}
