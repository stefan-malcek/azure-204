using Microsoft.EntityFrameworkCore;

namespace corewebapi.Model
{
    public class CourseContext : DbContext
    {
        // Replace your connection string here
        readonly string connectionstring = "";
        public DbSet<Course> Courses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(connectionstring);
            base.OnConfiguring(options);
        }
    }


}
