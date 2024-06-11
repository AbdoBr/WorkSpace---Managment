using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using WorkSpace___Managment.Models.BookingsModel;
using WorkSpace___Managment.Repositories.Bookings;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WorkSpace___Managment.Controllers.Booking
{
    [Route("api/[controller]")]
    public class BookingHallController : Controller
    {

        private readonly IBookingsRepository _bookinsRepository;
        private readonly IHallSettingRepository _hallSettingRepository;


        public BookingHallController(IBookingsRepository bookingRepository,IHallSettingRepository hallSettingRepository)
        {
            _bookinsRepository = bookingRepository;
            _hallSettingRepository = hallSettingRepository;
        }

        [HttpGet("halls")]
        public async Task<ActionResult<List<HallSetting>>> GetHallSettings()
        {
            var hallSettings = await _hallSettingRepository.GetAll();
            return Ok(hallSettings);
        }

        [HttpGet("price")]
        public async Task<ActionResult<float>> GetPrice(string hallName, string bookingType)
        {
            var hallSetting = await _hallSettingRepository.GetHallByNameAndTypeAsync(hallName, bookingType);
            if (hallSetting == null)
            {
                return BadRequest("Invalid HallName or BookingType");
            }
            return Ok(hallSetting.Price);
        }

        [HttpPost("book")]
        public async Task<ActionResult<BookingHall>> CreateBooking([FromBody] BookingHall booking)
        {
            var hallSetting = await _hallSettingRepository.GetHallByNameAndTypeAsync(booking.HallName, booking.BookingType);
            if (hallSetting == null)
            {
                return BadRequest("Invalid HallName or BookingType");
            }

            booking.Price = hallSetting.Price;

            // Ensure Status is set to "Active" by default
            if (string.IsNullOrEmpty(booking.Status))
            {
                booking.Status = "Active";
            }

            var createdBooking = await _bookinsRepository.Create(booking);
            return CreatedAtAction(nameof(CreateBooking), new { id = createdBooking.BookingID }, createdBooking);
        }


        [HttpGet("get/{id}")]
        public async Task<IActionResult> Get(String id)
        {
            var bookingHall = await _bookinsRepository.Get(ObjectId.Parse(id));
            return new JsonResult(bookingHall);
        }

        [HttpGet("getbyName/{Name}")]
        public async Task<IActionResult> GetAllByOrganizationName(String Name)
        {
            var bookingHall = await _bookinsRepository.GetAllByOrganizationName(Name);
            return new JsonResult(bookingHall);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, BookingHall bookingHall)
        {
            if (!ObjectId.TryParse(id, out var objectId))
            {
                return BadRequest("Invalid ID format");
            }

            try
            {
                var updatedBookingHall = await _bookinsRepository.Update(objectId, bookingHall);
                return new JsonResult(updatedBookingHall);
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
            var bookingHall = await _bookinsRepository.Delete(ObjectId.Parse(id));
            return new JsonResult(bookingHall);
        }

        [HttpGet("Fetch")]
        public async Task<IActionResult> GetAll()
        {
            var bookingHall = await _bookinsRepository.GetAll();
            return new JsonResult(bookingHall);
        }

    }
}

