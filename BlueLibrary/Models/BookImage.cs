using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlueLibrary.Models
{
    public class BookImage
    {
        public int Id { get; set; }
        public String ImageURL { get; set; }

        [StringLength(50)]
        [RegularExpression(@"^[aA-zZ0-9][aA-zZ0-9\s]*$",
         ErrorMessage = "Description must only contain english characters and digits.")]
        public String ImageDescription { get; set; }
        public Book Book { get; set; }
    }
}
