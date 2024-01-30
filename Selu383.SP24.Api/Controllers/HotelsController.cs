using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Selu383.SP24.Api.Models;
using System.Net;

namespace Selu383.SP24.Api.Controllers
{
    [ApiController]
    [Route("api/hotels")]
    public class HotelController : ControllerBase
    {
        private readonly ILogger<HotelController> _logger;
        private readonly DataContext _context; // Inject DataContext

        public HotelController(ILogger<HotelController> logger, DataContext context)
        {
            _logger = logger;
            _context = context; // Initialize DataContext
        }

        [HttpGet]
        public async Task<ActionResult<List<HotelDTO>>> ListAllHotels()
        {
            var resultDto = await _context.Hotel
                .Select(h => new HotelDTO
                {
                    Id = h.Id,
                    Name = h.Name,
                    Address = h.Address
                })
                .ToListAsync();

            return resultDto;
        }


        [HttpGet("{id}")]
        public ActionResult<HotelDTO> GetHotelById(int id)
        {
            var hotelDto = _context.Hotel?.Where(h => h.Id == id).FirstOrDefault();
            if(hotelDto == null)
            {
                return NotFound();

            }
            return Ok(hotelDto);
        }


        [HttpPost]
        public ActionResult<HotelDTO> CreateHotel(CreateHotelDTO createRequest)
        {
            if (string.IsNullOrEmpty(createRequest.Name) || createRequest.Name.Length > 120)
            {
                return BadRequest("Name must be provided and exceed 120 characters.");
            }

            if (string.IsNullOrEmpty(createRequest.Address))
            {
                return BadRequest("Must provide an address.");
            }

            var newHotel = new Hotel
            {
                Name = createRequest.Name,
                Address = createRequest.Address
            };

            _context.Hotel.Add(newHotel);
            _context.SaveChanges();

            var createdDto = new HotelDTO
            {
                Id = newHotel.Id,
                Name = newHotel.Name,
                Address = newHotel.Address
            };


            return CreatedAtAction(nameof(GetHotelById), new { id = createdDto.Id }, createdDto);
        }

        [HttpDelete("{id}")]

        public ActionResult DeleteHotel(int id)
        {
            var hotelToDelete = _context.Hotel.Find(id);
            if (hotelToDelete == null)
            {
                return NotFound();
            }

            _context.Hotel.Remove(hotelToDelete);
            _context.SaveChanges();

            return Ok(new HotelDTO
            {
                Id = hotelToDelete.Id,
                Name = hotelToDelete.Name,
                Address = hotelToDelete.Address
            });
        }
    }

}