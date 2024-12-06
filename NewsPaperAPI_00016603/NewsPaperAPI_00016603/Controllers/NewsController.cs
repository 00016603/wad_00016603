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
{/// <summary>
 /// Controller for managing News-related operations in the Issue Tracker API.
 /// </summary>
 /// <remarks>
 /// Provides endpoints for CRUD operations on News entities.
 /// </remarks>
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly IRepository<News> _newsRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="NewsController"/> class.
        /// </summary>
        /// <param name="newsRepository">Repository for accessing News data.</param>
        /// <param name="mapper">Mapper for transforming entities to DTOs and vice versa.</param>
        public NewsController(IRepository<News> newsRepository, IMapper mapper)
        {
            _newsRepository = newsRepository;
            _mapper = mapper;
        }

        // GET: api/News
        /// <summary>
        /// Retrieves all news items from the database, including related category data.
        /// </summary>
        /// <returns>List of news items as DTOs.</returns>
        /// <response code="200">Returns all news items.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet]
        [SwaggerResponse(200, "Returns All News", typeof(IEnumerable<NewsDTO>))]
        [SwaggerResponse(500, "Internal Server Error")]
        public async Task<ActionResult<IEnumerable<News>>> GetNews()
        {
            var news = await _newsRepository.GetAllAsync();
            var newsDtos = _mapper.Map<IEnumerable<NewsDTO>>(news);
            return Ok(newsDtos);
        }

        // GET: api/News/5
        /// <summary>
        /// Retrieves a specific news item by ID, including related category data.
        /// </summary>
        /// <param name="id">The ID of the news item to retrieve.</param>
        /// <returns>The news item as a DTO.</returns>
        /// <response code="200">Returns the requested news item.</response>
        /// <response code="404">News item not found.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet("{id}")]
        [SwaggerResponse(200, "Returns News Item", typeof(NewsDTO))]
        [SwaggerResponse(404, "Not Found")]
        [SwaggerResponse(500, "Internal Server Error")]
        public async Task<ActionResult<News>> GetNews(int id)
        {
            var newsItem = await _newsRepository.GetByIdAsync(id);
            if (newsItem == null)
            {
                return NotFound();
            }

            var newsDto = _mapper.Map<NewsDTO>(newsItem);
            return Ok(newsDto);
        }

        // PUT: api/News/5
        /// <summary>
        /// Updates an existing news item.
        /// </summary>
        /// <param name="id">The ID of the news item to update.</param>
        /// <param name="newsDto">The updated news data.</param>
        /// <returns>No content.</returns>
        /// <response code="204">Update successful.</response>
        /// <response code="400">Invalid request.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPut("{id}")]
        [SwaggerResponse(204, "Returns No Content")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal Server Error")]
        public async Task<IActionResult> PutNews(int id, NewsDTO newsDto)
        {
            if (id != newsDto.Id)
            {
                return BadRequest();
            }

            var news = _mapper.Map<News>(newsDto);
            await _newsRepository.UpdateAsync(news);
            return NoContent();
        }

        // POST: api/News
        /// <summary>
        /// Creates a new news item.
        /// </summary>
        /// <param name="newsDto">The data for the new news item.</param>
        /// <returns>The created news item as a DTO.</returns>
        /// <response code="201">News item created successfully.</response>
        /// <response code="400">Invalid request.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPost]
        [SwaggerResponse(201, "Returns Created")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(500, "Internal Server Error")]
        public async Task<ActionResult<News>> PostNews(NewsDTO newsDto)
        {
            var news = _mapper.Map<News>(newsDto);
            await _newsRepository.CreateAsync(news);
            var createdNewsDto = _mapper.Map<NewsDTO>(news);
            return CreatedAtAction(nameof(GetNews), new { id = createdNewsDto.Id }, createdNewsDto);
        }

        // DELETE: api/News/5
        /// <summary>
        /// Deletes a news item by ID.
        /// </summary>
        /// <param name="id">The ID of the news item to delete.</param>
        /// <returns>No content.</returns>
        /// <response code="204">Delete successful.</response>
        /// <response code="400">Invalid request.</response>
        /// <response code="404">News item not found.</response>
        /// <response code="500">Internal server error.</response>
        [HttpDelete("{id}")]
        [SwaggerResponse(204, "Returns No Content")]
        [SwaggerResponse(400, "Bad Request")]
        [SwaggerResponse(404, "Not Found")]
        [SwaggerResponse(500, "Internal Server Error")]
        public async Task<IActionResult> DeleteNews(int id)
        {
            var newsItem = await _newsRepository.GetByIdAsync(id);
            if (newsItem == null)
            {
                return NotFound();
            }

            await _newsRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
