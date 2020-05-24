using System;
using Xunit;
using Microsoft.EntityFrameworkCore;
using Studio.Models;
using Studio.Controllers;

namespace vf_gv_jubl_j
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var options = new DbContextOptionsBuilder<StudioContext>()
                .UseInMemoryDatabase(databaseName: "Db")
                .Options;

            var context = new StudioContext(options);

            Seed(context);

            var query = new GenresController(context);

            var result = query.Execute();

            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void Test2()
        {
            var options = new DbContextOptionsBuilder<StudioContext>()
                .UseInMemoryDatabase(databaseName: "Db")
                .Options;

            var context = new StudioContext(options);

            Seed(context);

            var query = new GenresController(context);

            var result = query.Delete(1);

            Assert.True(result);
        }

        private void Seed(StudioContext context)
        {
            var categories = new[]
            {
                new Genres { Name = "AAAA" },
                new Genres { Name = "BBBB" },

            };
            context.Genres.AddRange(categories);
            context.SaveChanges();
        }
    }
}
