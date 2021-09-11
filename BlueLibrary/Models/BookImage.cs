﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlueLibrary.Models
{
    public class BookImage
    {
        public int Id { get; set; }
        public String ImageURL { get; set; }

        [StringLength(50)]
        public String ImageDescription { get; set; }
        public Book Book { get; set; }
    }
}
