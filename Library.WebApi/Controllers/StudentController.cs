using Library.WebApi.Models;
using Library.WebApi.Repositories.Interfaces;
using Library.WebApi.Repositories.RepositoryClasses;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Library.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private IRepositoryBase<Student> repository;

        public StudentController(IRepositoryBase<Student> repository)
        {
            this.repository = repository;
        }

        // GET: api/<StudentController>
        [HttpGet]
        public async Task<List<Student>> GetAsync()
        {
            return await repository.GetAllRecordsAsync();
        }

        // GET api/<StudentController>/5
        [HttpGet("{id}")]
        public async Task<Student> GetByIdAsync(int id)
        {
            return await repository.GetRecordByIdAsync(id);
        }

        // POST api/<StudentController>
        [HttpPost]
        public async Task<ActionResult> PostAsync(Student student)
        {
            await repository.InsertRecordAsync(student);
            return Ok();
        }

        // PUT api/<StudentController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutAsync(int id, Student student)
        {
            var existingRecord = await repository.GetRecordByIdAsync(id);

            if (existingRecord is null)
                return NotFound();

            existingRecord.FirstName= student.FirstName;
            existingRecord.LastName = student.LastName;
            existingRecord.Grade = student.Grade;

            await repository.UpdateRecordAsync(existingRecord);

            return Ok();
        }

        // DELETE api/<StudentController>/5
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
