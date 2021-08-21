using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BlueLibrary.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public String BookName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public String Author { get; set; }
        public DateTime? ReleaseDate { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public String Description { get; set; }
        
        [ForeignKey("BookImage")]
        public int ImageId { get; set; }
        public BookImage Image { get; set; }

        public List<Genre> Genres { get; set; }

        [ForeignKey("Publisher")]
        public int PublisherId { get; set; }
        public Publisher Publisher { get; set; }
    }
}
