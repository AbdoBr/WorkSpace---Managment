using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using WorkSpace___Managment.Models.BookingsModel;
using WorkSpace___Managment.Repositories.Bookings;
using WorkSpace___Managment.Repositories.BookingsRepository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WorkSpace___Managment.Controllers.Booking
{
    [Route("api/[controller]")]
    public class HallSettingController : Controller
    {

        private readonly IHallSettingRepository _hallSettingRepository;
        public HallSettingController(IHallSettingRepository hallSettingRepository)
        {
            _hallSettingRepository = hallSettingRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Create(HallSetting hallSetting)
        {
            var id = await _hallSettingRepository.Create(hallSetting);
            //var createdHall = await _hallSettingRepository.Get(id);
            return new JsonResult(id.ToString());
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> Get(String id)
        {
            var hallSetting = await _hallSettingRepository.Get(ObjectId.Parse(id));
            return new JsonResult(hallSetting);
        }

        [HttpGet("getbyName/{Name}")]
        public async Task<IActionResult> GetAllByName(String Name)
        {
            var hallSetting = await _hallSettingRepository.GetAllByName(Name);
            return new JsonResult(hallSetting);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, HallSetting hallSetting)
        {
            if (!ObjectId.TryParse(id, out var objectId))
            {
                return BadRequest("Invalid ID format");
            }

            try
            {
                var updatedhallSetting = await _hallSettingRepository.Update(objectId, hallSetting);
                return new JsonResult(updatedhallSetting);
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
            var hallSetting = await _hallSettingRepository.Delete(ObjectId.Parse(id));
            return new JsonResult(hallSetting);
        }

        [HttpGet("Fetch")]
        public async Task<IActionResult> GetAll()
        {
            var hallSetting = await _hallSettingRepository.GetAll();
            return new JsonResult(hallSetting);
        }

    }
}

