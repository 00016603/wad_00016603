using NewsPaperAPI_00016603.Models;
using Microsoft.EntityFrameworkCore;


namespace NewsPaperAPI_00016603.Data
{
    

        /// <summary>
        /// Represents the database context for the Issue Tracker application.
        /// </summary>
        public class GeneralDbContext : DbContext
        {

            /// Initializes a new instance of the GeneralDbContext class
            public GeneralDbContext(DbContextOptions<GeneralDbContext> o) : base(o) { }


            /// Gets or sets the DbSet for managing Category entities.
            public DbSet<Category> CategoryDbSet { get; set; }


            /// Gets or sets the DbSet for managing Employee entities.
            public DbSet<News> NewsDbSet { get; set; }
        
    }

}
