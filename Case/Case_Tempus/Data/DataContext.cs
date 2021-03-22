using CaseTempus.Models;
using Microsoft.EntityFrameworkCore;

namespace CaseTempus.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) 
            : base(options)
        {
        }

        public DbSet<UserViewModel> Users { get; set; }
    }
}
