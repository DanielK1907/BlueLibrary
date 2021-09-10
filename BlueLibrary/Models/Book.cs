﻿using System;
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
        public String BookName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public String Author { get; set; }
        public DateTime? ReleaseDate { get; set; }

        [Required]
        [DataType(DataType.Text)]
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
