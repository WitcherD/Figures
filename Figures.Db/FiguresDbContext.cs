using Figures.Db.Entities;
using Microsoft.EntityFrameworkCore;

namespace Figures.Db
{
    public class FiguresDbContext : DbContext
    {
        public FiguresDbContext(DbContextOptions<FiguresDbContext> options) : base(options)
        {
        }

        public DbSet<Figure> Figures { get; set; }
    }
}
