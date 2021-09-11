using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlueLibrary.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [RegularExpression(@"^[aA-zZ0-9][aA-zZ0-9\s]{0,14}$",
         ErrorMessage = "Name must contain up to 15 english characters and digits.")]
        public String BookName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [RegularExpression(@"^[aA-zZ][aA-zZ\s]{0,14}$",
         ErrorMessage = "Author must contain up to 15 english characters.")]
        public String Author { get; set; }
        public DateTime? ReleaseDate { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [RegularExpression(@"^[aA-zZ0-9][aA-zZ0-9\s]{0,49}$",
         ErrorMessage = "Description must contain up to 50 english characters and digits.")]
        public String Description { get; set; }
        
        [ForeignKey("BookImage")]
        public int? ImageId { get; set; }

        #nullable enable
        public BookImage? Image { get; set; }

        #nullable enable
        public List<Genre>? Genres { get; set; }

        [ForeignKey("Publisher")]
        public int PublisherId { get; set; }

        #nullable enable
        public Publisher? Publisher { get; set; }
    }
}
