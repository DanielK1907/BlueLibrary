using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        
        // 1-1 relationship
        public int BookImageId { get; set; }
        public BookImage BookImage { get; set; }

        // M2M relationship
        public List<Genre> Genres { get; set; }

        // 1 to Many relationship
        public int PublisherId { get; set; }
        public Publisher Publisher { get; set; }
    }
}
