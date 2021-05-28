using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlueLibrary.Models
{
    public class Genre
    {
        public int Id { get; set; }

        public List<Book> books;
    }
}
