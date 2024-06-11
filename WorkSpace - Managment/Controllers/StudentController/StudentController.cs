using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using WorkSpace___Managment.Models.StudentModel;
using WorkSpace___Managment.Repositories.StudentRepository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WorkSpace___Managment.Controllers.StudentController
{
    [Route("api/[controller]")]
    public class StudentController : Controller
    {

        private readonly IStudentRepository _studentRepository;
        public StudentController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Students student)
        {
            var id = await _studentRepository.Create(student);
            var createdStudent = await _studentRepository.Get(id);
            return new JsonResult(createdStudent);
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> Get(String id)
        {
            var Student = await _studentRepository.Get(ObjectId.Parse(id));
            return new JsonResult(Student);
        }

        [HttpGet("getbyName/{Name}")]
        public async Task<IActionResult> GetbyName(String Name)
        {
            var Student = await _studentRepository.GetAllByName(Name);
            return new JsonResult(Student);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id,Students student)
        {
            if (!ObjectId.TryParse(id, out var objectId))
            {
                return BadRequest("Invalid ID format");
            }

            try
            {
                var updatedSnack = await _studentRepository.Update(objectId, student);
                return new JsonResult(updatedSnack);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var Student = await _studentRepository.Delete(ObjectId.Parse(id));
            return new JsonResult(Student);
        }

        [HttpGet("Fetch")]
        public async Task<IActionResult> GetAll()
        {
            var Student = await _studentRepository.GetAll();
            return new JsonResult(Student);
        }
    }
}

