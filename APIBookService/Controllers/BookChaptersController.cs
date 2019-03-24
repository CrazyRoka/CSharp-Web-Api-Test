using BooksServices.Models;
using BooksServices.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace APIBookService.Controllers
{
    [Produces("application/json", "application/xml")]
    [Route("api/[controller]")]
    public class BookChaptersController : Controller
    {
        private readonly IBookChaptersService _bookChaptersService;
        public BookChaptersController(IBookChaptersService bookChaptersService)
        {
            _bookChaptersService = bookChaptersService;
        }

        //GET api/bookchapters
        [HttpGet]
        public Task<IEnumerable<BookChapter>> GetBookChaptersAsync() =>
            _bookChaptersService.GetAllAsync();

        //GET api/bookchapters/guid
        [HttpGet("{id}", Name = nameof(GetBookChapterByIdAsync))]
        public async Task<IActionResult> GetBookChapterByIdAsync(Guid id)
        {
            BookChapter chapter = await _bookChaptersService.FindAsync(id);
            if(chapter == null)
            {
                return NotFound();
            }
            else
            {
                return new ObjectResult(chapter);
            }
        }

        /// <summary>
        /// Creates a BookChapter
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///     POST api/bookchapters
        ///     {
        ///         Number: 42,
        ///         Title: "Sample Title",
        ///         Pages: 98
        ///     }
        /// </remarks>
        /// <param name="chapter"></param>
        /// <returns>A newly created book chapter</returns>
        /// <response code="201">Returns the newly created book chapter</response>
        /// <response code="400">If the chapter is null</response>
        [HttpPost]
        [ProducesResponseType(typeof(BookChapter), 200)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> PostBookChapterAsync([FromBody]BookChapter chapter)
        {
            if(chapter == null)
            {
                return BadRequest();
            }
            await _bookChaptersService.AddAsync(chapter);
            return CreatedAtRoute(nameof(GetBookChapterByIdAsync), new { id = chapter.Id }, chapter);
        }

        //PUT api/bookchapters/guid
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBookChapterAsync(Guid id, [FromBody]BookChapter chapter)
        {
            if(chapter == null || id != chapter.Id)
            {
                return BadRequest();
            }
            if(await _bookChaptersService.FindAsync(id) == null)
            {
                return NotFound();
            }
            await _bookChaptersService.UpdateAsync(chapter);
            return new NoContentResult();
        }

        //DELETE api/bookchapters/guid
        [HttpDelete("{id}")]
        public async Task Delete(Guid id) =>
            await _bookChaptersService.RemoveAsync(id);
    }
}
