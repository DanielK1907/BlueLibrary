using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlueLibrary.Models
{
    public class Genre
    {
        public int Id { get; set; }

        [StringLength(20)]
        [RegularExpression(@"^[aA-zZ][aA-zZ\s]*$",
         ErrorMessage = "Name must only contain english characters.")]
        public String Name { get; set; }
        public List<Book> Books { get; set; }
    }
}
