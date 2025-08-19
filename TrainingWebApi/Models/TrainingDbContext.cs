using Microsoft.EntityFrameworkCore;

namespace TrainingWebApi.Models
{
    public class TrainingDbContext : DbContext
    {
        public TrainingDbContext()
        {

        }

        public TrainingDbContext(DbContextOptions<TrainingDbContext> options) : base(options)
        {

        }

        //declare the DbSet properties which maps and queries the database tables
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Student> Students { get; set; }

    }
}
