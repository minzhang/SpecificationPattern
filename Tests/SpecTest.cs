using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using ServiceStack.OrmLite.SqlServer;
using ServiceStack.OrmLite;

using SpecificationPattern;
using System.Data;
using System.Collections.Generic;

namespace Tests
{
    [TestClass]
    public class SpecTest
    {
        [TestMethod]
        public void ConnectToLocalDB()
        {
            string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Movies;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            var dbFactory = new OrmLiteConnectionFactory(
                                    connectionString,
                                    SqlServerDialect.Provider);

            using (IDbConnection db = dbFactory.Open())
            {
                //db.CreateTableIfNotExists<Movie>();
                //var movie = new Movie("Some Movie", new DateTime(2010, 2, 1), MpaaRating.G, "Triller", 6.0);

                //db.Save(movie);

                //var firstMovie = db.Select<Movie>(x => x.Id == 1);         
            }
        }

        [TestMethod]
        public void T1()
        {
            var gRating = new MpaaRatingAtMostSpecification(MpaaRating.G);
            var repository = new MovieRepository();

            IReadOnlyList<Movie> movies = repository.Find(gRating);

            Assert.AreEqual(1, movies.Count);
        }

        [TestMethod]
        public void T1_1()
        {
            var rRating = new MpaaRatingAtMostSpecification(MpaaRating.R);
            var repository = new MovieRepository();

            IReadOnlyList<Movie> movies = repository.Find(rRating);

            Assert.AreEqual(3, movies.Count);
        }


        [TestMethod]
        public void T2()
        {
            var movie = new Movie("Some Movie", new DateTime(2010, 2, 1), MpaaRating.R, "Triller", 7);
            var pg13Rating = new MpaaRatingAtMostSpecification(MpaaRating.PG13);

            bool isSatisfiedBy = pg13Rating.IsSatisfiedBy(movie);

            Assert.AreEqual(false, isSatisfiedBy);
        }


        [TestMethod]
        public void T3()
        {
            var movie = new Movie("Some Movie", new DateTime(2010, 2, 1), MpaaRating.G, "Triller", 7);
            var pg13Rating = new MpaaRatingAtMostSpecification(MpaaRating.PG13);

            bool isSatisfiedBy = pg13Rating.IsSatisfiedBy(movie);

            Assert.AreEqual(true, isSatisfiedBy);
        }


        [TestMethod]
        public void T4()
        {
            var gRating = new MpaaRatingAtMostSpecification(MpaaRating.G);
            var goodMovie = new GoodMovieSpecification();
            var repository = new MovieRepository();

            IReadOnlyList<Movie> movies = repository.Find(gRating.And(goodMovie));

            Assert.AreEqual(0, movies.Count);
        }


        [TestMethod]
        public void T5()
        {
            var gRating = new MpaaRatingAtMostSpecification(MpaaRating.PG13);
            var goodMovie = new GoodMovieSpecification();
            var repository = new MovieRepository();

            IReadOnlyList<Movie> movies = repository.Find(gRating.Or(goodMovie));

            Assert.AreEqual(2, movies.Count);
        }

        [TestMethod]
        public void T6()
        {
            var movie = new Movie("Some Movie", new DateTime(2010, 2, 1), MpaaRating.G, "Triller", 10);
            var pg13Rating = new MpaaRatingAtMostSpecification(MpaaRating.PG13);
            var goodMovie = new GoodMovieSpecification();
            var composed = pg13Rating.And(goodMovie);

            bool isSatisfiedBy = composed.IsSatisfiedBy(movie);

            Assert.AreEqual(true,isSatisfiedBy);
        }
    }
}
