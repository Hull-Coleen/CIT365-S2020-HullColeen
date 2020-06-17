using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace MvcMovie.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MvcMovieContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<MvcMovieContext>>()))
            {
                // Look for any movies.
                if (context.Movie.Any())
                {
                    return;   // DB has been seeded
                }

                context.Movie.AddRange(
                    new Movie
                    {
                        Title = "Once I Was A Beehive",
                        ReleaseDate = DateTime.Parse("2002-4-21"),
                        Genre = "Family",
                        Rating = "G",
                        Price = 14.99M,
                        Image = "/images/beehive.jpg"
                    },

                    new Movie
                    {
                        Title = "17 Miracles",
                        ReleaseDate = DateTime.Parse("2013-1-6"),
                        Genre = "Drama",
                        Rating = "G",
                        Price = 8.99M,
                        Image = "/images/miracles.jpg"
                    },

                    new Movie
                    {
                        Title = "The Otherside Of Heaven",
                        ReleaseDate = DateTime.Parse("1990-4-17"),
                        Genre = "Drama",
                        Rating = "G",
                        Price = 9.99M,
                        Image = "/images/heaven.jpg"
                    },

                    new Movie
                    {
                        Title = "Meet the Mormons",
                        ReleaseDate = DateTime.Parse("1985-4-15"),
                        Genre = "Comedy",
                        Rating = "G",
                        Price = 3.99M,
                        Image = "/images/meetMormon.jpg"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
