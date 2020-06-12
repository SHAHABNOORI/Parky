using Microsoft.EntityFrameworkCore;
using Parky.Api.Models;

namespace Parky.Api.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<NationalPark> NationalParks { get; set; }
    }
}