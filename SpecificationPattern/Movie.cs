using System;
using ServiceStack.DataAnnotations;

namespace SpecificationPattern
{
    [Alias("Movies")]
    public class Movie
    {
        [AutoIncrement]
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public MpaaRating MpaaRating { get;set; }
        public string Genre { get; set; }
        public double Rating { get; set; }



        public Movie(string name, DateTime releaseDate, MpaaRating mpaaRating, string genre, double rating)
        {
            Name = name;
            ReleaseDate = releaseDate;
            MpaaRating = mpaaRating;
            Genre = genre;
            Rating = rating;
        }
    }

    [Flags]
    public enum MpaaRating
    {
        G = 1,
        PG13 = 2,
        R = 3
    }
}
