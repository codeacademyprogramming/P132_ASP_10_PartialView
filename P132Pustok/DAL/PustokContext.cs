﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using P132Pustok.Models;

namespace P132Pustok.DAL
{
    public class PustokContext:IdentityDbContext
    {
        public PustokContext(DbContextOptions<PustokContext> options):base(options)
        {

        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookImage> BookImages { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<BookTag> BookTags { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
    }
}
