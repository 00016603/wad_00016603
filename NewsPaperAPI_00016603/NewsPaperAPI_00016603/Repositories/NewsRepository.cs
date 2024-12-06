using Microsoft.EntityFrameworkCore;
using NewsPaperAPI_00016603.Data;
using NewsPaperAPI_00016603.Models;

namespace NewsPaperAPI_00016603.Repositories
{

    /// <summary>
    /// Repository implementation for managing News entities.
    /// </summary>
    public class NewsRepository : IRepository<News>
    {
        private readonly GeneralDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="NewsRepository"/> class.
        /// </summary>
        /// <param name="context">The database context for News operations.</param>
        public NewsRepository(GeneralDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates a new News entity in the database.
        /// </summary>
        /// <param name="entity">The News entity to create.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task CreateAsync(News entity)
        {
            await _context.NewsDbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes a News entity from the database by its ID.
        /// </summary>
        /// <param name="id">The ID of the News entity to delete.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task DeleteAsync(int id)
        {
            var news = await _context.NewsDbSet.FindAsync(id);
            if (news != null)
            {
                _context.NewsDbSet.Remove(news);
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Retrieves all News entities from the database, including related Category data.
        /// </summary>
        /// <returns>A task that returns a collection of all News entities with related Category data.</returns>
        public async Task<IEnumerable<News>> GetAllAsync()
        {
            return await _context.NewsDbSet.Include(n => n.Category).ToListAsync();
        }

        /// <summary>
        /// Retrieves a News entity by its ID, including related Category data.
        /// </summary>
        /// <param name="id">The ID of the News entity to retrieve.</param>
        /// <returns>A task that returns the News entity if found, or null if not.</returns>
        public async Task<News> GetByIdAsync(int id)
        {
            return await _context.NewsDbSet.Include(n => n.Category).FirstOrDefaultAsync(n => n.Id == id);
        }

        /// <summary>
        /// Updates an existing News entity in the database.
        /// </summary>
        /// <param name="entity">The News entity with updated values.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task UpdateAsync(News entity)
        {
            _context.NewsDbSet.Update(entity);
            await _context.SaveChangesAsync();
        }
    }

}
