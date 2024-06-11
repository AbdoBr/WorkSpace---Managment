using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using WorkSpace___Managment.Models.SnacksModel;
using WorkSpace___Managment.Repositories.SnacksRepositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WorkSpace___Managment.Controllers.SnackController
{
    [Route("api/[controller]")]
    public class SnacksController : Controller
    {
        private readonly ISnacksRepository _snacksRepository;
        public SnacksController(ISnacksRepository snackstRepository)
        {
            _snacksRepository = snackstRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Snacks snacks)
        {
            var id = await _snacksRepository.Create(snacks);
            //var createdSnack = await _snacksRepository.Get(id);
            return new JsonResult(id.ToString());
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> Get(String id)
        {
            var Snacks = await _snacksRepository.Get(ObjectId.Parse(id));
            return new JsonResult(Snacks);
        }

        [HttpGet("getbyName/{Name}")]
        public async Task<IActionResult> GetbyName(String Name)
        {
            var Snacks = await _snacksRepository.GetAllByName(Name);
            return new JsonResult(Snacks);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Snacks snack)
        {
            if (!ObjectId.TryParse(id, out var objectId))
            {
                return BadRequest("Invalid ID format");
            }

            try
            {
                var updatedSnack = await _snacksRepository.Update(objectId, snack);
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
            var Snacks = await _snacksRepository.Delete(ObjectId.Parse(id));
            return new JsonResult(Snacks);
        }

        [HttpGet("Fetch")]
        public async Task<IActionResult> GetAll()
        {
            var Snacks = await _snacksRepository.GetAll();
            return new JsonResult(Snacks);
        }
    }
}

