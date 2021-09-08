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

        public DbSet<Book> Book { get; set; }

        public DbSet<Publisher> Publisher { get; set; }

        public DbSet<BookImage> BookImage { get; set; }

        public DbSet<Genre> Genre { get; set; }

        public DbSet<User> User { get; set; }
    }
}
