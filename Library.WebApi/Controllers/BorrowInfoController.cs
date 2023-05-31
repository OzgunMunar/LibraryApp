using Library.WebApi.Models;
using Library.WebApi.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Library.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowInfoController : ControllerBase
    {
        private IRepositoryBase<BorrowInfo> repository;

        public BorrowInfoController(IRepositoryBase<BorrowInfo> repository)
        {
            this.repository = repository;
        }

        // GET: api/<BorrowInfoController>
        [HttpGet]
        public async Task<List<BorrowInfo>> GetAsync()
        {
            return await repository.GetAllRecordsAsync();
        }

        // GET api/<BorrowInfoController>/5
        [HttpGet("{id}")]
        public async Task<BorrowInfo> GetByIdAsync(int id)
        {
            return await repository.GetRecordByIdAsync(id);
        }

        // POST api/<BorrowInfoController>
        [HttpPost]
        public async Task<ActionResult> PostAsync(BorrowInfo borrowInfo)
        {
            await repository.InsertRecordAsync(borrowInfo);
            return Ok();
        }

        // PUT api/<BorrowInfoController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutAsync(int id, BorrowInfo borrowInfo)
        {
            var existingRecord = await repository.GetRecordByIdAsync(id);

            if (existingRecord is null)
                return NotFound();

            existingRecord.StudentId = borrowInfo.StudentId;
            existingRecord.BookId = borrowInfo.BookId;
            existingRecord.BorrowDate = borrowInfo.BorrowDate;

            await repository.UpdateRecordAsync(existingRecord);

            return Ok();
        }

        // DELETE api/<BorrowInfoController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            var seekingRecord = await repository.GetRecordByIdAsync(id);

            if (seekingRecord is null)
            {
                return NotFound();
            }

            await repository.RemoveRecordAsync(seekingRecord);
            return Ok();
        }
    }
}
