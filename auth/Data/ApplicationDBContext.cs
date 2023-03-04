using auth.Model;
using Microsoft.EntityFrameworkCore;

namespace auth.Data
{
    public class ApplicationDBContext:DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext>options)
            :base(options) { }
        
        public  DbSet<User> users { get; set; }
        public  DbSet<Student> students { get; set; }
        public DbSet<Department> departments { get; set; }
        public DbSet<Major> majors { get; set; }

    }
}
