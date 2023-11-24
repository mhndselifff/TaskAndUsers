using Microsoft.EntityFrameworkCore;
using TaskAndUsers.Models;

namespace TaskAndUsers.Data
{
    public class AppDbContextClass:DbContext
    {
        public DbSet<TaskUsersModel> Tasks { get; set; }

        public AppDbContextClass(DbContextOptions<AppDbContextClass> options):base(options) 
        { 
        }

       
    }
}
