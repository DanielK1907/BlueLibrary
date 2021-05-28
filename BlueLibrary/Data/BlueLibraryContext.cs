using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BlueLibrary.Models;

namespace BlueLibrary.Data
{
    public class BlueLibraryContext : DbContext
    {
        public BlueLibraryContext (DbContextOptions<BlueLibraryContext> options)
            : base(options)
        {
        }

        public DbSet<BlueLibrary.Models.Book> Book { get; set; }

        public DbSet<BlueLibrary.Models.Publisher> Publisher { get; set; }

        public DbSet<BlueLibrary.Models.BookImage> BookImage { get; set; }

        public DbSet<BlueLibrary.Models.Genre> Genre { get; set; }
    }
}
