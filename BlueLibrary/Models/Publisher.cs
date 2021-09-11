﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlueLibrary.Models
{
    public class Publisher
    {
        public int Id { get; set; }

        [DisplayName("Publisher")]
        [StringLength(30)]
        public String Name { get; set; }
        public List<Book> BooksPublisher { get; set; }
    }
}
