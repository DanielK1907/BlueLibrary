using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlueLibrary.Models
{
    public class Publisher
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public List<Book> BooksPublisher { get; set; }
    }
}
