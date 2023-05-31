using Library.WebApi.Models;
using Library.WebApi.Repositories.Interfaces;
using Library.WebApi.Repositories.RepositoryClasses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace Library.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {

        private IRepositoryBase<Book> repository;

        public BookController(IRepositoryBase<Book> repository)
        {
            this.repository = repository;
        }

        // GET: api/<BookController>
        [HttpGet]
        public async Task<List<Book>> GetAsync()
        {
            return await repository.GetAllRecordsAsync();
        }

        // GET api/<BookController>/5
        [HttpGet("{id}")]
        public async Task<Book> GetByIdAsync(int id)
        {
            return await repository.GetRecordByIdAsync(id);
        }

        // POST api/<BookController>
        [HttpPost]
        public async Task<ActionResult> PostAsync(Book book)
        {
            await repository.InsertRecordAsync(book);
            return Ok();
        }

        // PUT api/<BookController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutAsync(int id, Book book)
        {

            var existingRecord = await repository.GetRecordByIdAsync(id);

            if (existingRecord is null)
                return NotFound();

            existingRecord.BookAuthor = book.BookAuthor;
            existingRecord.BookTitle = book.BookTitle;
            existingRecord.BookPublisher = book.BookPublisher;

            await repository.UpdateRecordAsync(existingRecord);

            return Ok();
        }

        // DELETE api/<BookController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {

            var seekingRecord = await repository.GetRecordByIdAsync(id);

            if(seekingRecord is null)
            {
                return NotFound();
            }

            await repository.RemoveRecordAsync(seekingRecord);
            return Ok();

        }
    }
}
