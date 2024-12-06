using Microsoft.EntityFrameworkCore;
using NewsPaperAPI_00016603.Data;
using NewsPaperAPI_00016603.Models;

namespace NewsPaperAPI_00016603.Repositories
{
    /// <summary>
    /// Repository implementation for managing Category entities.
    /// </summary>
    public class CategoryRepository : IRepository<Category>
    {
        private readonly GeneralDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryRepository"/> class.
        /// </summary>
        /// <param name="context">The database context for Category operations.</param>
        public CategoryRepository(GeneralDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates a new Category entity in the database.
        /// </summary>
        /// <param name="entity">The Category entity to create.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task CreateAsync(Category entity)
        {
            await _context.CategoryDbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Deletes a Category entity from the database by its ID.
        /// </summary>
        /// <param name="id">The ID of the Category entity to delete.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task DeleteAsync(int id)
        {
            var category = await _context.CategoryDbSet.FindAsync(id);
            if (category != null)
            {
                _context.CategoryDbSet.Remove(category);
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Retrieves all Category entities from the database.
        /// </summary>
        /// <returns>A task that returns a collection of all Category entities.</returns>
        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.CategoryDbSet.ToListAsync();
        }

        /// <summary>
        /// Retrieves a Category entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the Category entity to retrieve.</param>
        /// <returns>A task that returns the Category entity if found, or null if not.</returns>
        public async Task<Category> GetByIdAsync(int id)
        {
            return await _context.CategoryDbSet.FindAsync(id);
        }

        /// <summary>
        /// Updates an existing Category entity in the database.
        /// </summary>
        /// <param name="entity">The Category entity with updated values.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task UpdateAsync(Category entity)
        {
            _context.CategoryDbSet.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
