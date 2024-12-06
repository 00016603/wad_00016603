using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsPaperAPI_00016603.Data;
using NewsPaperAPI_00016603.DTOs;
using NewsPaperAPI_00016603.Models;
using NewsPaperAPI_00016603.Repositories;
using Swashbuckle.AspNetCore.Annotations;

namespace NewsPaperAPI_00016603.Controllers
{
    /// <summary>
    /// Controller for managing Category-related operations in the Issue Tracker API.
    /// </summary>
    /// <remarks>
    /// Provides endpoints for CRUD operations on Category entities.
    /// </remarks>
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoriesController"/> class.
        /// </summary>
        /// <param name="categoryRepository">Repository for accessing Category data.</param>
        /// <param name="mapper">Mapper for transforming entities to DTOs and vice versa.</param>
        public CategoriesController(IRepository<Category> categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        // GET: api/Categories
        /// <summary>
        /// Retrieves all categories from the database.
        /// </summary>
        /// <returns>List of categories as DTOs.</returns>
        /// <response code="200">Returns all categories.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet]
        [SwaggerResponse(200, "Returns All Categories", typeof(IEnumerable<CategoryDTO>))]
        [SwaggerResponse(500, "Internal Server Error")]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            var categories = await _categoryRepository.GetAllAsync();
            var categoryDtos = _mapper.Map<IEnumerable<CategoryDTO>>(categories);
            return Ok(categoryDtos);
        }

        // GET: api/Categories/5
        /// <summary>
        /// Retrieves a specific category by ID.
        /// </summary>
        /// <param name="id">The ID of the category to retrieve.</param>
        /// <returns>The category as a DTO.</returns>
        /// <response code="200">Returns the requested category.</response>
        /// <response code="404">Category not found.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet("{id}")]
        [SwaggerResponse(200, "Returns Category", typeof(CategoryDTO))]
        [SwaggerResponse(404, "Not Found")]
        [SwaggerResponse(500, "Internal Server Error")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            var categoryDto = _mapper.Map<CategoryDTO>(category);
            return Ok(categoryDto);
        }

        // PUT: api/Categories/5
        /// <summary>
        /// Updates an existing category.
        /// </summary>
        /// <param name="id">The ID of the category to update.</param>
        /// <param name="categoryDto">The updated category data.</param>
        /// <returns>No content.</returns>
        /// <response code="204">Update successful.</response>
        /// <response code="400">Invalid request.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPut("{id}")]
        [SwaggerResponse(204, "Returns No Content")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal Server Error")]
        public async Task<IActionResult> PutCategory(int id, CategoryDTO categoryDto)
        {
            if (id != categoryDto.Id)
            {
                return BadRequest();
            }

            var category = _mapper.Map<Category>(categoryDto);
            await _categoryRepository.UpdateAsync(category);
            return NoContent();
        }

        // POST: api/Categories
        /// <summary>
        /// Creates a new category.
        /// </summary>
        /// <param name="categoryDto">The data for the new category.</param>
        /// <returns>The created category as a DTO.</returns>
        /// <response code="201">Category created successfully.</response>
        /// <response code="400">Invalid request.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPost]
        [SwaggerResponse(201, "Returns Created")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal Server Error")]
        public async Task<ActionResult<Category>> PostCategory(CategoryDTO categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            await _categoryRepository.CreateAsync(category);
            var createdCategoryDto = _mapper.Map<CategoryDTO>(category);
            return CreatedAtAction(nameof(GetCategory), new { id = createdCategoryDto.Id }, createdCategoryDto);
        }

        // DELETE: api/Categories/5
        /// <summary>
        /// Deletes a category by ID.
        /// </summary>
        /// <param name="id">The ID of the category to delete.</param>
        /// <returns>No content.</returns>
        /// <response code="204">Delete successful.</response>
        /// <response code="400">Invalid request.</response>
        /// <response code="404">Category not found.</response>
        /// <response code="500">Internal server error.</response>
        [HttpDelete("{id}")]
        [SwaggerResponse(204, "Returns No Content")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(404, "Not Found")]
        [SwaggerResponse(500, "Internal Server Error")]
        public async Task<IActionResult> DeleteCategory(int id)        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            await _categoryRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
