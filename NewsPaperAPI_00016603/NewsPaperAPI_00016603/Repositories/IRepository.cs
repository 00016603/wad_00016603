using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewsPaperAPI_00016603.Repositories
{
    public interface IRepository<T>
    {

            /// <summary>
            /// Creates a new entity in the database.
            /// </summary>
            /// <param name="entity">The entity to create.</param>
            /// <returns>A task representing the asynchronous operation.</returns>
            Task CreateAsync(T entity);

            /// <summary>
            /// Updates an existing entity in the database.
            /// </summary>
            /// <param name="entity">The entity with updated values.</param>
            /// <returns>A task representing the asynchronous operation.</returns>
            Task UpdateAsync(T entity);

            /// <summary>
            /// Deletes an entity from the database by its ID.
            /// </summary>
            /// <param name="id">The ID of the entity to delete.</param>
            /// <returns>A task representing the asynchronous operation.</returns>
            Task DeleteAsync(int id);

            /// <summary>
            /// Retrieves all entities from the database.
            /// </summary>
            /// <returns>A task that returns a collection of all entities.</returns>
            Task<IEnumerable<T>> GetAllAsync();

            /// <summary>
            /// Retrieves an entity by its ID.
            /// </summary>
            /// <param name="id">The ID of the entity to retrieve.</param>
            /// <returns>A task that returns the entity if found, or null if not.</returns>
            Task<T> GetByIdAsync(int id);
        }
    }

