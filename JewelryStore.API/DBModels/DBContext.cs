using Microsoft.EntityFrameworkCore;

namespace JewelryStore.API.DBModels
{
    /// <summary>
    /// DBContext to be used for EntityFramework connection with DB
    /// </summary>
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options)
            : base(options)
        {
        }

        //Database tables
        public DbSet<User> Users { get; set; }
    }
}
