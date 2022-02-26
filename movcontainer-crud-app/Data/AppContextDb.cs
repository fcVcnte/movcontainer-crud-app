using Microsoft.EntityFrameworkCore;
using movcontainer_crud_app.Models;

namespace movcontainer_crud_app.Data
{
    public class AppContextDb : DbContext
    {
        public AppContextDb(DbContextOptions<AppContextDb> options) : base(options)
        {

        }
        public DbSet<Container> Containers { get; set; }
        public DbSet<Movement> Movements { get; set; }
    }
}
