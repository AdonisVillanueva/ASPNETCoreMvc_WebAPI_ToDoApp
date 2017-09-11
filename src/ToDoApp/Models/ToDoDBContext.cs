using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

  
namespace ToDoApp.Models
{
    /// <summary>
    /// To do model for the Physical database implementation. Derives from the DbContext
    /// </summary>
    public class ToDoDBContext : DbContext
    {
        private IConfiguration _config;

        /// <summary>
        /// ToDoDBContext
        /// </summary>
        public ToDoDBContext(IConfiguration config)
        {
            _config = config;
        }

        /// <summary>
        /// TodoItems
        /// </summary>
        public DbSet<ToDoItem> TodoItems { get; set; }

        /// <summary>
        /// Options builder for SqlServer
        /// </summary>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_config["Database:Connection"]);
        }
    }
}
