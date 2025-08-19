using Microsoft.EntityFrameworkCore;

namespace TrainingCentreWebApi.Models
{
    public class TrainingCentreDbContext : DbContext 
    {
        public TrainingCentreDbContext()
        {
            
        }
        public TrainingCentreDbContext(DbContextOptions<TrainingCentreDbContext> options) : base(options) 
        {
        }

        public virtual DbSet<Location> TrainingCentres { get; set; }
    }
}
