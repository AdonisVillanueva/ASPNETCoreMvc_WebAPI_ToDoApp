using Microsoft.EntityFrameworkCore;

namespace ToDoApp.Models
{
    /// <summary>
    /// //For Web API examle using in memory database
    /// </summary>
    public class ToDoTempContext : DbContext
    {
        /// <summary>
        /// //Context options
        /// </summary>
        public ToDoTempContext(DbContextOptions<ToDoTempContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// //ToDoTempItems
        /// </summary>
        public DbSet<ToDoItem> ToDoTempItems { get; set; }
    }
}
